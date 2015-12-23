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

namespace SincronosXMLFiscal.ViewModel
{
    public class PrincipalViewModel : MBase
    {

        private string txtCaminhoArquivo;
        private EmitenteBLL EmiteBLL;
        private ObservableCollection<TNfeProc> listaNFE = new ObservableCollection<TNfeProc>();
        private ObservableCollection<XMLNaoProcessadoModel> naoProcessadosCollection = new ObservableCollection<XMLNaoProcessadoModel>();
        //private XMLNaoProcessadoModel xmlNaoProcessado = new XMLNaoProcessadoModel();

        private decimal total = 0;
        //decimal valorTotal = 0;

        string PastaXMLSelecionado;

        public RelayCommand ProcessarCommand { get; set; }
        public RelayCommand CaminhoPastaXMLCommand { get; set; }


        public PrincipalViewModel()
        {
            EmiteBLL = new EmitenteBLL();
            PastaXMLSelecionado = "";
            TxtCaminhoArquivo = "";
            CaminhoPastaXMLCommand = new RelayCommand(CaminhoPastaXML);
            ProcessarCommand = new RelayCommand(Processar, CanProcessar);

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
                        Total += decimal.Parse(ListaNFE[cont].NFe.infNFe.total.ICMSTot.vNF, new CultureInfo("en-EN"));
                    }
                    else
                    {

                    }

                        
                    cont += 1;

                }
                catch
                {
                    //MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }



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


        ////public XMLNaoProcessadoModel XmlNaoProcessado
        ////{
        ////    get { return xmlNaoProcessado; }
        ////    set { xmlNaoProcessado = value; OnPropertyChanged("XmlNaoProcessado"); }
        ////}

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



    }
}
