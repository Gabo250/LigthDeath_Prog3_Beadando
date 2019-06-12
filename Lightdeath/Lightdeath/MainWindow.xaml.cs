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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// tha cons
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Vml = new Viewmodel_login();
            this.DataContext = Vml;            
        }        

        /// <summary>
        /// Gets or sets vml
        /// </summary>
        public Viewmodel_login Vml { get; set; }

        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Vml.User.Accname != null && Vml.User.Passwd != null)
                {
                    if (Vml.User.Haveacc())
                    {                        
                        Vml.StartgameScreen = new StartGameScreen(Vml.User.Chars, this);
                        this.Content = Vml.StartgameScreen.Start;
                        this.Height = Vml.StartgameScreen.Start.Height;
                        this.Width = Vml.StartgameScreen.Start.Width;
                    }
                    else
                    {
                        MessageBox.Show("Not existing this user");
                    }
                }
                else
                {
                    MessageBox.Show("Username or/and password is empty");
                }
            }
            catch (IncorrectAcc_username k)
            {
                MessageBox.Show(k.Message);
            }
        }

        private void CreateAcc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Vml.User.Accname != null && Vml.User.Passwd != null)
                {
                    Vml.User.Createacc();
                    Vml.StartgameScreen = new StartGameScreen(Vml.User.Chars, this);
                    this.Content = Vml.StartgameScreen.Start;
                    this.Height = Vml.StartgameScreen.Start.Height;
                    this.Width = Vml.StartgameScreen.Start.Width;
                }
                else if (Vml.User.Accname != null && Vml.User.Passwd != null)
                {
                    MessageBox.Show("Account has been created");
                }
                else
                {
                    MessageBox.Show("Usename or/and password is empty");                                     
                }
            }
            catch (Account_already_have k)
            {
                MessageBox.Show(k.Message);
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Vml.User.Passwd = ((PasswordBox)sender).Password;
        }
    }
}
