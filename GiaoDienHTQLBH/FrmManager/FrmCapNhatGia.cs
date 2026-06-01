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
    public partial class FrmCapNhatGia : Form
    {
        public string MaSP { get; set; } // Nhận từ FrmSP
        BindingSource bsGia = new BindingSource();
        public FrmCapNhatGia(string maSP)
        {
            InitializeComponent();
            MaSP = maSP;
        }
        public decimal GiaMoi { get; private set; }
     
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGia.Text.Trim(), out decimal giaMoi))
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!");
                return;
            }

            string maDG = GenerateMaDG();
            string insertQuery = @"INSERT INTO DonGiaSanPham
                                   (MaDG, MaSP, GiaBan, NgayCapNhatGia)
                                   VALUES (@MaDG, @MaSP, @GiaBan, GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDG", maDG),
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@GiaBan", giaMoi)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
                MessageBox.Show("Cập nhật giá thành công!");

                txtGia.Clear();
                txtMaDG.Text = GenerateMaDG();
                LoadLichSuGia(); // Load lại DGV
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm giá: " + ex.Message);
            }
        }

        private void FrmCapNhatGia_Load(object sender, EventArgs e)
        {
            
            txtMSP.Text = MaSP;
            txtMSP.ReadOnly = true;
            txtMaDG.Text = GenerateMaDG();
            LoadLichSuGia();
            bindingNavigator1.BindingSource = bsGia;
        }
        private string GenerateMaDG()
        {
            string query = "SELECT TOP 1 MaDG FROM DonGiaSanPham ORDER BY MaDG DESC";
            object result = DatabaseHelper.ExecuteScalar(query, null);

            if (result == null)
                return "DG001";

            string lastMa = result.ToString();
            int number = int.Parse(lastMa.Substring(2));
            number++;
            return "DG" + number.ToString("D3");
        }
        private void LoadLichSuGia()
        {
            string query = @"SELECT MaDG, MaSP, GiaBan, NgayCapNhatGia
                             FROM DonGiaSanPham
                             ";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            bsGia.DataSource = dt;
            dgvGiaSP.DataSource = bsGia;

            dgvGiaSP.Columns["MaDG"].HeaderText = "Mã giá";
            dgvGiaSP.Columns["MaSP"].HeaderText = "Mã sản phẩm";
            dgvGiaSP.Columns["GiaBan"].HeaderText = "Đơn giá";
            dgvGiaSP.Columns["NgayCapNhatGia"].HeaderText = "Ngày cập nhật";
            dgvGiaSP.Columns["GiaBan"].DefaultCellStyle.Format = "N0"; // định dạng số
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string maSPFilter = txtTimKiem.Text.Trim(); // TextBox nhập mã SP muốn lọc

            string query;
            SqlParameter[] parameters = null;

            if (string.IsNullOrEmpty(maSPFilter))
            {
                // Nếu không nhập gì => hiển thị tất cả
                query = @"SELECT MaDG, MaSP, GiaBan, NgayCapNhatGia
                  FROM DonGiaSanPham
                  ";
            }
            else
            {
                query = @"SELECT MaDG, MaSP, GiaBan, NgayCapNhatGia
                  FROM DonGiaSanPham
                  WHERE MaSP = @MaSP
                  ";

                parameters = new SqlParameter[]
                {
            new SqlParameter("@MaSP", maSPFilter)
                };
            }

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            bsGia.DataSource = dt;
            dgvGiaSP.DataSource = bsGia;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadLichSuGia();
        }
    }
}
