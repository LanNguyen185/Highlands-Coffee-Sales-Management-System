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

namespace GiaoDienHTQLBH.FrmSalesperson
{
    public partial class FrmCongThuc : Form
    {
        public FrmCongThuc()
        {
            InitializeComponent();
        }
        BindingSource bsCT = new BindingSource();
        private void LoadCongThuc(string where = "", SqlParameter[] parameters = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        sp.MaSP AS [Mã sản phẩm],
                        sp.TenSP AS [Tên sản phẩm],
                        nl.TenNL AS [Tên nguyên liệu],
                        dm.SoLuongSD AS [Số lượng sử dụng]
                    FROM DinhMuc dm
                    JOIN SanPham sp ON sp.MaSP = dm.MaSP
                    JOIN NguyenLieu nl ON nl.MaNL = dm.MaNL
                    WHERE sp.IsActive = 1
                    " + where + @"
                    ORDER BY sp.TenSP, nl.TenNL";

                List<SqlParameter> list = new List<SqlParameter>();

                if (parameters != null)
                    list.AddRange(parameters);

                DataTable dt = DatabaseHelper.ExecuteQuery(query, list.ToArray());

                bsCT.DataSource = dt;
                dgvDM.DataSource = bsCT;
                bindingNavigator1.BindingSource = bsCT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải công thức: " + ex.Message);
            }
        }
        private void FrmCongThuc_Load(object sender, EventArgs e)
        {
            LoadCongThuc();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm cần lọc!");
                return;
            }

            string where = " AND sp.TenSP LIKE @TenSP";
            SqlParameter[] param =
            {
                new SqlParameter("@TenSP", "%" + input + "%")
            };

            LoadCongThuc(where, param);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadCongThuc();
        }
    }
}
