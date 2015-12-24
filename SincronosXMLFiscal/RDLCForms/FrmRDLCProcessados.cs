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
using SincronosXMLFiscal.Model;
using System.Globalization;

namespace SincronosXMLFiscal.RDLCForms
{
    public partial class FrmRDLCProcessados : Form
    {

        private IList<TNfeProc> ListaNFe;


        public FrmRDLCProcessados()
        {
            InitializeComponent();
        }


        public FrmRDLCProcessados(IList<TNfeProc> _ListaNFe)
        {
            InitializeComponent();
            ListaNFe = _ListaNFe;
            
        }

        private void FrmRDLCProcessados_Load(object sender, EventArgs e)
        {
            CarregaReport();
           
        }

        private void CarregaReport()
        {
            this.reportViewer1.Reset();
            DS_XML.DT_ProcessadosDataTable dt = new DS_XML.DT_ProcessadosDataTable();

            for (int i = 0; i < ListaNFe.Count; i++)
            {

                dt.AddDT_ProcessadosRow(
                    ListaNFe[i].NFe.infNFe.ide.cNF, 
                    ListaNFe[i].protNFe.infProt.xMotivo, 
                    ListaNFe[i].NFe.infNFe.Id, 
                    ListaNFe[i].NFe.infNFe.ide.dhEmi,
                    decimal.Parse(ListaNFe[i].NFe.infNFe.total.ICMSTot.vNF, new CultureInfo("en-EN"))
                    ); 

            }

            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            //DataSet ds = new DS_XML();
            //DataTable dt = ds.Tables["DT_Processados"];
            ReportDataSource rds = new ReportDataSource("DataSet1", bs);
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SincronosXMLFiscal.Reports.RV_Processados.rdlc";

            this.reportViewer1.RefreshReport();
        }
    }
}
