using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GiaoDienHTQLBH.FrmDoiMK;

namespace GiaoDienHTQLBH
{
    public partial class FrmĐang_nhap : Form
    {
        public FrmĐang_nhap()
        {
            InitializeComponent();
        }

        private void Btn_DangNhap_Click(object sender, EventArgs e)
        {
            string user = txt_TaiKhoan.Text;
            string pass = txt_MatKhau.Text;
            string query = "SELECT MaTK, Email, VaiTro FROM TaiKhoan WHERE TenDN = @user AND MatKhauTK = @pass";
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@user", user),
                 new SqlParameter("@pass", pass)
            };

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    // Lưu thông tin tài khoản cơ bản
                    CurrentUser.MaTK = dt.Rows[0]["MaTK"].ToString();
                    CurrentUser.TenDN = user;
                    CurrentUser.Email = dt.Rows[0]["Email"].ToString();
                    CurrentUser.VaiTro = dt.Rows[0]["VaiTro"].ToString();         

                    // Mở form chính theo vai trò
                    this.Hide();
                    if (CurrentUser.VaiTro == "Manager")
                    {
                        using (FrmMain_Manager M = new FrmMain_Manager())
                        {
                            M.ShowDialog();
                        }
                    }
                    else if (CurrentUser.VaiTro == "Salesperson")
                    {
                        using (FrmMain_Salesperson S = new FrmMain_Salesperson())
                        {
                            S.ShowDialog();
                        }
                    }

                    // Reset lại form đăng nhập sau khi form chính đóng
                    this.Show();
                    txt_TaiKhoan.Clear();
                    txt_MatKhau.Clear();
                    txt_TaiKhoan.Focus();
                    ckbHienMK.Checked = false;
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void Btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Đang_nhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình ??","Thông báo",MessageBoxButtons.OKCancel)!=System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }

        private void linkLabel_QuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FrmQuen_mat_khau qmk = new FrmQuen_mat_khau();
            qmk.ShowDialog();
            this.Show(); // hiện lại sau khi đóng form quên mật khẩu
        }

        private void ckbHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienMK.Checked)
            {
                // Hiện mật khẩu rõ
                txt_MatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                // Ẩn mật khẩu bằng ký tự *
                txt_MatKhau.UseSystemPasswordChar = true;
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            txt_MatKhau.UseSystemPasswordChar = true;
        }
    }
}
