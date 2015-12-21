using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SincronosXMLFiscal.InfraEstrutura.Converters
{
    public class ZeroToStringConverter : IValueConverter
    {

        public int EmptStringValue { get; set; }

        private bool IsNumeric(string strToCheck)
        {
            return Regex.IsMatch(strToCheck, "^\\d+(\\.\\d+)?$");
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            else if (value is string)
                return value;
            else if (value is int && (int)value == EmptStringValue)
                return string.Empty;
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {

                string s = (string)value;

                if (IsNumeric(s))
                {
                    return System.Convert.ToInt32(s);
                }
                else
                {
                    return EmptStringValue;
                }

            }

            return value;
        }
    }
}
