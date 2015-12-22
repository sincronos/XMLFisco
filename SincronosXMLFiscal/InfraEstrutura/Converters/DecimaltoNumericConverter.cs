using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SincronosXMLFiscal.InfraEstrutura.Converters
{

    [ValueConversion(typeof(decimal), typeof(string))]
    public class DecimaltoNumericConverter : IValueConverter
    {

        CultureInfo ci = CultureInfo.CurrentCulture;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal preco = (decimal)value;
            return preco.ToString("C", ci);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string nvalue = (string)value;
            string preco = nvalue.ToString(ci);
            decimal result;
            if (Decimal.TryParse(preco, NumberStyles.Any, culture, out result))
            {
                return result;
            }

            return value;
        }
    }
}
