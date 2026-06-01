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
    public partial class FrmTTCN1 : Form
    {
        public FrmTTCN1()
        {
            InitializeComponent();
        }

        private void FormTTCN_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán thông tin tài khoản từ CurrentUser
                label23.Text = CurrentUser.MaTK;
                label22.Text = CurrentUser.TenDN;
                label12.Text = CurrentUser.Email;
                label4.Text = CurrentUser.VaiTro;

                // Lấy mật khẩu từ bảng TAIKHOAN
                string queryPass = "SELECT MatKhauTK FROM TaiKhoan WHERE MaTK = @MaTK";
                SqlParameter[] passParam = {
            new SqlParameter("@MaTK", CurrentUser.MaTK)
        };
                object mk = DatabaseHelper.ExecuteScalar(queryPass, passParam);
                if (mk != null)
                    label21.Text = mk.ToString();
                else
                    label21.Text = "(Không tìm thấy)";

                // Lấy thông tin cá nhân từ DB
                string query = @"
            SELECT MaNV, HoTenNV, GioiTinhNV, NgaySinhNV, DiaChiNV, SDTNV,
                   ChucVuNV, MaQL
            FROM NhanVien
            WHERE MaTK = @MaTK";

                SqlParameter[] parameters = {
            new SqlParameter("@MaTK", CurrentUser.MaTK)
        };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    label13.Text = row["MaNV"].ToString();
                    label2.Text = row["HoTenNV"].ToString();
                    label3.Text = row["GioiTinhNV"].ToString();
                    label5.Text = Convert.ToDateTime(row["NgaySinhNV"]).ToString("dd/MM/yyyy");
                    label15.Text = row["DiaChiNV"].ToString();
                    label8.Text = row["SDTNV"].ToString();
                    label16.Text = row["ChucVuNV"].ToString();
                    label18.Text = row["MaQL"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin cá nhân.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message);
            }
        }
    }
}
