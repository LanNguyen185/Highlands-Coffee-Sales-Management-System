using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiaoDienHTQLBH.BUS;
using Microsoft.Reporting.WinForms;

namespace GiaoDienHTQLBH.FrmSalesperson
{
    public partial class FrmReportInHoaDon : Form
    {
        private string _maHD;
        private DonHangBUS dhBUS = new DonHangBUS();
        public FrmReportInHoaDon(string maHD)
        {
            InitializeComponent();
            _maHD = maHD;
        }

        private void FrmReportInHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtFull = dhBUS.GetHoaDonFull(_maHD);

                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\ReportHoaDon.rdlc";

                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource = new ReportDataSource( "HoaDonFull", dtFull);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load báo cáo: " + ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
