using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SincronosXMLFiscal.Datasets;
using Microsoft.Reporting.WinForms;

namespace SincronosXMLFiscal.RDLCForms
{
    public partial class FrmRDLCProcessados : Form
    {
        public FrmRDLCProcessados()
        {
            InitializeComponent();
        }

        private void FrmRDLCProcessados_Load(object sender, EventArgs e)
        {

            this.reportViewer1.Reset();
            DataSet ds = new DS_XML();
            DataTable dt = ds.Tables["DT_Processados"];
            ReportDataSource rds = new ReportDataSource("DataSet1",dt);
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SincronosXMLFiscal.Reports.RV_Processados.rdlc";

            this.reportViewer1.RefreshReport();
        }
    }
}
