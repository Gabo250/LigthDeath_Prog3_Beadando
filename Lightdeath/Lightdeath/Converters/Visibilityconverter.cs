using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Lightdeath
{
    /// <summary>
    /// visibilitzy converter
    /// </summary>
    public class Visibilityconverter : IValueConverter
    {
        /// <summary>
        /// the convert method
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <param name="targetType">target type parameter</param>
        /// <param name="parameter">parameter parameter</param>
        /// <param name="culture">culture parameter</param>
        /// <returns>convert value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibility = (bool)value;
            if (visibility)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// convert bakc method
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <param name="targetType">targettype parameter</param>
        /// <param name="parameter">parameter parameter</param>
        /// <param name="culture">culture parameter</param>
        /// <returns>convertback value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
