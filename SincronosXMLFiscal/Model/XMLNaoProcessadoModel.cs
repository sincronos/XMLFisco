using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronosXMLFiscal.Model
{
    public class XMLNaoProcessadoModel : MBase
    {

        private string arquivoNaoProcessado;
       public string ArquivoNaoProcessado 
       {
           get { return arquivoNaoProcessado; }
           set { arquivoNaoProcessado = value; OnPropertyChanged("ArquivoNaoProcessado"); }
       }


    }
}
