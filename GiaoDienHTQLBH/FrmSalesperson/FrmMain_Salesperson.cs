using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiaoDienHTQLBH.FrmSalesperson;

namespace GiaoDienHTQLBH
{
    public partial class FrmMain_Salesperson : Form
    {
        public FrmMain_Salesperson()
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
        private void mnBH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmBanHang());
        }

        private void mnTK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTonKho());
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnDH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmDonHang());
        }

        private void mnKH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmKH());
        }

      
     
        private void mnLLV_Click(object sender, EventArgs e)
        {

            OpenChildForm(new FrmLLVstaff());
        }

        private void DMKToolStripMenuItem_Click(object sender, EventArgs e)
        {
             OpenChildForm(new FrmDoiMK());
        }

        private void TTCNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTTCN1());
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
        }

        private void côngThứcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCongThuc());
        }
    }
}
