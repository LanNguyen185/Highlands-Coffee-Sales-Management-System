using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienHTQLBH
{
    public partial class FrmTonKho : Form
    {
        public FrmTonKho()
        {
            InitializeComponent();
        }
        BindingSource bsTonKho = new BindingSource();
        BindingSource bsCTPNK = new BindingSource();
        private void FormTonKho_Load(object sender, EventArgs e)
        {
            dgvTK.DefaultCellStyle.Font = new Font("Times New Roman", 9);
            dgvNLNK.DefaultCellStyle.Font = new Font("Times New Roman", 9);
            dgvTK.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvNLNK.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dgvTK.RowHeadersWidth = 22;
            dgvNLNK.RowHeadersWidth = 22;

            dgvTK.DataSource = bsTonKho;
            bindingNavigatorTonKho.BindingSource = bsTonKho;
            dgvNLNK.DataSource = bsCTPNK;
            bindingNavigatorCTPNK.BindingSource = bsCTPNK;

            LoadTonKho();
            LoadChiTietPNK();

            dgvTK.Columns["MaTonKho"].HeaderText = "Mã tồn kho";
            dgvTK.Columns["MaNL"].HeaderText = "Mã nguyên liệu";
            dgvTK.Columns["TenNL"].HeaderText = "Tên nguyên liệu";
            dgvTK.Columns["ThangTK"].HeaderText = "Tháng";
            dgvTK.Columns["NamTK"].HeaderText = "Năm";
            dgvTK.Columns["TonDauKy"].HeaderText = "Tồn ĐK";
            dgvTK.Columns["TriGiaTonDK"].HeaderText = "Trị giá tồn ĐK";
            dgvTK.Columns["NhapTrongKy"].HeaderText = "Nhập TK";
            dgvTK.Columns["TriGiaNhapTK"].HeaderText = "Trị giá nhập TK";
            dgvTK.Columns["XuatTrongKy"].HeaderText = "Xuất TK";
            dgvTK.Columns["TriGiaXuatTK"].HeaderText = "Trị giá xuất TK";
            dgvTK.Columns["TonCuoiKy"].HeaderText = "Tồn CK";
            dgvTK.Columns["TriGiaTonCK"].HeaderText = "Trị giá tồn CK";


            dgvNLNK.Columns["MaPNK"].HeaderText = "Mã phiếu nhập";
            dgvNLNK.Columns["MaNL"].HeaderText = "Mã nguyên liệu";
            dgvNLNK.Columns["TenNL"].HeaderText = "Tên nguyên liệu";
            dgvNLNK.Columns["NgayNhap"].HeaderText = "Ngày nhập";
            dgvNLNK.Columns["SoLuongNK"].HeaderText = "Số lượng nhập";
            dgvNLNK.Columns["DonGiaNK"].HeaderText = "Đơn giá nhập";
        }
        private void LoadTonKho(string where = "")
        {
            string query = @"
                SELECT  
                    tk.MaTonKho,
                    tk.MaNL,
                    nl.TenNL,
                    tk.ThangTK,
                    tk.NamTK,
                    tk.TonDauKy,
                    tk.TriGiaTonDK,
                    tk.NhapTrongKy,
                    tk.TriGiaNhapTK,
                    tk.XuatTrongKy,
                    tk.TriGiaXuatTK,
                    tk.TonCuoiKy,
                    tk.TriGiaTonCK
                FROM TonKho tk
                JOIN NguyenLieu nl ON tk.MaNL = nl.MaNL
                " + where;

            bsTonKho.DataSource = DatabaseHelper.ExecuteQuery(query);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string where = "";

            if (rdoMTonKho.Checked)
                where = " AND tk.MaTonKho LIKE '%" + txtTimKiem.Text.Trim() + "%'";

            else if (rdoThang.Checked)
            {
                if (int.TryParse(txtTimKiem.Text, out int month))
                    where = $" AND tk.ThangTK = {month}";
            }
            else if (rdoNam.Checked)
            {
                if (int.TryParse(txtTimKiem.Text, out int year))
                    where = $" AND tk.NamTK = {year}";
            }

            LoadTonKho(where);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTonKho();
        }
        private void LoadChiTietPNK(string where = "")
        {
            string query = @"
                SELECT 
                    pnk.MaPNK,
                    ctpnk.MaNL,
                    nl.TenNL,
                    pnk.NgayNhap,
                    ctpnk.SoLuongNK,
                    ctpnk.DonGiaNK
                FROM ChiTietPNK ctpnk
                JOIN NguyenLieu nl ON ctpnk.MaNL = nl.MaNL
                JOIN PhieuNhapKho pnk ON ctpnk.MaPNK = pnk.MaPNK
                " + where;

            bsCTPNK.DataSource = DatabaseHelper.ExecuteQuery(query);
        }

        private void btnLoc2_Click(object sender, EventArgs e)
        {
            string where = "";

            string value = txtLoc.Text.Trim();

            if (rdoMNL.Checked)
                where = $" AND ctpnk.MaNL LIKE '%{value}%'";

            else if (rdoThang2.Checked)
            {
                if (int.TryParse(value, out int m))
                    where = $" AND MONTH(pnk.NgayNhap) = {m}";
            }
            else if (rdoNam2.Checked)
            {
                if (int.TryParse(value, out int y))
                    where = $" AND YEAR(pnk.NgayNhap) = {y}";
            }
            else if (rdoNgay2.Checked)
            {
                if (int.TryParse(value, out int d))
                    where = $" AND DAY(pnk.NgayNhap) = {d}";
            }

            LoadChiTietPNK(where);
        }

        private void btnDS_Click(object sender, EventArgs e)
        {
            LoadChiTietPNK();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }
    }
}
