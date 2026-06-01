using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienHTQLBH
{
    public partial class FrmCLV : Form
    {
        public FrmCLV()
        {
            InitializeComponent();
        }

        BindingSource bsCLV = new BindingSource();
        BindingSource bsCaDangKy = new BindingSource();
        private void LoadCLV()
        {
            string query = @"SELECT MaCa, NgayLam, ThoiGianBD, ThoiGianKT, Ca
                     FROM CaLamViec
                     WHERE IsActive = 1";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            bsCLV.DataSource = dt;       
            dgvCLV.DataSource = bsCLV;   

            dgvCLV.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvCLV.Columns["MaCa"].HeaderText = "Mã Ca";
            dgvCLV.Columns["NgayLam"].HeaderText = "Ngày làm";
            dgvCLV.Columns["ThoiGianBD"].HeaderText = "Giờ bắt đầu";
            dgvCLV.Columns["ThoiGianKT"].HeaderText = "Giờ kết thúc";
            dgvCLV.Columns["Ca"].HeaderText = "Ca";
        }
        private void LoadCa_DangKy()
        {
            string query = @"SELECT CDK.MaCa, NV.MaNV, NV.HoTenNV
                     FROM Ca_DangKy CDK
                     JOIN NhanVien NV ON CDK.MaNV = NV.MaNV";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            bsCaDangKy.DataSource = dt;
            dgvCa_dangky.DataSource = bsCaDangKy;

            dgvCa_dangky.RowHeadersWidth = 22;
            dgvCa_dangky.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvCa_dangky.Columns["MaCa"].HeaderText = "Mã Ca";
            dgvCa_dangky.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvCa_dangky.Columns["HoTenNV"].HeaderText = "Họ tên nhân viên";
        }
        private void BsCLV_PositionChanged(object sender, EventArgs e)
        {
            if (bsCLV.Current == null) return;

            DataRowView row = bsCLV.Current as DataRowView;
            if (row == null) return;

            // Load TIME → DateTimePicker
            if (row["ThoiGianBD"] != DBNull.Value)
                dtpStart.Value = DateTime.Today + (TimeSpan)row["ThoiGianBD"];

            if (row["ThoiGianKT"] != DBNull.Value)
                dtpEnd.Value = DateTime.Today + (TimeSpan)row["ThoiGianKT"];
        }
        private void FormCLV_Load(object sender, EventArgs e)
        {
            // Set dữ liệu cho combobox Ca
            if (cbCa.Items.Count == 0)
            {
                cbCa.Items.AddRange(new object[] { "Sáng", "Chiều", "Tối" });
            }
            cbCa.SelectedIndex = -1;

            LoadCLV();
            LoadCa_DangKy();
            bindingNavigatorCLV.BindingSource = bsCLV;
            bindingNavigatorDK.BindingSource = bsCaDangKy;
            AddBindings();
            txtMC.ReadOnly = true;
            bsCLV.PositionChanged += BsCLV_PositionChanged;
        }
       
        private void AddBindings()
        {
            txtMC.DataBindings.Add("Text", bsCLV, "MaCa", true, DataSourceUpdateMode.Never);
            cbCa.DataBindings.Add("Text", bsCLV, "Ca", true, DataSourceUpdateMode.Never);
            dtpNgayLam.DataBindings.Add("Value", bsCLV, "NgayLam", true, DataSourceUpdateMode.Never);           
        }
        private string GenerateMaCa()
        {
            string query = "SELECT TOP 1 MaCa FROM CaLamViec ORDER BY MaCa DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null || result == DBNull.Value)
                return "C001";

            string last = result.ToString();
            int num = int.Parse(last.Substring(2)) + 1;
            return "C" + num.ToString("D3");
        }
        private SqlParameter[] GetAllParams()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaCa", txtMC.Text.Trim()),
                new SqlParameter("@NgayLam", dtpNgayLam.Value),
                new SqlParameter("@TGBD", dtpStart.Value.TimeOfDay),
                new SqlParameter("@TGKT", dtpEnd.Value.TimeOfDay),
                new SqlParameter("@Ca", cbCa.Text)
            };
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMC.Text))
            {
                MessageBox.Show("Vui lòng chọn ca để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa ca này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string query = "UPDATE CaLamViec SET IsActive = 0 WHERE MaCa=@MaCa";

            try
            {
                SqlParameter[] pr = { new SqlParameter("@MaCa", txtMC.Text) };

                int rows = DatabaseHelper.ExecuteNonQuery(query, pr);

                if (rows > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadCLV();
                    ClearFields();
                }
                else
                    MessageBox.Show("Không thể xóa!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbCa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn ca!");
                return;
            }

            if (dtpStart.Value >= dtpEnd.Value)
            {
                MessageBox.Show("Giờ bắt đầu phải nhỏ hơn giờ kết thúc!");
                return;
            }

            txtMC.Text = GenerateMaCa();

            string query =
                @"INSERT INTO CaLamViec (MaCa, NgayLam, ThoiGianBD, ThoiGianKT, Ca, IsActive)
                  VALUES (@MaCa, @NgayLam, @TGBD, @TGKT, @Ca, 1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetAllParams());
                MessageBox.Show("Thêm ca thành công!");

                LoadCLV();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMC.Text))
            {
                MessageBox.Show("Vui lòng chọn ca để sửa!");
                return;
            }

            if (dtpStart.Value >= dtpEnd.Value)
            {
                MessageBox.Show("Giờ bắt đầu phải nhỏ hơn giờ kết thúc!");
                return;
            }

            string query =
                @"UPDATE CaLamViec
                  SET NgayLam=@NgayLam, ThoiGianBD=@TGBD, ThoiGianKT=@TGKT, Ca=@Ca
                  WHERE MaCa=@MaCa";

            try
            {
                int rows = DatabaseHelper.ExecuteNonQuery(query, GetAllParams());

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadCLV();
                }
                else
                    MessageBox.Show("Không có thay đổi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            txtMC.Text = GenerateMaCa();
            cbCa.SelectedIndex = -1;

            dtpNgayLam.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            dtpNgayLam.Focus();
        }

        private void dgvCLV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCLV.Rows[e.RowIndex];

            txtMC.Text = row.Cells["MaCa"].Value?.ToString();
            cbCa.Text = row.Cells["Ca"].Value?.ToString();

            if (DateTime.TryParse(row.Cells["NgayLam"].Value?.ToString(), out DateTime ngay))
                dtpNgayLam.Value = ngay;

            if (TimeSpan.TryParse(row.Cells["ThoiGianBD"].Value?.ToString(), out TimeSpan bd))
                dtpStart.Value = DateTime.Today + bd;

            if (TimeSpan.TryParse(row.Cells["ThoiGianKT"].Value?.ToString(), out TimeSpan kt))
                dtpEnd.Value = DateTime.Today + kt;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCLV();
            ClearFields();
        }
    }
}
