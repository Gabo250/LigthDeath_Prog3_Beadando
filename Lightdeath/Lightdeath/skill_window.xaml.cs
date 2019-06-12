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
    /// Interaction logic for skill_window.xaml
    /// </summary>
    public partial class Skill_window : Window
    {
        private DispatcherTimer timer;

        /// <summary>
        /// the cons
        /// </summary>
        /// <param name="mage">act char</param>
        /// <param name="time">disp timer</param>
        public Skill_window(DarkMage mage, DispatcherTimer time)
        {
            InitializeComponent();
            Aktchar = mage;
            timer = time;
            this.DataContext = Aktchar;
            if (Aktchar.IonShieldSkillPoint == 16)
            {
                gr.Children.Remove(button2);
            }
        }

        /// <summary>
        /// Gets or sets the char
        /// </summary>
        public DarkMage Aktchar { get; set; }

        private void DarkBallpoint(object sender, RoutedEventArgs e)
        {
            Aktchar.DarkBallSkillPoint++;
            Aktchar.Skillpoint--;
        }

        private void Lightingpoint(object sender, RoutedEventArgs e)
        {
            Aktchar.LightingSkillPoint++;
            Aktchar.Skillpoint--;
        }

        private void Ionshieldpoint(object sender, RoutedEventArgs e)
        {
            if (Aktchar.IonShieldSkillPoint < 16)
            {
                Aktchar.IonShieldSkillPoint++;
                Aktchar.Skillpoint--;
            }
            
            if (Aktchar.IonShieldSkillPoint == 16)
            {
                gr.Children.Remove(button2);
            }           
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            timer.Start();    
        }

        private void Conract(object sender, RoutedEventArgs e)
        {
            Aktchar.ContractSkillPoint++;
            Aktchar.Skillpoint--;
        }
    }
}
