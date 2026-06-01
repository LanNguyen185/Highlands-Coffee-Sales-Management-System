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
    public partial class FrmKH : Form
    {
        public FrmKH()
        {
            InitializeComponent();
        }

        BindingSource bsKH = new BindingSource();

        private void LoadKhachHang(string keyword = "")
        {
            string query = "SELECT MaKH, HoTenKH, SDTKH, EmailKH, DiemTichLuy FROM KhachHang WHERE IsActive = 1";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsKH.DataSource = dt;
            dgvKH.DataSource = bsKH;

            dgvKH.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvKH.Columns["MaKH"].HeaderText = "Mã khách hàng";
            dgvKH.Columns["HoTenKH"].HeaderText = "Họ tên";
            dgvKH.Columns["SDTKH"].HeaderText = "Số điện thoại";
            dgvKH.Columns["EmailKH"].HeaderText = "Email";
            dgvKH.Columns["DiemTichLuy"].HeaderText = "Điểm tích lũy";
        }
        private void FormKH_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
            bindingNavigator1.BindingSource = bsKH;
            AddBindings();
            txtMaKH.ReadOnly = true; // Mã tự sinh
        }

        private void AddBindings()
        {

            txtMaKH.DataBindings.Add("Text", bsKH, "MaKH", true, DataSourceUpdateMode.Never);
            txtHoTenKH.DataBindings.Add("Text", bsKH, "HoTenKH", true, DataSourceUpdateMode.Never);
            txtSoDTKH.DataBindings.Add("Text", bsKH, "SDTKH", true, DataSourceUpdateMode.Never);
            txtEmailKH.DataBindings.Add("Text", bsKH, "EmailKH", true, DataSourceUpdateMode.Never);
            txtDTL.DataBindings.Add("Text", bsKH, "DiemTichLuy", true, DataSourceUpdateMode.Never);
        }
        private string GenerateMaKH()
        {
            string query = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "KH001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2));
            number++;
            return "KH" + number.ToString("D3");
        }
        private SqlParameter[] GetKhachHangParameters()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaKH", txtMaKH.Text.Trim()),
                new SqlParameter("@HoTenKH", txtHoTenKH.Text.Trim()),
                new SqlParameter("@SDT", string.IsNullOrEmpty(txtSoDTKH.Text) ? (object)DBNull.Value : txtSoDTKH.Text),
                new SqlParameter("@Email", string.IsNullOrEmpty(txtEmailKH.Text) ? (object)DBNull.Value : txtEmailKH.Text),
                new SqlParameter("@Diem", string.IsNullOrEmpty(txtDTL.Text) ? 0 : int.Parse(txtDTL.Text))
            };
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }

            string query = "";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (rdoTenKH.Checked)
                query = "SELECT MaKH, HoTenKH, SDTKH, EmailKH, DiemTichLuy FROM KhachHang WHERE IsActive=1 AND HoTenKH LIKE @keyword";
            else if (rdoSDTKH.Checked)
                query = "SELECT MaKH, HoTenKH, SDTKH, EmailKH, DiemTichLuy FROM KhachHang WHERE IsActive=1 AND SDTKH LIKE @keyword";
            else
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm!");
                return;
            }

            parameters.Add(new SqlParameter("@keyword", "%" + keyword + "%"));

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
                if (dt.Rows.Count > 0)
                {
                    bsKH.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng!");
                    dgvKH.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!");
                return;
            }

            txtMaKH.Text = GenerateMaKH();

            string insertQuery = @"INSERT INTO KhachHang
                                   (MaKH, HoTenKH, SDTKH, EmailKH, DiemTichLuy, IsActive)
                                   VALUES (@MaKH, @HoTenKH, @SDT, @Email, @Diem,1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertQuery, GetKhachHangParameters());
                MessageBox.Show("Thêm khách hàng thành công!");
                LoadKhachHang();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            string updateQuery = @"UPDATE KhachHang
                                   SET HoTenKH=@HoTenKH, SDTKH=@SDT, EmailKH=@Email, DiemTichLuy=@Diem
                                   WHERE MaKH=@MaKH";

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateQuery, GetKhachHangParameters());
                MessageBox.Show("Cập nhật thành công!");
                LoadKhachHang();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa KH {txtMaKH.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string deleteQuery = "UPDATE KhachHang SET IsActive=0 WHERE MaKH=@MaKH";

                try
                {
                    DatabaseHelper.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@MaKH", txtMaKH.Text) });
                    MessageBox.Show("Xóa thành công!");
                    LoadKhachHang();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
            ClearFields();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvKH.Rows[e.RowIndex];

            txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
            txtHoTenKH.Text = row.Cells["HoTenKH"].Value?.ToString();
            txtSoDTKH.Text = row.Cells["SDTKH"].Value?.ToString();
            txtEmailKH.Text = row.Cells["EmailKH"].Value?.ToString();
            txtDTL.Text = row.Cells["DiemTichLuy"].Value?.ToString();
        }

        private void ClearFields()
        {
            txtHoTenKH.Clear();
            txtSoDTKH.Clear();
            txtEmailKH.Clear();
            txtDTL.Clear();
            txtMaKH.Text = GenerateMaKH(); // Sinh mã mới
            txtHoTenKH.Focus();
        }
    }
}
