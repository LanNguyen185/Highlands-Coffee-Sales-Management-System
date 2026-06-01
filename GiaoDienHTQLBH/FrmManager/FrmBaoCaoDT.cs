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
    public partial class FrmBaoCaoDT : Form
    {
        public FrmBaoCaoDT()
        {
            InitializeComponent();
        }
        BindingSource bsBaoCao = new BindingSource();
        DataTable dtBaoCao = new DataTable();
        private void LoadData(DateTime? fromDate = null, DateTime? toDate = null)
        {
            string query = @"
        SELECT MaHD, NgayLapHD, ThanhTienHD, ThueVAT, SoTienKhachDua, SoTienDu
        FROM HoaDon
        WHERE (@TuNgay IS NULL OR NgayLapHD >= @TuNgay)
          AND (@DenNgay IS NULL OR NgayLapHD <= @DenNgay)
        ORDER BY NgayLapHD ASC";

            SqlParameter[] parameters =
            {
        new SqlParameter("@TuNgay", (object)fromDate ?? DBNull.Value),
        new SqlParameter("@DenNgay", (object)toDate ?? DBNull.Value)
    };

            dtBaoCao = DatabaseHelper.ExecuteQuery(query, parameters);
            bsBaoCao.DataSource = dtBaoCao;
            dgvBC.DataSource = bsBaoCao;
        }
        private void FrmBaoCao_Load(object sender, EventArgs e)
        {
            LoadData();
            bindingNavigator1.BindingSource = bsBaoCao;
            dgvBC.DefaultCellStyle.ForeColor = Color.Black;
            dgvBC.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            dgvBC.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvBC.Columns["MaHD"].HeaderText = "Mã Hóa Đơn";
            dgvBC.Columns["NgayLapHD"].HeaderText = "Ngày Lập";
            dgvBC.Columns["ThanhTienHD"].HeaderText = "Thành Tiền";
            dgvBC.Columns["ThueVAT"].HeaderText = "Thuế VAT";
            dgvBC.Columns["SoTienKhachDua"].HeaderText = "Số Tiền Khách Đưa";
            dgvBC.Columns["SoTienDu"].HeaderText = "Số Tiền Thừa";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            // Lấy ngày từ 2 DateTimePicker
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date;

            LoadData(fromDate, toDate); // truyền ngày để lọc

            if (bsBaoCao.Count == 0)
                MessageBox.Show("Không tìm thấy dữ liệu trong khoảng ngày này.", "Thông báo");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Dữ liệu đã tải lại!");
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (bsBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo!");
                return;
            }

            // Lấy dữ liệu hiện tại trong DataGridView (đã lọc)
            DataTable dt = ((DataTable)bsBaoCao.DataSource).Copy();

            FrmReportBCDT frm = new FrmReportBCDT(dt);
            frm.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (bsBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để thống kê!");
                return;
            }

            // Lấy dữ liệu hiện tại trong DataGridView (đã lọc)
            DataTable dt = ((DataTable)bsBaoCao.DataSource).Copy();

            FrmReportThongKe frm = new FrmReportThongKe(dt);
            frm.ShowDialog();
        }
    }
}
