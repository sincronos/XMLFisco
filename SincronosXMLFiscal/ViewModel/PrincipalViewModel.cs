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

namespace SincronosXMLFiscal.ViewModel
{
    public class PrincipalViewModel : MBase
    {

        private string txtCaminhoArquivo;
    
        private EmitenteBLL EmiteBLL;
        private List<TNfeProc> listaNFE = new List<TNfeProc>();
        private List<TNFeInfNFeDetProd> listaProd = new List<TNFeInfNFeDetProd>();
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


            DirectoryInfo diretorio = new DirectoryInfo(PastaXMLSelecionado);

            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            string[] files = System.IO.Directory.GetFiles(TxtCaminhoArquivo);

            for (int i = 0; i < files.Length; i++)
            {
                ListaNFE.Add(UtilXml.DeserializeObject<TNfeProc>(files[i]));    
            }
         

        }

        private void CaminhoPastaXML(object obj)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            PastaXMLSelecionado = fbd.SelectedPath.ToString();

            TxtCaminhoArquivo = PastaXMLSelecionado;

            //dlg.InitialDirectory = "C:\\";
            //dlg.RestoreDirectory = true;

            //if (dlg.ShowDialog() == true)
            //{
            //    PastaXMLSelecionado = dlg.FileName;
            //    TxtCaminhoArquivo = PastaXMLSelecionado;
            //}
             
        }


        public string TxtCaminhoArquivo
        {
            get { return txtCaminhoArquivo; }
            set { txtCaminhoArquivo = value; OnPropertyChanged("TxtCaminhoArquivo"); }
        }

        public List<TNfeProc> ListaNFE
        {
            get { return listaNFE; }
            set { listaNFE = value; OnPropertyChanged("ListaNFE"); }
        }


        public List<TNFeInfNFeDetProd> ListaProd
        {
            get { return listaProd; }
            set { listaProd = value; OnPropertyChanged("ListaProd"); }
        }




    }
}
