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
using GiaoDienHTQLBH.BUS;
using GiaoDienHTQLBH.Class;
using GiaoDienHTQLBH.FrmSalesperson;
using GiaoDienHTQLBH.Model;

namespace GiaoDienHTQLBH
{
    public partial class FrmBanHang : Form
    {
        private BanBUS banBUS = new BanBUS();
        private SanPhamBUS spBUS = new SanPhamBUS();
        private DonHangBUS dhBUS = new DonHangBUS();
        private DataTable dtChiTietTam;
        private BindingSource bsChiTietTam = new BindingSource();
        private string _currentMaNV = null;
        private string _currentMaBan = null;
        private string _currentMaDH = null;
        private string _lastMaHD;
        public FrmBanHang()
        {
            InitializeComponent();
        }

        private Button CreateTableButton(Ban tb)
        {
            Button bt = new Button
            {
                Text = $"Bàn {tb.SoBan}",
                Name = tb.MaBan,
                Tag = tb,
                Width = 50,
                Height = 50,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            // màu theo trạng thái
            if (tb.TinhTrangBan == "Trống")
            {
                bt.BackColor = Color.LightGreen;
                bt.ForeColor = Color.Black;
            }
            else if (tb.TinhTrangBan == "Đang sử dụng")
            {
                bt.BackColor = Color.OrangeRed;
                bt.ForeColor = Color.White;
            }
            else if (tb.TinhTrangBan == "Đặt trước")
            {
                bt.BackColor = Color.Goldenrod;
                bt.ForeColor = Color.Black;
            }

            bt.Click += TableButton_Click;
            return bt;
        }
        //private DbTable _dbTable = new DbTable();

        private void LoadTablesToFLP()
        {
            flpDSBan.Controls.Clear();
            DataTable dt = banBUS.GetAll();

            foreach (DataRow r in dt.Rows)
            {
                Ban tb = new Ban
                {
                    MaBan = r["MaBan"].ToString(),
                    SoBan = Convert.ToInt32(r["SoBan"]),
                    ViTriBan = r["ViTriBan"].ToString(),
                    TinhTrangBan = r["TinhTrangBan"].ToString()
                };

                Button b = CreateTableButton(tb);
                flpDSBan.Controls.Add(b);
            }
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Ban tb = b.Tag as Ban;

            string oldLabel = lblBan.Text;
            lblBan.Text = $"Bàn {tb.SoBan} - {tb.ViTriBan} - {tb.TinhTrangBan}";

            if (tb.TinhTrangBan == "Đang sử dụng" || tb.TinhTrangBan == "Đặt trước")
            {
                MessageBox.Show("Bàn này hiện đã có người dùng.\nVui lòng chọn bàn khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblBan.Text = oldLabel;
                return;
            }

            ClearOrderForm();
            _currentMaBan = tb.MaBan;
            _currentMaDH = null; // reset đơn hàng

            // Xóa danh sách món cũ
            dtChiTietTam.Rows.Clear();
            bsChiTietTam.ResetBindings(false);
            txtTC.Text = "0";
            txtThanhTien.Text = "0";
            txtTienDua.Text = "0";
            txtTienThua.Text = "0";
            nmrVAT.Value = 0;

            lblBan.Text = $"Bàn {tb.SoBan} - {tb.ViTriBan} - {tb.TinhTrangBan}";
        }
     
        //private DbSanPham _dbSP = new DbSanPham();

        private void LoadSanPham(string loai = "Tất cả")
        {
            flpSP.Controls.Clear();
            DataTable dt = loai == "Tất cả" ? spBUS.GetAll() : spBUS.GetByLoai(loai);

            string projectPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));

            foreach (DataRow r in dt.Rows)
            {
                string maSP = r["MaSP"].ToString();
                string tenSP = r["TenSP"].ToString();
                decimal gia = r["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(r["DonGia"]);
                string relativePath = r["HinhAnh"].ToString();
                string fullPath = Path.Combine(projectPath, relativePath);

                Panel pnl = new Panel
                {
                    Width = 120,
                    Height = 140,
                    Tag = maSP,
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand
                };

                PictureBox pb = new PictureBox
                {
                    Width = 110,
                    Height = 80,
                    Top = 5,
                    Left = 5,
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                if (File.Exists(fullPath)) pb.Image = Image.FromFile(fullPath);

                Label lbl = new Label
                {
                    AutoSize = false,
                    Width = 110,
                    Height = 40,
                    Top = 100,
                    Left = 5,
                    Text = $"{tenSP}\n{gia:N0}đ",
                    TextAlign = ContentAlignment.MiddleCenter
                };

                pnl.Controls.Add(pb);
                pnl.Controls.Add(lbl);

                pnl.Click += SanPham_Click;
                pb.Click += SanPham_Click;
                lbl.Click += SanPham_Click;

                flpSP.Controls.Add(pnl);
            }
        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cbLoai.SelectedItem.ToString();
            LoadSanPham(loai);
        }
        private void LoadLoaiSanPham()
        {
            DataTable dt = spBUS.GetAll(); // lấy tất cả sản phẩm
            cbLoai.Items.Clear();
            cbLoai.Items.Add("Tất cả");

            foreach (DataRow r in dt.Rows)
            {
                string loaiSP = r["LoaiSP"].ToString();
                if (!cbLoai.Items.Contains(loaiSP))
                    cbLoai.Items.Add(loaiSP);
            }

            cbLoai.SelectedIndex = 0;
        }
        private void ClearOrderForm()
        {
            txtTC.Text = "0";
            nmrVAT.Value = 0;
            txtTienDua.Text = "0";
            txtTienThua.Text = "0";
            txtThanhTien.Text = "0";
        }
        private void KhoiTaoChiTietTam()
        {
            dtChiTietTam = new DataTable();
            dtChiTietTam.Columns.Add("MaSP", typeof(string));
            dtChiTietTam.Columns.Add("TenSP", typeof(string));
            dtChiTietTam.Columns.Add("DonGia", typeof(decimal));
            dtChiTietTam.Columns.Add("SoLuong", typeof(int));
            dtChiTietTam.Columns.Add("ThanhTien", typeof(decimal));
            dtChiTietTam.Columns.Add("GhiChu", typeof(string));

            // Gán BindingSource nếu chưa gán
            if (bsChiTietTam.DataSource == null)
                bsChiTietTam.DataSource = dtChiTietTam;
            else
                bsChiTietTam.DataSource = dtChiTietTam;

            bsChiTietTam.ResetBindings(false);
        }
        private void SanPham_Click(object sender, EventArgs e)
        {
            string maSP = null;

            if (sender is Panel pnl)
                maSP = pnl.Tag?.ToString();
            else if (sender is Control ctl && ctl.Parent is Panel parentPnl)
                maSP = parentPnl.Tag?.ToString();

            if (string.IsNullOrEmpty(maSP)) return;

            if (_currentMaBan == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước!");
                return;
            }

            string tenSP = spBUS.GetTenSP(maSP);
            decimal donGia = spBUS.GetDonGia(maSP);

            using (FrmThemMon frm = new FrmThemMon(maSP, tenSP, donGia))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int soLuong = frm.SoLuong;
                    string ghiChu = frm.GhiChu;

                    decimal thanhTienSP = soLuong * donGia;
                    dtChiTietTam.Rows.Add(maSP, tenSP, donGia, soLuong, thanhTienSP, ghiChu);

                    // Gán trực tiếp DataTable (không Copy)
                    dgvDH.DataSource = dtChiTietTam;
                    FormatDgvDH();
                }
            }
        }

