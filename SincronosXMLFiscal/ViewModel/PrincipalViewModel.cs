using System.Collections.Generic;
using SincronosXMLFiscal.Model;
using SincronosXMLFiscal.BLL;
using SincronosXMLFiscal.Commands;
using Microsoft.Win32;
using SincronosXMLFiscal.Util;
using SincronosXMLFiscal.Model;
using System.Data;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace SincronosXMLFiscal.ViewModel
{
    public class PrincipalViewModel : MBase
    {

        private string txtCaminhoArquivo;
        private EmitenteBLL EmiteBLL;
        private ObservableCollection<TNfeProc> listaNFE = new ObservableCollection<TNfeProc>();
        private ObservableCollection<XMLNaoProcessadoModel> naoProcessadosCollection = new ObservableCollection<XMLNaoProcessadoModel>();
        private XMLNaoProcessadoModel xmlNaoProcessado = new XMLNaoProcessadoModel();


        string PastaXMLSelecionado;

        public RelayCommand ProcessarCommand { get; set; }
        public RelayCommand CaminhoPastaXMLCommand { get; set; }


        public PrincipalViewModel()
        {
            EmiteBLL = new EmitenteBLL();
            PastaXMLSelecionado = "";
            TxtCaminhoArquivo = "";
            CaminhoPastaXMLCommand = new RelayCommand(CaminhoPastaXML);
            ProcessarCommand = new RelayCommand(Processar,CanProcessar);

        }

        private bool CanProcessar(object obj)
        {
            return !string.IsNullOrEmpty(PastaXMLSelecionado);
        }

        private void Processar(object obj)
        {

            ListaNFE.Clear();
            DirectoryInfo diretorio = new DirectoryInfo(PastaXMLSelecionado);
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");
            string[] files = System.IO.Directory.GetFiles(TxtCaminhoArquivo);

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    ListaNFE.Add(UtilXml.DeserializeObject<TNfeProc>(files[i]));
                }
                catch (System.Exception ex)
                {
                    XmlNaoProcessado.ArquivoNaoProcessado = ex.Data["NaoProcessado"].ToString();
                    NaoProcessados.Add(XmlNaoProcessado);

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
        

        public XMLNaoProcessadoModel XmlNaoProcessado
        {
            get { return xmlNaoProcessado; }
            set { xmlNaoProcessado = value; OnPropertyChanged("XmlNaoProcessado"); }
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



    }
}
