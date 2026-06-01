using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace GiaoDienHTQLBH
{
    public partial class FrmSP : Form
    {
        public FrmSP()
        {
            InitializeComponent();
        }
        BindingSource bsSP = new BindingSource();
        private void LoadSanPham(string keyword = "")
        {
            string query = @"SELECT sp.MaSP, sp.TenSP, sp.MoTaSP, sp.DonViTinhSP, sp.LoaiSP,
                        dg.GiaBan, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN (
                    SELECT dg1.MaSP, dg1.GiaBan 
                    FROM DonGiaSanPham dg1 
                    INNER JOIN (
                        SELECT MaSP, MAX(NgayCapNhatGia) AS NgayMoiNhat 
                        FROM DonGiaSanPham 
                        GROUP BY MaSP
                    ) dg2 
                    ON dg1.MaSP = dg2.MaSP AND dg1.NgayCapNhatGia = dg2.NgayMoiNhat
                ) dg ON sp.MaSP = dg.MaSP
                WHERE sp.IsActive = 1";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsSP.DataSource = dt;
            dgvSP.DataSource = bsSP;

            dgvSP.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);

            dgvSP.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSP.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvSP.Columns["MoTaSP"].HeaderText = "Mô tả";
            dgvSP.Columns["DonViTinhSP"].HeaderText = "Đơn vị tính";
            dgvSP.Columns["LoaiSP"].HeaderText = "Loại sản phẩm";
            dgvSP.Columns["GiaBan"].HeaderText = "Đơn giá";
            dgvSP.Columns["GiaBan"].DefaultCellStyle.Format = "N0"; // định dạng số
        }
       

        private void FormSP_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            bindingNavigator1.BindingSource = bsSP;
            AddBindings();
            txtMaSP.ReadOnly = true;
            dgvSP.Columns["HinhAnh"].Visible = false; // Ẩn cột HINHANH
            bsSP.PositionChanged += BsSP_PositionChanged;
            LoadImageFromBinding();
        }
      
        private void AddBindings()
        {
            ClearBindings();
            txtMaSP.DataBindings.Add("Text", bsSP, "MaSP", true, DataSourceUpdateMode.Never);
            txtTenSP.DataBindings.Add("Text", bsSP, "TenSP", true, DataSourceUpdateMode.Never);
            txtMoTa.DataBindings.Add("Text", bsSP, "MoTaSP", true, DataSourceUpdateMode.Never);
            txtDVT.DataBindings.Add("Text", bsSP, "DonViTinhSP", true, DataSourceUpdateMode.Never);
            txtLoaiSP.DataBindings.Add("Text", bsSP, "LoaiSP", true, DataSourceUpdateMode.Never);
            picSP.DataBindings.Add("Tag", bsSP, "HinhAnh", true, DataSourceUpdateMode.Never);

        }
        private void ClearBindings()
        {
            txtMaSP.DataBindings.Clear();
            txtTenSP.DataBindings.Clear();
            txtMoTa.DataBindings.Clear();
            txtDVT.DataBindings.Clear();
            txtLoaiSP.DataBindings.Clear();
            picSP.DataBindings.Clear();
        }
        private void LoadImageFromBinding()
        {
            try
            {
                string relativePath = picSP.Tag?.ToString();

                if (string.IsNullOrEmpty(relativePath))
                {
                    picSP.Image = null;
                    return;
                }

                // Lấy thư mục project
                string projectPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
                string fullPath = Path.Combine(projectPath, relativePath);

                if (File.Exists(fullPath))
                {
                    picSP.Image = Image.FromFile(fullPath);
                }
                else
                {
                    picSP.Image = null;
                }
            }
            catch
            {
                picSP.Image = null;
            }
        }
        private void BsSP_PositionChanged(object sender, EventArgs e)
        {
            LoadImageFromBinding();
        }
        private string GenerateMaSP()
        {
            string query = "SELECT TOP 1 MaSP FROM SanPham ORDER BY MaSP DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "SP001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2));
            number++;

            return "SP" + number.ToString("D3");
        }
        private SqlParameter[] GetSanPhamParameters()
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSP", txtMaSP.Text.Trim()),
                new SqlParameter("@TenSP", txtTenSP.Text.Trim()),
                new SqlParameter("@MoTa", (object)txtMoTa.Text ?? DBNull.Value),
                new SqlParameter("@DVT", (object)txtDVT.Text ?? DBNull.Value),
                new SqlParameter("@Loai", (object)txtLoaiSP.Text ?? DBNull.Value)
            };
        }
       
 
        private void button2_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT TOP 10 
            sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuongBan
        FROM 
            ChiTietDonHang ct
        JOIN 
            SANPHAM sp ON ct.MaSP = sp.MaSP
        GROUP BY 
            sp.MaSP, sp.TenSP
        ORDER BY 
            TongSoLuongBan DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query); 
            dgvSP.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để cập nhật giá!");
                return;
            }

            // Lấy mã sản phẩm từ dòng đang chọn
            string maSP = dgvSP.SelectedRows[0].Cells["MaSP"].Value.ToString();

            // Mở form cập nhật giá và truyền mã sản phẩm
            using (FrmCapNhatGia frm = new FrmCapNhatGia(maSP))
            {
                frm.ShowDialog();
            }

            // Sau khi đóng form cập nhật giá, load lại danh sách sản phẩm để hiển thị giá mới nhất
            LoadSanPham();
        }

      

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }

            txtMaSP.Text = GenerateMaSP();

            string query = @"INSERT INTO SanPham 
                             (MaSP, TenSP, MoTaSP, DonViTinhSP, LoaiSP, IsActive)
                             VALUES (@MaSP, @TenSP, @MoTa, @DVT, @Loai, 1)";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetSanPhamParameters());
                MessageBox.Show("Thêm sản phẩm thành công!");
                LoadSanPham();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sản phẩm: " + ex.Message);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }

            string query = @"UPDATE SanPham
                             SET TenSP=@TenSP, MoTaSP=@MoTa, DonViTinhSP=@DVT, LoaiSP=@Loai
                             WHERE MaSP=@MaSP";

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, GetSanPhamParameters());
                MessageBox.Show("Cập nhật thành công!");
                LoadSanPham();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa SP {txtMaSP.Text}?", "Xác nhận", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                string query = "UPDATE SanPham SET IsActive=0 WHERE MaSP=@MaSP";

                try
                {
                    DatabaseHelper.ExecuteNonQuery(query, new SqlParameter[] {
                        new SqlParameter("@MaSP", txtMaSP.Text)
                    });
                    MessageBox.Show("Xóa thành công!");
                    LoadSanPham();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sản phẩm: " + ex.Message);
                }
            }
        }
        private void ClearFields()
        {
            txtTenSP.Clear();
            txtMoTa.Clear();
            txtDVT.Clear();
            txtLoaiSP.Clear();
            txtMaSP.Text = GenerateMaSP();
            txtTenSP.Focus();
        }
        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSP.Rows[e.RowIndex];

            txtMaSP.Text = row.Cells["MaSP"].Value?.ToString();
            txtTenSP.Text = row.Cells["TenSP"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTaSP"].Value?.ToString();
            txtDVT.Text = row.Cells["DonViTinhSP"].Value?.ToString();
            txtLoaiSP.Text = row.Cells["LoaiSP"].Value?.ToString();

            string relativePath = row.Cells["HinhAnh"].Value?.ToString();

            if (!string.IsNullOrEmpty(relativePath))
            {
                // Lấy đường dẫn root project (lùi 2 thư mục từ bin\Debug)
                string projectPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
                string fullPath = Path.Combine(projectPath, relativePath);

                if (File.Exists(fullPath))
                {
                    picSP.Image = Image.FromFile(fullPath);
                }
                else
                {
                    picSP.Image = null;
                    MessageBox.Show("Không tìm thấy file ảnh: " + fullPath);
                }
            }
            else
            {
                picSP.Image = null;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadSanPham();
            ClearFields();
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

            if (rdoMSP.Checked)
            {
                query = @"SELECT sp.MaSP, sp.TenSP, sp.MoTaSP, sp.DonViTinhSP, sp.LoaiSP,
                        dg.GiaBan, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN (
                    SELECT dg1.MaSP, dg1.GiaBan 
                    FROM DonGiaSanPham dg1 
                    INNER JOIN (
                        SELECT MaSP, MAX(NgayCapNhatGia) AS NgayMoiNhat 
                        FROM DonGiaSanPham 
                        GROUP BY MaSP
                    ) dg2 
                    ON dg1.MaSP = dg2.MaSP AND dg1.NgayCapNhatGia = dg2.NgayMoiNhat
                ) dg ON sp.MaSP = dg.MaSP 
                  WHERE sp.IsActive = 1 AND sp.MaSP LIKE @keyword";
            }
            else if (rdoTenSP.Checked)
            {
                query = @"SELECT sp.MaSP, sp.TenSP, sp.MoTaSP, sp.DonViTinhSP, sp.LoaiSP,
                        dg.GiaBan, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN (
                    SELECT dg1.MaSP, dg1.GiaBan 
                    FROM DonGiaSanPham dg1 
                    INNER JOIN (
                        SELECT MaSP, MAX(NgayCapNhatGia) AS NgayMoiNhat 
                        FROM DonGiaSanPham 
                        GROUP BY MaSP
                    ) dg2 
                    ON dg1.MaSP = dg2.MaSP AND dg1.NgayCapNhatGia = dg2.NgayMoiNhat
                ) dg ON sp.MaSP = dg.MaSP 
                  WHERE sp.IsActive = 1 AND sp.TenSP LIKE @keyword";
            }
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
                    bsSP.DataSource = dt;
                    AddBindings();              // quan trọng: binding lại textbox + ảnh
                    LoadImageFromBinding();     // load hình SP đầu tiên
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!");
                    bsSP.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}
