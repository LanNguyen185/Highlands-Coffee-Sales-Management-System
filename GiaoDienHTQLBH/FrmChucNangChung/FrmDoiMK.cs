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
    public partial class FrmDoiMK : Form
    {

        private void FormDoiMK_Load(object sender, EventArgs e)
        {

        }
        

        public FrmDoiMK()
        {
            InitializeComponent();
        }
        private void btnTao_Click(object sender, EventArgs e)
        {
            string mkCu = txtMKCu.Text.Trim();
            string mkMoi = txtMKMoi.Text.Trim();

            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu cũ và mật khẩu mới.");
                return;
            }

            // Kiểm tra mật khẩu cũ đúng
            string sqlCheck = "SELECT COUNT(*) FROM TAIKHOAN WHERE MaTK = @MaTK AND MatKhauTK = @MKCu";
            SqlParameter[] prCheck = {
        new SqlParameter("@MaTK", CurrentUser.MaTK),
        new SqlParameter("@MKCu", mkCu)
    };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(sqlCheck, prCheck));
            if (count == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc email hoặc mật khẩu hiện tại không đúng. Vui lòng kiểm tra lại!!");
                return;
            }

            // Cập nhật mật khẩu mới
            string sqlUpdate = "UPDATE TAIKHOAN SET MatKhauTK = @MKMoi WHERE MaTK = @MaTK";
            SqlParameter[] prUpdate = {
        new SqlParameter("@MKMoi", mkMoi),
        new SqlParameter("@MaTK", CurrentUser.MaTK)
    };

            int rows = DatabaseHelper.ExecuteNonQuery(sqlUpdate, prUpdate);
            if (rows > 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công.");
                txtMKCu.Clear();
                txtMKMoi.Clear();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại.");
            }
        }
    }
}