        private void FormatDgvDH()
        {
            if (dtChiTietTam == null || dtChiTietTam.Rows.Count == 0)
            {
                dgvDH.DataSource = null;
                txtTC.Text = "0";
                return;
            }

            // Copy dtChiTietTam để hiển thị
            DataTable dtDisplay = dtChiTietTam.Copy();

            // Thêm cột STT
            if (!dtDisplay.Columns.Contains("STT"))
                dtDisplay.Columns.Add("STT", typeof(int));

            decimal tong = 0;
            for (int i = 0; i < dtDisplay.Rows.Count; i++)
            {
                dtDisplay.Rows[i]["STT"] = i + 1;
                tong += Convert.ToDecimal(dtDisplay.Rows[i]["ThanhTien"]);
            }

            // Chọn các cột muốn hiển thị
            DataView dv = dtDisplay.DefaultView;
            dv.Sort = "STT ASC";
            dgvDH.DataSource = dv.ToTable(false, "STT", "TenSP", "DonGia", "SoLuong", "ThanhTien", "GhiChu");

            // Format dgv
            dgvDH.Columns["STT"].HeaderText = "STT";
            dgvDH.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvDH.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvDH.Columns["SoLuong"].HeaderText = "SL";
            dgvDH.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvDH.Columns["GhiChu"].HeaderText = "Ghi chú";

            dgvDH.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDH.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDH.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDH.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDH.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvDH.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            txtTC.Text = tong.ToString("N0"); // Hiển thị tổng
        }
        private void TinhTien()
        {
            decimal tongTien = 0;
            decimal vat = 0;
            decimal tienKhachDua = 0;
            decimal thanhTien = 0;
            decimal tienThua = 0;

            // Lấy tổng tiền hàng từ dgvDH
            if (!string.IsNullOrEmpty(txtTC.Text))
                tongTien = decimal.Parse(txtTC.Text, System.Globalization.NumberStyles.Number);

            // Lấy VAT (%)
            vat = nmrVAT.Value; // decimal

            // Tính thành tiền
            thanhTien = tongTien + (tongTien * vat / 100);
            txtThanhTien.Text = thanhTien.ToString("N0"); // chỉ hiển thị, vẫn là số nguyên/decimal

            // Lấy tiền khách đưa
            if (!string.IsNullOrEmpty(txtTienDua.Text))
                tienKhachDua = decimal.Parse(txtTienDua.Text, System.Globalization.NumberStyles.Number);

            // Tính tiền thừa
            tienThua = tienKhachDua - thanhTien;
            if (tienThua < 0) tienThua = 0;

            txtTienThua.Text = tienThua.ToString("N0");
        }
      
