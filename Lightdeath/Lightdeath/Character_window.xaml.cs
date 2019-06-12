using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// Interaction logic for Character_window.xaml
    /// </summary>
    public partial class Character_window : Window
    {
        private Viewmodel_charstatview vmcv;

        private DispatcherTimer timer;

        /// <summary>
        /// the cons
        /// </summary>
        /// <param name="aktchar">actual char</param>
        /// <param name="time">disp timer</param>
        public Character_window(Character_classes aktchar, DispatcherTimer time)
        {
            InitializeComponent();
            this.vmcv = new Viewmodel_charstatview(aktchar);
            timer = time;
            this.DataContext = vmcv;
        }

        private void STR_plus(object sender, RoutedEventArgs e)
        {
            vmcv.Aktchar.STR += 1;
            vmcv.Aktchar.Statpoint -= 1;
        }

        private void INT_plus(object sender, RoutedEventArgs e)
        {
            vmcv.Aktchar.INT += 1;
            vmcv.Aktchar.Statpoint -= 1;
        }

        private void VIT_plus(object sender, RoutedEventArgs e)
        {
            vmcv.Aktchar.VIT += 1;
            vmcv.Aktchar.Statpoint -= 1;
        }

        private void WIT_plus(object sender, RoutedEventArgs e)
        {
            vmcv.Aktchar.WIT += 1;
            vmcv.Aktchar.Statpoint -= 1;
        }

        private void MEN_plus(object sender, RoutedEventArgs e)
        {
            vmcv.Aktchar.Men += 1;
            vmcv.Aktchar.Statpoint -= 1;
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            timer.Start();
        }
    }
}
