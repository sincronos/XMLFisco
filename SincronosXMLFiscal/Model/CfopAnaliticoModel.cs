using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronosXMLFiscal.Model
{
    public class CfopAnaliticoModel
    {

        public string Data { get; set; }
        public string Serie { get; set; }
        public string Cfop { get; set; }
        public decimal BaseCalculo { get; set; }
        public string Aliquota { get; set; }
        public decimal Tributado { get; set; }
        public decimal Isento { get; set; }
        public decimal Outros { get; set; }


    }
}
