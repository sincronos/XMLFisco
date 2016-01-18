using Microsoft.Reporting.WinForms;
using SincronosXMLFiscal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronosXMLFiscal
{
    public partial class FrmRDLCAnalitico : Form
    {

        List<CfopAnaliticoModel> listaCfop;

        public FrmRDLCAnalitico(List<CfopAnaliticoModel> _listaCfop)
        {
            InitializeComponent();

            listaCfop = _listaCfop;

        }

        private void FrmRDLCAnalitico_Load(object sender, EventArgs e)
        {

            CarregaReport();
                  
        }

        private void CarregaReport()
        {

            DataSetRelatorio.DT_AnaliticoDataTable dt = new DataSetRelatorio.DT_AnaliticoDataTable();


            foreach (var item in listaCfop)
            {

                dt.AddDT_AnaliticoRow(item.Aliquota, item.BaseCalculo, item.Cfop, item.Data, item.Isento, item.Outros, item.Serie, item.Tributado);

            }

            DT_AnaliticoBindingSource.DataSource = dt;
            ReportDataSource rds = new ReportDataSource("DataSetAnalitico", DT_AnaliticoBindingSource);

            this.reportViewer1.LocalReport.DataSources.Add(rds);
            string caminho = System.Environment.CurrentDirectory + "\\" + @"Relatorios\";
            this.reportViewer1.LocalReport.ReportPath = caminho + "RV_Analitico.rdlc";
            this.reportViewer1.RefreshReport();

        }
    }
}
