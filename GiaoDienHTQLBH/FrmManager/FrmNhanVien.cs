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
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        BindingSource bsNV = new BindingSource();

        private void LoadNhanVien(string keyword = "")
        {
            string query = @"SELECT MaNV, HoTenNV, GioiTinhNV, NgaySinhNV, SDTNV, DiaChiNV, ChucVuNV, MaQL, MaTK
                 FROM NhanVien
                 WHERE IsActive = 1";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsNV.DataSource = dt;
            dgvNhanVien.DataSource = bsNV;

            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            //dgvNhanVien.RowHeadersWidth = 22;

            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns["HoTenNV"].HeaderText = "Họ tên";
            dgvNhanVien.Columns["GioiTinhNV"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["NgaySinhNV"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["SDTNV"].HeaderText = "Số điện thoại";
            dgvNhanVien.Columns["DiaChiNV"].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns["ChucVuNV"].HeaderText = "Chức vụ";
        }
       
        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            if (cbGioiTinh.Items.Count == 0)
            {
                cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            }
            cbGioiTinh.SelectedIndex = -1;
            LoadNhanVien();
            bindingNavigator1.BindingSource = bsNV;
            AddBindings();
            txtMaNV.ReadOnly = true; // Mã tự sinh
        }
        private void AddBindings()
        {
            // Add binding
            txtMaNV.DataBindings.Add("Text", bsNV, "MaNV", true, DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text", bsNV, "HoTenNV", true, DataSourceUpdateMode.Never);
            cbGioiTinh.DataBindings.Add("Text", bsNV, "GioiTinhNV", true, DataSourceUpdateMode.Never);
            dtpNgaySinhNV.DataBindings.Add("Value", bsNV, "NgaySinhNV", true, DataSourceUpdateMode.Never);
            txtDiaChiNV.DataBindings.Add("Text", bsNV, "DiaChiNV", true, DataSourceUpdateMode.Never);
            txtSoDTNV.DataBindings.Add("Text", bsNV, "SDTNV", true, DataSourceUpdateMode.Never);
            txtChucVu.DataBindings.Add("Text", bsNV, "ChucVuNV", true, DataSourceUpdateMode.Never);
            txtMQL.DataBindings.Add("Text", bsNV, "MaQL", true, DataSourceUpdateMode.Never);
            txtTaiKhoan.DataBindings.Add("Text", bsNV, "MaTK", true, DataSourceUpdateMode.Never);
        }
        private void ClearFields()
        {       
            txtHoTen.Clear();
            cbGioiTinh.SelectedIndex = -1;
            dtpNgaySinhNV.Value = DateTime.Now;
            txtDiaChiNV.Clear();
            txtSoDTNV.Clear();
            txtChucVu.Clear();
            txtMQL.Clear();
            txtTaiKhoan.Clear();
            txtMaNV.Text = GenerateMaNV(); // Sinh mã mới
            txtHoTen.Focus();
        }
        private string GenerateMaNV()
        {
            string query = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "NV001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2)) + 1;
            return "NV" + number.ToString("D3");
        }
        private SqlParameter[] GetNhanVienParameters()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaNV", txtMaNV.Text.Trim()),
                new SqlParameter("@HoTenNV", txtHoTen.Text.Trim()),
                new SqlParameter("@GioiTinhNV", string.IsNullOrEmpty(cbGioiTinh.Text) ? (object)DBNull.Value : cbGioiTinh.Text),
                new SqlParameter("@NgaySinhNV", dtpNgaySinhNV.Value),
                new SqlParameter("@DiaChiNV", string.IsNullOrEmpty(txtDiaChiNV.Text) ? (object)DBNull.Value : txtDiaChiNV.Text),
                new SqlParameter("@SDTNV", string.IsNullOrEmpty(txtSoDTNV.Text) ? (object)DBNull.Value : txtSoDTNV.Text),
                new SqlParameter("@ChucVuNV", string.IsNullOrEmpty(txtChucVu.Text) ? (object)DBNull.Value : txtChucVu.Text),
                new SqlParameter("@MaQL", string.IsNullOrEmpty(txtMQL.Text) ? (object)DBNull.Value : txtMQL.Text),
                new SqlParameter("@MaTK", string.IsNullOrEmpty(txtTaiKhoan.Text) ? (object)DBNull.Value : txtTaiKhoan.Text)
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }

            string query = "";
            if (rdoMaNV.Checked)
                query = "SELECT MaNV, HoTenNV, GioiTinhNV, NgaySinhNV, SDTNV, DiaChiNV, ChucVuNV, MaQL, MaTK FROM NhanVien WHERE IsActive=1 AND MaNV LIKE @keyword";
            else if (rdoTenNV.Checked)
                query = "SELECT MaNV, HoTenNV, GioiTinhNV, NgaySinhNV, SDTNV, DiaChiNV, ChucVuNV, MaQL, MaTK FROM NhanVien WHERE IsActive=1 AND HoTenNV LIKE @keyword";
            else
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm!");
                return;
            }

            DataTable dt = DatabaseHelper.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@keyword", "%" + keyword + "%") });
            if (dt.Rows.Count > 0)
                bsNV.DataSource = dt;
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên!");
                dgvNhanVien.DataSource = null;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa NV {txtMaNV.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string deleteQuery = "UPDATE NhanVien SET IsActive=0 WHERE MaNV=@MaNV";

                try
                {
                    DatabaseHelper.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@MaNV", txtMaNV.Text) });
                    MessageBox.Show("Xóa thành công!");
                    LoadNhanVien();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message);
                }
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!");
                return;
            }

            // Sinh mã nhân viên
            txtMaNV.Text = GenerateMaNV();

            // KIỂM TRA MAQL TỒN TẠI
            if (!string.IsNullOrWhiteSpace(txtMQL.Text))
            {
                if (!ExistsInTable("NhanVien", "MaNV", txtMQL.Text.Trim()))
                {
                    MessageBox.Show("Mã Quản Lý không tồn tại! Vui lòng kiểm tra lại.");
                    return;
                }
            }

            // KIỂM TRA MATK TỒN TẠI
            if (!string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                if (!ExistsInTable("TaiKhoan", "MaTK", txtTaiKhoan.Text.Trim()))
                {
                    MessageBox.Show("Mã Tài Khoản không tồn tại! Vui lòng kiểm tra lại hoặc tạo tài khoản đó trước khi thêm nhân viên mới.");
                    return;
                }
            }

            string insertQuery = @"INSERT INTO NhanVien
                                   (MaNV, HoTenNV, GioiTinhNV, NgaySinhNV, DiaChiNV, SDTNV, ChucVuNV, MaQL, MaTK, IsActive)
                                   VALUES (@MaNV, @HoTenNV, @GioiTinhNV, @NgaySinhNV, @DiaChiNV, @SDTNV, @ChucVuNV, @MaQL, @MaTK, 1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertQuery, GetNhanVienParameters());
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadNhanVien();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            string updateQuery = @"UPDATE NhanVien
                                   SET HoTenNV=@HoTenNV, GioiTinhNV=@GioiTinhNV, NgaySinhNV=@NgaySinhNV,
                                       DiaChiNV=@DiaChiNV, SDTNV=@SDTNV, ChucVuNV=@ChucVuNV,
                                       MaQL=@MaQL, MaTK=@MaTK
                                   WHERE MaNV=@MaNV";

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateQuery, GetNhanVienParameters());
                MessageBox.Show("Cập nhật thành công!");
                LoadNhanVien();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           LoadNhanVien();
           ClearFields();
        }
        private bool ExistsInTable(string table, string column, string value)
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @val ";

            object result = DatabaseHelper.ExecuteScalar(
                query,
                new SqlParameter[] { new SqlParameter("@val", value) }
            );

            return Convert.ToInt32(result) > 0;
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
            txtHoTen.Text = row.Cells["HoTenNV"].Value?.ToString();
            cbGioiTinh.Text = row.Cells["GioiTinhNV"].Value?.ToString();
            txtDiaChiNV.Text = row.Cells["DiaChiNV"].Value?.ToString();
            txtSoDTNV.Text = row.Cells["SDTNV"].Value?.ToString();
            txtChucVu.Text = row.Cells["ChucVuNV"].Value?.ToString();
            txtMQL.Text = row.Cells["MaQL"].Value?.ToString();
            txtTaiKhoan.Text = row.Cells["MaTK"].Value?.ToString();

            if (row.Cells["NgaySinhNV"].Value != DBNull.Value && DateTime.TryParse(row.Cells["NgaySinhNV"].Value.ToString(), out DateTime ngaySinh))
                dtpNgaySinhNV.Value = ngaySinh;
        }

        
    }
}
