using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SincronosXMLFiscal.Util
{
    public static class UtilXml
    {


        public static T DeserializeObject<T>(string fileName)
        {
            
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream fs = new FileStream(fileName, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);

                T i;

                i = (T)serializer.Deserialize(reader);
                fs.Close();

                return i;

            }
            catch (Exception)
            {
                Exception ex = new Exception("Erro no XML");
                throw ex;
            }

         
         
        }


      
    }
}
