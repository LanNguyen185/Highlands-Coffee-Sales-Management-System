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

namespace GiaoDienHTQLBH
{
    public partial class FrmQuen_mat_khau : Form
    {
        public FrmQuen_mat_khau()
        {
            InitializeComponent();
        }

        private void Btn_LayLaiMK_Click(object sender, EventArgs e)
        {
            string tenDN = txt_TaiKhoan.Text.Trim();
            string email = txt_Email_QuenMK.Text.Trim();

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show(" Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string query = "SELECT MatKhauTK FROM TaiKhoan WHERE TenDN = @TenDN AND Email = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDN", tenDN),
                new SqlParameter("@Email", email)
            };

            try
            {
                object result = DatabaseHelper.ExecuteScalar(query, parameters);

                if (result != null)
                {
                    string matKhau = result.ToString();
                    lbl_LaylaiMK.Text = $"{matKhau}";
                }
                else
                {
                    lbl_LaylaiMK.Text = "Không tìm thấy tài khoản với thông tin đã nhập.";
                }
            }
            catch (Exception ex)
            {
                lbl_LaylaiMK.Text = "Lỗi: " + ex.Message;
            }
        }
        private void Btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
