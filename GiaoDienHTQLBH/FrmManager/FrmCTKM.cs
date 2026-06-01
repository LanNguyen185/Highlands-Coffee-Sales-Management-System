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
    public partial class FrmCTKM : Form
    {
        public FrmCTKM()
        {
            InitializeComponent();
        }
        BindingSource bsKM = new BindingSource();
        private void LoadKM()
        {
            string query = "SELECT MaKM, TenKM, TuNgay, DenNgay, LoaiKM, DieuKienKM FROM CTKhuyenMai WHERE IsActive = 1";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsKM.DataSource = dt;
            dgvKM.DataSource = bsKM;
            dgvKM.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            SetColumnHeaderText();
        }
        private void SetColumnHeaderText()
        {
            dgvKM.Columns["MaKM"].HeaderText = "Mã CT khuyến mãi";
            dgvKM.Columns["TenKM"].HeaderText = "Tên CT khuyến mãi";
            dgvKM.Columns["TuNgay"].HeaderText = "Từ ngày";
            dgvKM.Columns["DenNgay"].HeaderText = "Đến ngày";
            dgvKM.Columns["LoaiKM"].HeaderText = "Loại khuyến mãi";
            dgvKM.Columns["DieuKienKM"].HeaderText = "Điều kiện khuyến mãi";
        }

        private void AddBindings()
        {
            txtMaKM.DataBindings.Add("Text", bsKM, "MaKM", true, DataSourceUpdateMode.Never);
            txtTenKM.DataBindings.Add("Text", bsKM, "TenKM", true, DataSourceUpdateMode.Never);
            dtpTuNgay.DataBindings.Add("Value", bsKM, "TuNgay", true, DataSourceUpdateMode.Never);
            dtpDenNgay.DataBindings.Add("Value", bsKM, "DenNgay", true, DataSourceUpdateMode.Never);
            txtLKM.DataBindings.Add("Text", bsKM, "LoaiKM", true, DataSourceUpdateMode.Never);
            txtDK.DataBindings.Add("Text", bsKM, "DieuKienKM", true, DataSourceUpdateMode.Never);
        }
        private void FormCTKM_Load(object sender, EventArgs e)
        {
            LoadKM();
            bindingNavigator1.BindingSource = bsKM;
            AddBindings();
            txtMaKM.ReadOnly = true; // Mã KM tự sinh
        }
        private string GenerateMaKM()
        {
            string query = "SELECT TOP 1 MaKM FROM CTKhuyenMai ORDER BY MaKM DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "KM001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2)) + 1;
            return "KM" + number.ToString("D3");
        }
        private SqlParameter[] GetKMParameters()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaKM", txtMaKM.Text.Trim()),
                new SqlParameter("@TenKM", txtTenKM.Text.Trim()),
                new SqlParameter("@TuNgay", dtpTuNgay.Value),
                new SqlParameter("@DenNgay", dtpDenNgay.Value),
                new SqlParameter("@LoaiKM", txtLKM.Text.Trim()),
                new SqlParameter("@DieuKienKM", txtDK.Text.Trim())
            };
        }
       

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm!");
                return;
            }

            string where = "";
            List<SqlParameter> pr = new List<SqlParameter>();

            // Tìm theo mã
            if (rdoMCTKM.Checked)
            {
                where = " WHERE IsActive=1 AND MaKM LIKE @keyword";
                pr.Add(new SqlParameter("@keyword", "%" + input + "%"));
            }
            // Tìm theo ngày
            else if (rdoNgay.Checked)
            {
                if (!int.TryParse(input, out int ngay) || ngay < 1 || ngay > 31)
                {
                    MessageBox.Show("Ngày không hợp lệ!");
                    return;
                }
                where = " WHERE IsActive=1 AND ( DAY(TuNgay) = @ngay OR DAY(DenNgay) = @ngay)";
                pr.Add(new SqlParameter("@ngay", ngay));
            }
            // Tìm theo tháng
            else if (rdoThang.Checked)
            {
                if (!int.TryParse(input, out int thang) || thang < 1 || thang > 12)
                {
                    MessageBox.Show("Tháng không hợp lệ!");
                    return;
                }
                where = " WHERE IsActive=1 AND (MONTH(TuNgay) = @t OR MONTH(DenNgay) = @t)";
                pr.Add(new SqlParameter("@t", thang));
            }
            // Tìm theo năm
            else if (rdoNam.Checked)
            {
                if (!int.TryParse(input, out int nam))
                {
                    MessageBox.Show("Năm không hợp lệ!");
                    return;
                }
                where = " WHERE IsActive=1 AND (YEAR(TuNgay) = @n OR YEAR(DenNgay) = @n)";
                pr.Add(new SqlParameter("@n", nam));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm!");
                return;
            }

            string query = "SELECT MaKM, TenKM, TuNgay, DenNgay, LoaiKM, DieuKienKM FROM CTKhuyenMai " + where;

            DataTable dt = DatabaseHelper.ExecuteQuery(query, pr.ToArray());

            if (dt.Rows.Count > 0)
                bsKM.DataSource = dt;
            else
            {
                MessageBox.Show("Không tìm thấy!");
                dgvKM.DataSource = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKM.Text))
            {
                MessageBox.Show("Vui lòng nhập tên KM!");
                return;
            }

            txtMaKM.Text = GenerateMaKM();

            string query = @"INSERT INTO CTKhuyenMai
                             (MaKM, TenKM, TuNgay, DenNgay, LoaiKM, DieuKienKM, IsActive)
                             VALUES (@MaKM, @TenKM, @TuNgay, @DenNgay, @LoaiKM, @DieuKienKM,1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetKMParameters());
                MessageBox.Show("Thêm khuyến mãi thành công!");
                LoadKM();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm KM: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKM.Text))
            {
                MessageBox.Show("Chọn KM để sửa!");
                return;
            }

            string query = @"UPDATE CTKhuyenMai SET
                             TenKM=@TenKM, TuNgay=@TuNgay, DenNgay=@DenNgay,
                             LoaiKM=@LoaiKM, DieuKienKM=@DieuKienKM
                             WHERE MaKM=@MaKM";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetKMParameters());
                MessageBox.Show("Cập nhật thành công!");
                LoadKM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa KM: " + ex.Message);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKM.Text))
            {
                MessageBox.Show("Chọn KM để xóa!");
                return;
            }

            DialogResult r = MessageBox.Show("Xóa khuyến mãi này?", "Hỏi", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                string query = "UPDATE CTKhuyenMai SET IsActive = 0 WHERE MaKM=@MaKM";
                SqlParameter[] pr = { new SqlParameter("@MaKM", txtMaKM.Text.Trim()) };

                int rows = DatabaseHelper.ExecuteNonQuery(query, pr);

                if (rows > 0)
                {
                    MessageBox.Show("Đã xóa!");
                    LoadKM();
                    ClearFields();
                }
            }
        }
        private void dgvCTKM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvKM.Rows[e.RowIndex];

            txtMaKM.Text = r.Cells["MaKM"].Value?.ToString();
            txtTenKM.Text = r.Cells["TenKM"].Value?.ToString();
            txtLKM.Text = r.Cells["LoaiKM"].Value?.ToString();
            txtDK.Text = r.Cells["DieuKienKM"].Value?.ToString();

            if (r.Cells["TuNgay"].Value != DBNull.Value)
                dtpTuNgay.Value = Convert.ToDateTime(r.Cells["TuNgay"].Value);

            if (r.Cells["DenNgay"].Value != DBNull.Value)
                dtpDenNgay.Value = Convert.ToDateTime(r.Cells["DenNgay"].Value);
        }

        private void ClearFields()
        {
            txtTenKM.Clear();
            txtLKM.Clear();
            txtDK.Clear();
            dtpTuNgay.Value = DateTime.Now;
            dtpDenNgay.Value = DateTime.Now;
            txtMaKM.Text = GenerateMaKM(); // sinh mã mới
            txtTenKM.Focus();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadKM();
            ClearFields();
        }
    }
}
