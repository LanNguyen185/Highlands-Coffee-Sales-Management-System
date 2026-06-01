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
    public partial class FrmTaoTaiKhoan : Form
    {
        public FrmTaoTaiKhoan()
        {
            InitializeComponent();
        }
        BindingSource bsTK = new BindingSource();
        private void LoadTaiKhoan(string keyword = "")
        {
            string query = "SELECT MaTK, TenDN, MatKhauTK, Email, VaiTro FROM TaiKhoan";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsTK.DataSource = dt;
            dgvTK.DataSource = bsTK;

            dgvTK.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvTK.DefaultCellStyle.Font = new Font("Times New Roman", 10);

            dgvTK.Columns["MaTK"].HeaderText = "Mã tài khoản";
            dgvTK.Columns["TenDN"].HeaderText = "Tên đăng nhập";
            dgvTK.Columns["MatKhauTK"].HeaderText = "Mật khẩu";
            dgvTK.Columns["Email"].HeaderText = "Email";
            dgvTK.Columns["VaiTro"].HeaderText = "Vai trò";
        }
        private void AddBindings()
        {
            txtMaTK.DataBindings.Add("Text", bsTK, "MaTK", true, DataSourceUpdateMode.Never);
            txtTenDN.DataBindings.Add("Text", bsTK, "TenDN", true, DataSourceUpdateMode.Never);
            txtMK.DataBindings.Add("Text", bsTK, "MatKhauTK", true, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Add("Text", bsTK, "Email", true, DataSourceUpdateMode.Never);
            cbVaiTro.DataBindings.Add("Text", bsTK, "VaiTro", true, DataSourceUpdateMode.Never);
        }
        private string GenerateMaTK()
        {
            string query = "SELECT TOP 1 MaTK FROM TaiKhoan ORDER BY MaTK DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "TK001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2));
            number++;
            return "TK" + number.ToString("D3");
        }

        private SqlParameter[] GetTaiKhoanParameters()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaTK", txtMaTK.Text.Trim()),
                new SqlParameter("@TenDN", txtTenDN.Text.Trim()),
                new SqlParameter("@MatKhauTK", txtMK.Text.Trim()),
                new SqlParameter("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text),
                new SqlParameter("@VaiTro", cbVaiTro.Text.Trim())
            };
        }

        private void FormTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            cbVaiTro.Items.AddRange(new[] { "Manager", "Salesperson" });
            LoadTaiKhoan();
            bindingNavigator1.BindingSource = bsTK;
            AddBindings();
            txtMaTK.ReadOnly = true; // Mã tự sinh
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDN.Text) || string.IsNullOrWhiteSpace(txtMK.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!");
                return;
            }

            txtMaTK.Text = GenerateMaTK();

            string insertQuery = @"INSERT INTO TaiKhoan
                                   (MaTK, TenDN, MatKhauTK, Email, VaiTro)
                                   VALUES (@MaTK, @TenDN, @MatKhauTK, @Email, @VaiTro)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertQuery, GetTaiKhoanParameters());
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadTaiKhoan();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm tài khoản: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTK.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }

            string updateQuery = @"UPDATE TaiKhoan
                                   SET TenDN=@TenDN, MatKhauTK=@MatKhauTK, Email=@Email, VaiTro=@VaiTro
                                   WHERE MaTK=@MaTK";

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateQuery, GetTaiKhoanParameters());
                MessageBox.Show("Cập nhật thành công!");
                LoadTaiKhoan();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTK.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa TK {txtMaTK.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM TaiKhoan WHERE MaTK=@MaTK"; // hoặc update trạng thái nếu muốn soft delete

                try
                {
                    DatabaseHelper.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@MaTK", txtMaTK.Text) });
                    MessageBox.Show("Xóa thành công!");
                    LoadTaiKhoan();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa tài khoản: " + ex.Message);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            ClearFields();
        }
        private void dgvTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvTK.Rows[e.RowIndex];

            txtMaTK.Text = row.Cells["MaTK"].Value?.ToString();
            txtTenDN.Text = row.Cells["TenDN"].Value?.ToString();
            txtMK.Text = row.Cells["MatKhauTK"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            cbVaiTro.Text = row.Cells["VaiTro"].Value?.ToString();
        }
        private void ClearFields()
        {
            txtMaTK.Text = GenerateMaTK();
            txtTenDN.Clear();
            txtMK.Clear();
            txtEmail.Clear();
            cbVaiTro.SelectedIndex = -1;
            txtTenDN.Focus();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập Mã tài khoản cần tìm!");
                return;
            }

            string query = "SELECT MaTK, TenDN, MatKhauTK, Email, VaiTro " +
                           "FROM TaiKhoan WHERE MaTK LIKE @keyword";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@keyword", "%" + keyword + "%")
            };

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    bsTK.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản!");
                    dgvTK.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
