using System.Collections.Generic;
using SincronosXMLFiscal.Model;
using SincronosXMLFiscal.BLL;
using SincronosXMLFiscal.Commands;
using Microsoft.Win32;
using SincronosXMLFiscal.Util;
using System.Data;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System;
using System.Globalization;
using System.Xml;
using System.Linq;

namespace SincronosXMLFiscal.ViewModel
{
    public class PrincipalViewModel : MBase
    {

        private string txtCaminhoArquivo;
        private EmitenteBLL EmiteBLL;
        FileInfo fileInfo;
        CultureInfo usCulture = new CultureInfo("en-US");
        NumberFormatInfo dbNumberFormat;
        

        private ObservableCollection<TNfeProc> listaNFE = new ObservableCollection<TNfeProc>();
        private ObservableCollection<TProcEvento> listaEventoNFe = new ObservableCollection<TProcEvento>();

        private ObservableCollection<XMLNaoProcessadoModel> naoProcessadosCollection = new ObservableCollection<XMLNaoProcessadoModel>();
        //private XMLNaoProcessadoModel xmlNaoProcessado = new XMLNaoProcessadoModel();

        private decimal total = 0;
        //decimal valorTotal = 0;

        string PastaXMLSelecionado;

        public RelayCommand LoadRDLCFormProcessadosCommand { get; set; }
        public RelayCommand ProcessarCommand { get; set; }
        public RelayCommand CaminhoPastaXMLCommand { get; set; }


        public PrincipalViewModel()
        {
            EmiteBLL = new EmitenteBLL();
            PastaXMLSelecionado = "";
            TxtCaminhoArquivo = "";


            CaminhoPastaXMLCommand = new RelayCommand(CaminhoPastaXML);
            ProcessarCommand = new RelayCommand(Processar, CanProcessar);
            LoadRDLCFormProcessadosCommand = new RelayCommand(LoadRDLCFormProcessados);

            dbNumberFormat = usCulture.NumberFormat;
           
        }

        private void LoadRDLCFormProcessados(object obj)
        {
            new FrmRDLCProcessados(ListaNFE).ShowDialog();
        }

        private bool CanProcessar(object obj)
        {
            return !string.IsNullOrEmpty(PastaXMLSelecionado);
        }

