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
    public partial class FrmThemMon : Form
    {
        private string _maSP;
        private string _tenSP;
        private decimal _donGia;

        public int SoLuong { get; private set; }
        public string GhiChu { get; private set; }

        public FrmThemMon(string maSP, string tenSP, decimal donGia)
        {
            InitializeComponent();
            _maSP = maSP;
            _tenSP = tenSP;
            _donGia = donGia;
        }

        private void FrmThemMon_Load(object sender, EventArgs e)
        {
            lblTenSP.Text = _tenSP;
            lblDonGia.Text = _donGia.ToString("N0") + " đ";
            nudSL.Value = 1;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            SoLuong = (int)nudSL.Value;
            GhiChu = txtMoTa.Text.Trim();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
