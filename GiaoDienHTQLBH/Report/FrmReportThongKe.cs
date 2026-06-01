using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace GiaoDienHTQLBH
{
    public partial class FrmReportThongKe : Form
    {
        private DataTable data;
        public FrmReportThongKe(DataTable dt)
        {
            InitializeComponent();
            this.data = dt;
        }
        
        private void FrmThongKeDT_Load(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\ReportBCThongKe.rdlc";

                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource = new ReportDataSource(
                    "DataSetThongKeDT",
                    data                       // DataTable truyền từ form
                );

                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