        private void Processar(object obj)
        {

            ListaNFE.Clear();
            NaoProcessados.Clear();
            DirectoryInfo diretorio = new DirectoryInfo(PastaXMLSelecionado);
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");
            string[] files = System.IO.Directory.GetFiles(TxtCaminhoArquivo);
            
            int cont = 0;
            Total = 0;

            foreach (FileInfo File in Arquivos)
            {


                try
                {

                    string nameFile = File.ToString();
                    string naoProcessado = "";

                    if (nameFile.Contains("-caneve.xml"))
                    {
                        naoProcessado = nameFile.ToString();
                        NaoProcessados.Add
                        (
                            new XMLNaoProcessadoModel() { ArquivoNaoProcessado = naoProcessado }
                        );
                    }
                    else if (nameFile.Contains("-nfce.xml"))
                    {
                        ListaNFE.Add(UtilXml.DeserializeObject<TNfeProc>(File.FullName));
                        Total += decimal.Parse(ListaNFE[cont].NFe.infNFe.total.ICMSTot.vNF, dbNumberFormat);

                        cont += 1;

                    }
                    else
                    {

                    }


                }
                catch
                {  }

            }

            List<CfopAnaliticoModel> listaCfop = new List<CfopAnaliticoModel>();

            foreach (var nota in ListaNFE)
            {
                foreach (var item in nota.NFe.infNFe.det)
                {
                    var i = new CfopAnaliticoModel();

                    i.Data = nota.NFe.infNFe.ide.dhEmi;
                    i.Serie = nota.NFe.infNFe.ide.serie;
                    i.Cfop = item.prod.CFOP.ToString().Substring(4);

                    if (item.imposto.Items[0].GetType() == typeof(TNFeInfNFeDetImpostoICMS))
                    {

                        #region TRIBUTADO
                        
                        TNFeInfNFeDetImpostoICMS ICMS = (TNFeInfNFeDetImpostoICMS)item.imposto.Items[0];
                        var tipoICMS = ICMS.Item;

                        var x = tipoICMS.GetType();

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS00))
                        {
                            TNFeInfNFeDetImpostoICMSICMS00 oICMS = (TNFeInfNFeDetImpostoICMSICMS00)tipoICMS;
                            i.Aliquota = oICMS.pICMS;
                            i.BaseCalculo = decimal.Parse(oICMS.vBC,dbNumberFormat);
                            i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom,dbNumberFormat);
                        }

                        //if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN101))
                        //{
                        //    TNFeInfNFeDetImpostoICMSICMSSN101 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN101)tipoICMS;
                        //    i.Aliquota = oICMS.pICMS;
                        //    i.BaseCalculo = decimal.Parse(oICMS.vBC, dbNumberFormat);
                        //    i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        //}

                        //if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN102))
                        //{
                        //    TNFeInfNFeDetImpostoICMSICMSSN102 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN102)tipoICMS;
                        //    i.Aliquota = oICMS.pICMS;
                        //    i.BaseCalculo = decimal.Parse(oICMS.vBC, dbNumberFormat);
                        //    i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        //}

                        //if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN201))
                        //{
                        //    TNFeInfNFeDetImpostoICMSICMSSN201 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN201)tipoICMS;
                        //    i.Aliquota = oICMS.pICMS;
                        //    i.BaseCalculo = decimal.Parse(oICMS.vBC, dbNumberFormat);
                        //    i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        //}

                        //if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN202))
                        //{
                        //    TNFeInfNFeDetImpostoICMSICMSSN202 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN202)tipoICMS;
                        //    i.Aliquota = oICMS.pICMS;
                        //    i.BaseCalculo = decimal.Parse(oICMS.vBC, dbNumberFormat);
                        //    i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        //}

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS51))
                        {
                            TNFeInfNFeDetImpostoICMSICMS51 oICMS = (TNFeInfNFeDetImpostoICMSICMS51)tipoICMS;
                            i.Aliquota = oICMS.pICMS;
                            i.BaseCalculo = decimal.Parse(oICMS.vBC, dbNumberFormat);
                            i.Tributado = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        #endregion

                        #region OUTROS
                        

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS10))
                        {
                            TNFeInfNFeDetImpostoICMSICMS10 oICMS = (TNFeInfNFeDetImpostoICMSICMS10)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS30))
                        {
                            TNFeInfNFeDetImpostoICMSICMS30 oICMS = (TNFeInfNFeDetImpostoICMSICMS30)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS60))
                        {
                            TNFeInfNFeDetImpostoICMSICMS60 oICMS = (TNFeInfNFeDetImpostoICMSICMS60)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS70))
                        {
                            TNFeInfNFeDetImpostoICMSICMS70 oICMS = (TNFeInfNFeDetImpostoICMSICMS70)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN500))
                        {
                            TNFeInfNFeDetImpostoICMSICMSSN500 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN500)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS90))
                        {
                            TNFeInfNFeDetImpostoICMSICMS90 oICMS = (TNFeInfNFeDetImpostoICMSICMS90)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Outros = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        #endregion

                        #region ISENTO
                        
                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMS40))
                        {
                            TNFeInfNFeDetImpostoICMSICMS40 oICMS = (TNFeInfNFeDetImpostoICMSICMS40)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Isento = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        if (x == typeof(TNFeInfNFeDetImpostoICMSICMSSN900))
                        {
                            TNFeInfNFeDetImpostoICMSICMSSN900 oICMS = (TNFeInfNFeDetImpostoICMSICMSSN900)tipoICMS;
                            i.Aliquota = "0.0";
                            i.BaseCalculo = 0;
                            i.Isento = decimal.Parse(item.prod.vProd, dbNumberFormat) * decimal.Parse(item.prod.qCom, dbNumberFormat);
                        }

                        #endregion
                    }
                    
        //[System.Xml.Serialization.XmlElementAttribute("ICMS20", typeof(TNFeInfNFeDetImpostoICMSICMS20))]
        //[System.Xml.Serialization.XmlElementAttribute("ICMSPart", typeof(TNFeInfNFeDetImpostoICMSICMSPart))]
        //[System.Xml.Serialization.XmlElementAttribute("ICMSST", typeof(TNFeInfNFeDetImpostoICMSICMSST))]

                    listaCfop.Add(i);

                }
                
            }

                    //List<TNfeProc> ListaCFOP = new List<TNfeProc>(ListaNFE);

                    //var resultDetNFe = ListaCFOP.SelectMany(x => x.NFe.infNFe.det);

                    

                   

            
        }


        private void CaminhoPastaXML(object obj)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            PastaXMLSelecionado = fbd.SelectedPath.ToString();

            TxtCaminhoArquivo = PastaXMLSelecionado;

        }


        public decimal Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged("Total"); }
        }


        public string TxtCaminhoArquivo
        {
            get { return txtCaminhoArquivo; }
            set { txtCaminhoArquivo = value; OnPropertyChanged("TxtCaminhoArquivo"); }
        }



        public ObservableCollection<XMLNaoProcessadoModel> NaoProcessados
        {
            get { return naoProcessadosCollection; }
            set { naoProcessadosCollection = value; OnPropertyChanged("NaoProcessados"); }
        }


        public ObservableCollection<TNfeProc> ListaNFE
        {
            get { return listaNFE; }
            set { listaNFE = value; OnPropertyChanged("ListaNFE"); }
        }

        public ObservableCollection<TProcEvento> ListaEventoNFe
        {
            get { return listaEventoNFe; }
            set { listaEventoNFe = value; OnPropertyChanged("ListaEventoNFe"); }
        }

    }
}
