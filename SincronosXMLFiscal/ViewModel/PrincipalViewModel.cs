using System.Collections.Generic;
using SincronosXMLFiscal.Model;
using SincronosXMLFiscal.BLL;
using SincronosXMLFiscal.Commands;
using Microsoft.Win32;
using SincronosXMLFiscal.Util;

namespace SincronosXMLFiscal.ViewModel
{
    public class PrincipalViewModel : MBase
    {

        private string txtCaminhoArquivo;

        private EmitenteBLL EmiteBLL;
        private List<TNfeProc> listaNFE = new List<TNfeProc>();
        string ArquivoXMLSelecionado;


        public RelayCommand ProcessarCommand { get; set; }
        public RelayCommand CaminhoPastaXMLCommand { get; set; }


        public PrincipalViewModel()
        {
            EmiteBLL = new EmitenteBLL();
            ArquivoXMLSelecionado = "";
            TxtCaminhoArquivo = "";
            CaminhoPastaXMLCommand = new RelayCommand(CaminhoPastaXML);
            ProcessarCommand = new RelayCommand(Processar,CanProcessar);

        }

        private bool CanProcessar(object obj)
        {
            return !string.IsNullOrEmpty(ArquivoXMLSelecionado);
        }

        private void Processar(object obj)
        {
            ListaNFE.Add(UtilXml.DeserializeObject<TNfeProc>(ArquivoXMLSelecionado));
        }

        private void CaminhoPastaXML(object obj)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "C:\\";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                ArquivoXMLSelecionado = dlg.FileName;
                TxtCaminhoArquivo = ArquivoXMLSelecionado;
            }
             
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

        


    }
}
