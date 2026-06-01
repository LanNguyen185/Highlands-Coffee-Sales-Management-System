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
    public partial class FrmPNK : Form
    {
        public FrmPNK()
        {
            InitializeComponent();
        }
        BindingSource bsPNK = new BindingSource();
        private void LoadPNK(string query = "")
        {
            if (string.IsNullOrEmpty(query))
            {
                query = @"SELECT MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK, 
                                 LiDonNhapKho, MaNCC, MaNV
                          FROM PhieuNhapKho
                          WHERE IsActive = 1";
            }

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsPNK.DataSource = dt;
            dgvPNK.DataSource = bsPNK;
            dgvPNK.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);

            dgvPNK.Columns["MaPNK"].HeaderText = "Mã phiếu nhập kho";
            dgvPNK.Columns["TongSoLuongNK"].HeaderText = "Tổng số lượng nhập";
            dgvPNK.Columns["NgayNhap"].HeaderText = "Ngày nhập";
            dgvPNK.Columns["GhiChuNK"].HeaderText = "Ghi chú";
            dgvPNK.Columns["LiDonNhapKho"].HeaderText = "Lí do nhập kho";
            dgvPNK.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            dgvPNK.Columns["MaNV"].HeaderText = "Mã nhân viên lập";
        }
        private void AddBindings()
        {
            txtMPNK.DataBindings.Add("Text", bsPNK, "MaPNK", true, DataSourceUpdateMode.Never);
            txtSL.DataBindings.Add("Text", bsPNK, "TongSoLuongNK", true, DataSourceUpdateMode.Never);
            dtpNgayNhap.DataBindings.Add("Value", bsPNK, "NgayNhap", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", bsPNK, "GhiChuNK", true, DataSourceUpdateMode.Never);
            txtLyDo.DataBindings.Add("Text", bsPNK, "LiDonNhapKho", true, DataSourceUpdateMode.Never);
            txtMNCC.DataBindings.Add("Text", bsPNK, "MaNCC", true, DataSourceUpdateMode.Never);
            txtMNV.DataBindings.Add("Text", bsPNK, "MaNV", true, DataSourceUpdateMode.Never);
        }
        private void ClearFields()
        {
            txtSL.Clear();
            txtGhiChu.Clear();
            txtLyDo.Clear();
            txtMNCC.Clear();
            txtMNV.Clear();
            dtpNgayNhap.Value = DateTime.Now;

            txtMPNK.Text = GenerateMaPNK();
        }
        private string GenerateMaPNK()
        {
            string query = "SELECT TOP 1 MaPNK FROM PhieuNhapKho ORDER BY MaPNK DESC";
            object result = DatabaseHelper.ExecuteScalar(query,null);

            if (result == null)
                return "PNK001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(3)) + 1;

            return "PNK" + number.ToString("D3");
        }
        private SqlParameter[] GetPNKParams()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaPNK", txtMPNK.Text.Trim()),
                new SqlParameter("@TongSoLuongNK", string.IsNullOrEmpty(txtSL.Text) ? (object)DBNull.Value : txtSL.Text),
                new SqlParameter("@NgayNhap", dtpNgayNhap.Value),
                new SqlParameter("@GhiChuNK", string.IsNullOrEmpty(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text),
                new SqlParameter("@LiDonNhapKho", string.IsNullOrEmpty(txtLyDo.Text) ? (object)DBNull.Value : txtLyDo.Text),
                new SqlParameter("@MaNCC", txtMNCC.Text.Trim()),
                new SqlParameter("@MaNV", txtMNV.Text.Trim())
            };
        }
        private bool ExistsInTable(string table, string column, string value)
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column}=@v";
            object result = DatabaseHelper.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@v", value) });
            return Convert.ToInt32(result) > 0;
        }
       
        private void FormPNK_Load(object sender, EventArgs e)
        {
            LoadPNK();
            bindingNavigator1.BindingSource = bsPNK;
            AddBindings();

            txtMPNK.ReadOnly = true;  // Mã tự sinh
        }     

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMNCC.Text) || string.IsNullOrWhiteSpace(txtMNV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã NCC và Mã NV!");
                return;
            }

            if (!ExistsInTable("NhaCungCap", "MaNCC", txtMNCC.Text.Trim()))
            {
                MessageBox.Show("Mã NCC không tồn tại!");
                return;
            }

            if (!ExistsInTable("NhanVien", "MaNV", txtMNV.Text.Trim()))
            {
                MessageBox.Show("Mã NV không tồn tại!");
                return;
            }

            txtMPNK.Text = GenerateMaPNK();

            string query = @"INSERT INTO PhieuNhapKho
                             (MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK, LiDonNhapKho, MaNCC, MaNV, IsActive)
                             VALUES (@MaPNK, @TongSoLuongNK, @NgayNhap, @GhiChuNK, @LiDonNhapKho, @MaNCC, @MaNV, 1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetPNKParams());
                MessageBox.Show("Thêm phiếu nhập thành công!");
                LoadPNK();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = @"UPDATE PhieuNhapKho
                             SET TongSoLuongNK=@TongSoLuongNK, NgayNhap=@NgayNhap,
                                 GhiChuNK=@GhiChuNK, LiDonNhapKho=@LiDonNhapKho,
                                 MaNCC=@MaNCC, MaNV=@MaNV
                             WHERE MaPNK=@MaPNK";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetPNKParams());
                MessageBox.Show("Cập nhật thành công!");
                LoadPNK();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Xóa PNK {txtMPNK.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = "UPDATE PhieuNhapKho SET IsActive=0 WHERE MaPNK=@MaPNK";
            DatabaseHelper.ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@MaPNK", txtMPNK.Text) });

            MessageBox.Show("Xóa thành công!");
            LoadPNK();
            ClearFields();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (rdoMPNK.Checked)
            {
                LoadPNK($"SELECT MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK,LiDonNhapKho, MaNCC, MaNV FROM PhieuNhapKho WHERE IsActive=1 AND MaPNK LIKE '%{txtTimKiem.Text}%'");
            }
            else if (rdoNgay.Checked)
            {
                LoadPNK($"SELECT MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK,LiDonNhapKho, MaNCC, MaNV FROM PhieuNhapKho WHERE IsActive=1 AND DAY(NgayNhap) = {txtTimKiem.Text}");
            }
            else if (rdoThang.Checked)
            {
                LoadPNK($"SELECT MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK,LiDonNhapKho, MaNCC, MaNV  FROM PhieuNhapKho WHERE IsActive=1 AND MONTH(NgayNhap) = {txtTimKiem.Text}");
            }
            else if (rdoNam.Checked)
            {
                LoadPNK($"SELECT MaPNK, TongSoLuongNK, NgayNhap, GhiChuNK,LiDonNhapKho, MaNCC, MaNV  FROM PhieuNhapKho WHERE IsActive=1 AND YEAR(NgayNhap) = {txtTimKiem.Text}");
            }
            else
            {
                MessageBox.Show("Hãy chọn tiêu chí tìm kiếm!");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadPNK();
            ClearFields();
        }
        private void dgvPNK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPNK.Rows[e.RowIndex];

            txtMPNK.Text = row.Cells["MaPNK"].Value.ToString();
            txtSL.Text = row.Cells["TongSoLuongNK"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChuNK"].Value.ToString();
            txtLyDo.Text = row.Cells["LiDonNhapKho"].Value.ToString();
            txtMNCC.Text = row.Cells["MaNCC"].Value.ToString();
            txtMNV.Text = row.Cells["MaNV"].Value.ToString();

            if (row.Cells["NgayNhap"].Value != DBNull.Value)
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
        }
    }
}