        private void FormSale_Load(object sender, EventArgs e)
        {
            dgvDH.DefaultCellStyle.Font = new Font("Times New Roman", 11);
            dgvDH.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            LoadTablesToFLP();
            LoadLoaiSanPham();
            LoadSanPham();
            KhoiTaoChiTietTam();

            // Gán BindingSource cho DataGridView
            bsChiTietTam.DataSource = dtChiTietTam;
            dgvDH.DataSource = bsChiTietTam;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLoai.SelectedItem == null) return;

                string loai = cbLoai.SelectedItem.ToString();
                LoadSanPham(loai);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc sản phẩm: " + ex.Message);
            }
        }

        private void btnTT_Click(object sender, EventArgs e)
        {
            TinhTien();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            try
            {
                if (_currentMaBan == null || dtChiTietTam.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn bàn và thêm ít nhất 1 sản phẩm!");
                    return;
                }

                // 1. Tạo Đơn Hàng mới
                string maDH = dhBUS.GenerateNewMaDH();
                decimal thanhTienDH = dtChiTietTam.AsEnumerable().Sum(r => Convert.ToDecimal(r["ThanhTien"]));

                DonHang dh = new DonHang
                {
                    MaDH = maDH,
                    MaBan = _currentMaBan,
                    NgayTaoDH = DateTime.Now,
                    ThanhTienDH = thanhTienDH,
                    MaNV = _currentMaNV
                };
                dhBUS.InsertDonHang(dh);

                // 2. Lưu Chi Tiết Đơn Hàng
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    dhBUS.AddChiTiet(maDH,
                                     r["MaSP"].ToString(),
                                     Convert.ToInt32(r["SoLuong"]),
                                     Convert.ToDecimal(r["DonGia"]),
                                     r["GhiChu"].ToString());
                }

                // 3. Lưu Hóa Đơn
                string maHD = "HD" + maDH.Substring(2);
                decimal vat = nmrVAT.Value / 100;
                decimal thanhTienHD = thanhTienDH * (1 + vat);

                decimal tienKhachDua = 0;
                decimal.TryParse(txtTienDua.Text, System.Globalization.NumberStyles.Number, null, out tienKhachDua);

                decimal tienThua = tienKhachDua - thanhTienHD;
                if (tienThua < 0) tienThua = 0;

                HoaDon hd = new HoaDon
                {
                    MaHD = maHD,
                    NgayLapHD = DateTime.Now,
                    ThueVAT = vat,
                    ThanhTienHD = thanhTienHD,
                    SoTienKhachDua = tienKhachDua,
                    SoTienDu = tienThua,
                    MaDH = maDH
                };
                dhBUS.InsertHoaDon(hd);
                _lastMaHD = maHD;

                // 4. Cập nhật trạng thái bàn sang "Đang sử dụng"
                banBUS.UpdateTinhTrang(_currentMaBan, "Đang sử dụng");

                // 5. Thông báo & Reset form
                MessageBox.Show("Đơn hàng và hóa đơn đã lưu thành công!");

                // Xóa DataTable an toàn
                dtChiTietTam.Rows.Clear();
                bsChiTietTam.ResetBindings(false);

                ClearOrderForm();
                _currentMaBan = null;
                _currentMaDH = null;
                lblBan.Text = "Chọn bàn";

                // 6. Load lại bàn với trạng thái mới
                LoadTablesToFLP();
                dgvDH.DataSource = bsChiTietTam;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastMaHD))
            {
                MessageBox.Show("Chưa có hóa đơn để in!");
                return;
            }

            FrmReportInHoaDon frm = new FrmReportInHoaDon(_lastMaHD);
            frm.ShowDialog();
        }
    }
}
