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
    public partial class FrmLLVstaff : Form
    {
        public FrmLLVstaff()
        {
            InitializeComponent();
        }
        BindingSource bsLLV = new BindingSource();
        private void LoadLichLamViec(string where = "", SqlParameter[] parameters = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        c.MaCa AS [Mã Ca], 
                        c.NgayLam AS [Ngày Làm], 
                        c.ThoiGianBD AS [Giờ Bắt Đầu], 
                        c.ThoiGianKT AS [Giờ Kết Thúc], 
                        c.Ca AS [Ca]
                    FROM CaLamViec c
                    JOIN Ca_DangKy p ON c.MaCa = p.MaCa
                    JOIN NhanVien nv ON nv.MaNV = p.MaNV
                    WHERE nv.MaTK = @MaTK  
                          AND c.IsActive = 1
                    " + where + @"
                    ORDER BY c.NgayLam, c.ThoiGianBD";

                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@MaTK", CurrentUser.MaTK));

                if (parameters != null)
                    list.AddRange(parameters);

                DataTable dt = DatabaseHelper.ExecuteQuery(query, list.ToArray());

                // Binding
                bsLLV.DataSource = dt;
                dgvLLV.DataSource = bsLLV;
                bindingNavigator1.BindingSource = bsLLV;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch làm việc: " + ex.Message);
            }
        }
        private void FormLLVstaff_Load(object sender, EventArgs e)
        {
            LoadLichLamViec();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu cần lọc!");
                return;
            }

            string where = "";
            List<SqlParameter> parameters = new List<SqlParameter>();

            // --- Lọc ngày ---
            if (rdoNgay.Checked)
            {
                if (!int.TryParse(input, out int ngay) || ngay < 1 || ngay > 31)
                {
                    MessageBox.Show("Ngày không hợp lệ!");
                    return;
                }

                where = " AND DAY(c.NgayLam) = @ngay";
                parameters.Add(new SqlParameter("@ngay", ngay));
            }
            // --- Lọc tháng ---
            else if (rdoThang.Checked)
            {
                if (!int.TryParse(input, out int thang) || thang < 1 || thang > 12)
                {
                    MessageBox.Show("Tháng không hợp lệ!");
                    return;
                }

                where = " AND MONTH(c.NgayLam) = @thang";
                parameters.Add(new SqlParameter("@thang", thang));
            }
            // --- Lọc năm ---
            else if (rdoNam.Checked)
            {
                if (!int.TryParse(input, out int nam) || nam < 1900 || nam > 2100)
                {
                    MessageBox.Show("Năm không hợp lệ!");
                    return;
                }

                where = " AND YEAR(c.NgayLam) = @nam";
                parameters.Add(new SqlParameter("@nam", nam));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chế độ lọc!");
                return;
            }

            LoadLichLamViec(where, parameters.ToArray());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadLichLamViec();
        }
    }
}
