using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lightdeath
{
    /// <summary>
    /// percent conv
    /// </summary>
    public class Percentconverter : IValueConverter
    {
        /// <summary>
        /// convert for percent
        /// </summary>
        /// <param name="value">conv value</param>
        /// <param name="targetType">targettype of oaram</param>
        /// <param name="parameter">parameter of param</param>
        /// <param name="culture">culture of param</param>
        /// <returns> return convert </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double percent = (double)value;
            string s = (percent * 100).ToString("0.##");
            return s + " %";
        }

        /// <summary>
        /// null value
        /// </summary>
        /// <param name="value"> value param </param>
        /// <param name="targetType"> targtype param </param>
        /// <param name="parameter"> parameter paran </param>
        /// <param name="culture"> culture param </param>
        /// <returns> return exception </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
