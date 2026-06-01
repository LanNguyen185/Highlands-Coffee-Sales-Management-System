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
    public partial class FrmDonHang : Form
    {
        public FrmDonHang()
        {
            InitializeComponent();
        }
        BindingSource bsDH = new BindingSource();

        private void LoadDH(string where = "")
        {
            string query =
                "SELECT DH.MaDH, DH.NgayTaoDH, DH.HinhThucThanhToan, " +
                "HD.ThueVAT, HD.ThanhTienHD, " +
                "DH.MaNV, DH.MaBan, DH.MaKH " +
                "FROM DonHang DH LEFT JOIN HoaDon HD ON DH.MaDH = HD.MaDH "
                + where;

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsDH.DataSource = dt;
            dgvDH.DataSource = bsDH;

            dgvDH.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);

            SetColumnHeaderText();
        }
        private void SetColumnHeaderText()
        {
            dgvDH.Columns["MaDH"].HeaderText = "Mã đơn hàng";
            dgvDH.Columns["NgayTaoDH"].HeaderText = "Ngày tạo";
            dgvDH.Columns["HinhThucThanhToan"].HeaderText = "Hình thức thanh toán";
            dgvDH.Columns["ThueVAT"].HeaderText = "Thuế VAT";
            dgvDH.Columns["ThanhTienHD"].HeaderText = "Tổng tiền";
            dgvDH.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvDH.Columns["MaBan"].HeaderText = "Mã bàn";
            dgvDH.Columns["MaKH"].HeaderText = "Mã khách hàng";
        }


        private void FormDonHang_Load(object sender, EventArgs e)
        {
            LoadDH();
            bindingNavigator1.BindingSource = bsDH;          
        }
     

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadDH();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string where = "";
            List<SqlParameter> parameters = new List<SqlParameter>();

            string input = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập thông tin cần lọc!");
                return;
            }

            // Lọc theo Mã đơn hàng
            if (rdoMDH.Checked)
            {
                where = " WHERE DH.MaDH LIKE @keyword";
                parameters.Add(new SqlParameter("@keyword", "%" + input + "%"));
            }
            // Lọc theo NGÀY
            else if (rdoNgay.Checked)
            {
                int ngay;
                if (!int.TryParse(input, out ngay) || ngay < 1 || ngay > 31)
                {
                    MessageBox.Show("Ngày không hợp lệ!");
                    return;
                }

                where = " WHERE DAY( DH.NgayTaoDH) = @ngay";
                parameters.Add(new SqlParameter("@ngay", ngay));
            }
            else if (rdoThang.Checked)
            {
                int thang;
                if (!int.TryParse(input, out thang) || thang < 1 || thang > 12)
                {
                    MessageBox.Show("Tháng không hợp lệ!");
                    return;
                }

                where = " WHERE MONTH(DH.NgayTaoDH) = @thang";
                parameters.Add(new SqlParameter("@thang", thang));
            } 
            else if (rdoNam.Checked) 
            {
                int nam;
                if (!int.TryParse(input, out nam) )
                {
                    MessageBox.Show("Năm không hợp lệ!");
                    return;
                }

                where = " WHERE YEAR(DH.NgayTaoDH) = @nam";
                parameters.Add(new SqlParameter("@nam", nam));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chế độ lọc!");
                return;
            }

            try
            {
                string query =
                    "SELECT DH.MaDH, DH.NgayTaoDH, DH.HinhThucThanhToan, " +
                    "HD.ThueVAT, HD.ThanhTienHD, DH.MaNV, DH.MaBan, DH.MaKH " +
                    "FROM DonHang DH LEFT JOIN HoaDon HD ON DH.MaDH = HD.MaDH "
                    + where;

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

                if (dt.Rows.Count > 0)
                {
                    bsDH.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu!");
                    dgvDH.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc: " + ex.Message);
            }
        }
    }
    
}
