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
    public partial class FrmMain_Manager : Form
    {       
        public FrmMain_Manager()
        {
            InitializeComponent();

        }
      
        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
                currentFormChild.Close();

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        private void bntNhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNhanVien());
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmKH());
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDonHang());
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSP());
        }

        private void btnCTKM_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCTKM());
        }

        private void btnPNK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmPNK());
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTonKho());
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNCC());
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmBaoCaoDT());
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear(); // Xóa hết các form con
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCLV());
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTTCN1());
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDoiMK());
        }

        private void tạoTàiKhoảnVàPhânQuyềnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTaoTaiKhoan());
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_Manager_Load(object sender, EventArgs e)
        {

        }
    }
}
