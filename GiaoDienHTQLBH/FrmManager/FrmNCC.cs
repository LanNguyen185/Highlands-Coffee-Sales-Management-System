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
    public partial class FrmNCC : Form
    {
        public FrmNCC()
        {
            InitializeComponent();
        }
        BindingSource bsNCC = new BindingSource();
        private void LoadNCC()
        {
            string query = "SELECT MaNCC, TenNCC, DiaChiNCC, SDTNCC, EmailNCC, LoaiHangCC, TinhTrangNCC FROM NhaCungCap WHERE IsActive = 1";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsNCC.DataSource = dt;
            dgvNCC.DataSource = bsNCC;
            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);

            dgvNCC.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns["TenNCC"].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns["DiaChiNCC"].HeaderText = "Địa chỉ";
            dgvNCC.Columns["SDTNCC"].HeaderText = "Số điện thoại";
            dgvNCC.Columns["EmailNCC"].HeaderText = "Email";
            dgvNCC.Columns["LoaiHangCC"].HeaderText = "Loại hàng cung cấp";
            dgvNCC.Columns["TinhTrangNCC"].HeaderText = "Tình trạng";
        }
        private void FormNCC_Load(object sender, EventArgs e)
        {
            if (cbTinhTrang.Items.Count == 0)
            {
                cbTinhTrang.Items.AddRange(new object[] { "Hoạt động", "Tạm đóng cửa" });
            }
            cbTinhTrang.SelectedIndex = -1;
            LoadNCC();
            bindingNavigator1.BindingSource = bsNCC;
            AddBindings();
            txtMaNCC.ReadOnly = true; // Mã tự sinh
        }
        private void AddBindings()
        {
            txtMaNCC.DataBindings.Add("Text", bsNCC, "MaNCC", true, DataSourceUpdateMode.Never);
            txtTenNCC.DataBindings.Add("Text", bsNCC, "TenNCC", true, DataSourceUpdateMode.Never);
            txtDiaChiNCC.DataBindings.Add("Text", bsNCC, "DiaChiNCC", true, DataSourceUpdateMode.Never);
            txtSoDTNCC.DataBindings.Add("Text", bsNCC, "SDTNCC", true, DataSourceUpdateMode.Never);
            txtEmailNCC.DataBindings.Add("Text", bsNCC, "EmailNCC", true, DataSourceUpdateMode.Never);
            txtLoaiHang.DataBindings.Add("Text", bsNCC, "LoaiHangCC", true, DataSourceUpdateMode.Never);
            cbTinhTrang.DataBindings.Add("Text", bsNCC, "TinhTrangNCC", true, DataSourceUpdateMode.Never);
        }
        private void ClearFields()
        {
            txtTenNCC.Clear();
            txtDiaChiNCC.Clear();
            txtSoDTNCC.Clear();
            txtEmailNCC.Clear();
            txtLoaiHang.Clear();
            cbTinhTrang.SelectedIndex = -1;
            txtMaNCC.Text = GenerateMaNCC(); // Sinh mã mới
            txtTenNCC.Focus();
        }
        private string GenerateMaNCC()
        {
            string query = "SELECT TOP 1 MaNCC FROM NhaCungCap ORDER BY MaNCC DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null || result == DBNull.Value)
                return "NCC001";

            string last = result.ToString();
            int number = int.Parse(last.Substring(3)) + 1;
            return "NCC" + number.ToString("D3");
        }
        private SqlParameter[] GetNCCParams()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaNCC", txtMaNCC.Text.Trim()),
                new SqlParameter("@TenNCC", txtTenNCC.Text.Trim()),
                new SqlParameter("@DiaChiNCC", string.IsNullOrEmpty(txtDiaChiNCC.Text) ? (object)DBNull.Value : txtDiaChiNCC.Text),
                new SqlParameter("@SDTNCC", string.IsNullOrEmpty(txtSoDTNCC.Text) ? (object)DBNull.Value : txtSoDTNCC.Text),
                new SqlParameter("@EmailNCC", string.IsNullOrEmpty(txtEmailNCC.Text) ? (object)DBNull.Value : txtEmailNCC.Text),
                new SqlParameter("@LoaiHangCC", string.IsNullOrEmpty(txtLoaiHang.Text) ? (object)DBNull.Value : txtLoaiHang.Text),
                new SqlParameter("@TinhTrangNCC", string.IsNullOrEmpty(cbTinhTrang.Text) ? (object)DBNull.Value : cbTinhTrang.Text)
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            string query = "";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập thông tin cần tìm!");
                return;
            }

            if (rdoTenNCC.Checked)
                query = "SELECT MaNCC, TenNCC, DiaChiNCC, SDTNCC, EmailNCC, LoaiHangCC, TinhTrangNCC FROM NhaCungCap WHERE IsActive=1 AND TenNCC LIKE @kw";
            else if (rdoLoaiHang.Checked)
                query = "SELECT MaNCC, TenNCC, DiaChiNCC, SDTNCC, EmailNCC, LoaiHangCC, TinhTrangNCC FROM NhaCungCap WHERE IsActive=1 AND LoaiHangCC LIKE @kw";
            else
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm!");
                return;
            }

            parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

                if (dt.Rows.Count > 0)
                    bsNCC.DataSource = dt;
                else
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp nào!");
                    dgvNCC.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên NCC!");
                return;
            }

            txtMaNCC.Text = GenerateMaNCC();

            string insertQuery = @"INSERT INTO NhaCungCap
                                   (MaNCC, TenNCC, DiaChiNCC, SDTNCC, EmailNCC, 
                                    LoaiHangCC, TinhTrangNCC, IsActive)
                                   VALUES (@MaNCC, @TenNCC, @DiaChiNCC, @SDTNCC,
                                           @EmailNCC, @LoaiHangCC, @TinhTrangNCC, 1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertQuery, GetNCCParams());
                MessageBox.Show("Thêm nhà cung cấp thành công!");
                LoadNCC();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm NCC: " + ex.Message);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn NCC!");
                return;
            }

            string updateQuery = @"UPDATE NhaCungCap SET 
                                   TenNCC=@TenNCC, DiaChiNCC=@DiaChiNCC,
                                   SDTNCC=@SDTNCC, EmailNCC=@EmailNCC,
                                   LoaiHangCC=@LoaiHangCC, TinhTrangNCC=@TinhTrangNCC
                                   WHERE MaNCC=@MaNCC";

            try
            {
                DatabaseHelper.ExecuteNonQuery(updateQuery, GetNCCParams());
                MessageBox.Show("Cập nhật thành công!");
                LoadNCC();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa NCC: " + ex.Message);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn NCC!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string deleteQuery = "UPDATE NhaCungCap SET IsActive=0 WHERE MaNCC=@MaNCC";

                try
                {
                    DatabaseHelper.ExecuteNonQuery(deleteQuery,
                        new SqlParameter[] { new SqlParameter("@MaNCC", txtMaNCC.Text) });

                    MessageBox.Show("Xóa thành công!");
                    LoadNCC();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa NCC: " + ex.Message);
                }
            }
        }
        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvNCC.Rows[e.RowIndex];

            txtMaNCC.Text = r.Cells["MaNCC"].Value?.ToString();
            txtTenNCC.Text = r.Cells["TenNCC"].Value?.ToString();
            txtDiaChiNCC.Text = r.Cells["DiaChiNCC"].Value?.ToString();
            txtSoDTNCC.Text = r.Cells["SDTNCC"].Value?.ToString();
            txtEmailNCC.Text = r.Cells["EmailNCC"].Value?.ToString();
            txtLoaiHang.Text = r.Cells["LoaiHangCC"].Value?.ToString();
            cbTinhTrang.Text = r.Cells["TinhTrangNCC"].Value?.ToString();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadNCC();
            ClearFields();
        }
    }
}
