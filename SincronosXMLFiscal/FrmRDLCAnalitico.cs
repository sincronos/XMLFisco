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


            var novaLista = listaCfop.GroupBy(x => new { x.Data,x.Serie,x.Cfop, x.Aliquota })
                .Select(y => new CfopAnaliticoModel()
                {
                    Aliquota = y.Key.Aliquota,
                    BaseCalculo = y.Sum(g => Convert.ToDecimal(g.BaseCalculo)),
                    Cfop = y.Key.Cfop,
                    Data = y.Key.Data,
                    Isento = y.Sum(g => Math.Round(Convert.ToDecimal(g.Isento),2)),
                    Outros = y.Sum(g => Math.Round(Convert.ToDecimal(g.Outros),2)),
                    Serie = y.Key.Serie,
                    Tributado = y.Sum(g => Math.Round(Convert.ToDecimal(g.Tributado),2))

                }
                );



            foreach (var item in novaLista)
            {

                dt.AddDT_AnaliticoRow(item.Aliquota, 
                    item.BaseCalculo, 
                    item.Cfop, 
                    Convert.ToDateTime(item.Data).ToShortDateString(),
                    item.Isento, 
                    item.Outros, 
                    item.Serie, 
                    item.Tributado);

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
