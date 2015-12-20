using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SincronosXMLFiscal.Model;

namespace SincronosXMLFiscal.BLL
{
    public class EmitenteBLL
    {
        

        public IEnumerable<XElement> GetEmitente(string caminho)
        {


            XDocument doc = XDocument.Load(caminho);

            //List<EmitenteModel>  emitente = 


            return null;

        }

      

    }
}
