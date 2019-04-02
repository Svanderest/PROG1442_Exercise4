using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Lab3_Client.Converters
{
    class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value == null)
                    return string.Empty;
                return String.Format("{0:n0}", value);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string val = value.ToString();
                if (!decimal.TryParse(val, out decimal result))
                    return 0;
                else
                    return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}
