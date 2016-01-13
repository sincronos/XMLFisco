using Microsoft.Reporting.WinForms;
using SincronosXMLFiscal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace SincronosXMLFiscal
{
    public partial class FrmRDLCProcessados : Form
    {
        private ObservableCollection<TNfeProc> ListaNFe;
       
        public FrmRDLCProcessados()
        {
            InitializeComponent();
        }


        public FrmRDLCProcessados(ObservableCollection<TNfeProc> _ListaNFe)
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
           
            DataSetRelatorio.DT_ProcessadosDataTable dt = new DataSetRelatorio.DT_ProcessadosDataTable();


            foreach (var item in ListaNFe)
            {

                dt.AddDT_ProcessadosRow(
                  item.NFe.infNFe.ide.cNF,
                  item.protNFe.infProt.xMotivo,
                  item.NFe.infNFe.Id,
                  Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi),
                  decimal.Parse(item.NFe.infNFe.total.ICMSTot.vNF, new CultureInfo("en-US"))
                  );

            }


            //for (int i = 0; i < ListaNFe.Count; i++)
            //{


            //    dt.AddDT_ProcessadosRow(
            //        ListaNFe[i].NFe.infNFe.ide.cNF,
            //        ListaNFe[i].protNFe.infProt.xMotivo,
            //        ListaNFe[i].NFe.infNFe.Id,
            //        Convert.ToDateTime(ListaNFe[i].NFe.infNFe.ide.dhEmi),
            //        decimal.Parse(ListaNFe[i].NFe.infNFe.total.ICMSTot.vNF, new CultureInfo("en-EN"))
            //        );

            //}

            //BindingSource bs = new BindingSource();

            FrmRDLCProcessados FrmRDLCProcessados = new FrmRDLCProcessados();
            var setup = FrmRDLCProcessados.reportViewer1.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(1, 1, 1, 1);
            FrmRDLCProcessados.reportViewer1.SetPageSettings(setup);

            DT_ProcessadosBindingSource.DataSource = dt;
            ReportDataSource rds = new ReportDataSource("DataSetRelatorio", DT_ProcessadosBindingSource);
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            string caminho = System.Environment.CurrentDirectory + "\\" + @"Relatorios\";
            this.reportViewer1.LocalReport.ReportPath = caminho + "RV_Processados.rdlc";
            this.reportViewer1.RefreshReport();
        }
    }
}
