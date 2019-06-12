using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lightdeath
{
    /// <summary>
    /// post entry screen
    /// </summary>
    public class StartGameScreen
    {
        ////private varriables//
        #region
        private Grid start;

        private Button startbutton;

        private Button exitbutton;

        private Rectangle rect;

        private Character_classes character;

        private TextBox tb;

        private System.Windows.Controls.Label nevmegadas;

        private Button ok;

        private System.Windows.Controls.Label firstmaplabel;

        private System.Windows.Controls.Label secmaplabel;

        private System.Windows.Controls.Label thirdmaplabel;

        private System.Windows.Controls.Label chose;

        private bool firstmap;

        private bool secmap;

        private bool thirdmap;

        private MainWindow main;

        private Maps firtmap;

        private Maps smap;

        private Maps thmap;
        
        #endregion

        /// <summary>
        /// startgame constructor
        /// </summary>
        /// <param name="character">character class</param>
        /// <param name="main">main window</param>
        public StartGameScreen(Character_classes character, MainWindow main)
        {
            this.start = new Grid();
            this.character = character;
            this.start.Width = 1000;
            this.start.Height = 800;
            ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\Startbackground.PNG", UriKind.Relative)));
            this.start.Background = ib;
            this.main = main;
            main.Vml.Display = new Displaying_engine(main.Vml.Map, main, this);
            firstmap = false;
            secmap = false;
            thirdmap = false;           
            firtmap = new Firstmap(1000, 800, main.Vml.User.Chars);
            smap = new Secondmap(1000, 800, main.Vml.User.Chars);
            thmap = new Thirdmap(1000, 800, main.Vml.User.Chars);
            this.Initalize();
        }

        /// <summary>
        /// Gets start grid
        /// </summary>
        public Grid Start
        {
            get { return this.start; }
        }
        ////initalize start screen//
        #region
        private void Initalize()
        {
            ////start button//
            startbutton = new Button();
            startbutton.Content = "Start";
            LinearGradientBrush lg = new LinearGradientBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"), (Color)ColorConverter.ConvertFromString("#FF252424"), new Point(0.5, 0), new Point(0.5, 1));
            startbutton.Background = lg;
            startbutton.Width = 100;
            startbutton.Height = 40;
            startbutton.HorizontalAlignment = HorizontalAlignment.Center;
            startbutton.Margin = new Thickness(-800, 500, 0, 0);           
            startbutton.Click += Startbutton_Click;
            this.start.Children.Add(startbutton);
            ////exit button//
            exitbutton = new Button();
            exitbutton.Content = "Exit";            
            exitbutton.Background = lg;
            exitbutton.Width = 100;
            exitbutton.Height = 40;
            exitbutton.HorizontalAlignment = HorizontalAlignment.Center;
            exitbutton.Margin = new Thickness(-450, 500, 0, 0);
            exitbutton.Click += Exitbutton_Click;
            this.start.Children.Add(exitbutton);
            ////for character image//
            rect = new Rectangle();
            rect.Width = 220;
            rect.Height = 300;
            ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\Mage.PNG", UriKind.Relative)));
            rect.Fill = ib;
            rect.HorizontalAlignment = HorizontalAlignment.Center;
            rect.Margin = new Thickness(0, 10, 0, 0);
            this.start.Children.Add(rect);
            ////if new character need because this is a new account//
            if (character.Name == null)
            {
                ////for write//
                tb = new TextBox();
                tb.Text = null;
                tb.Width = rect.Width;
                tb.Background = null;
                tb.Foreground = Brushes.White;
                tb.Height = 23;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Margin = new Thickness(0, 360, 0, 0);
                tb.PreviewKeyDown += Tb_PreviewKeyDown;
                start.Children.Add(tb);
                ////info//
                nevmegadas = new System.Windows.Controls.Label();
                nevmegadas.Content = "Give name to the Character";
                nevmegadas.Foreground = Brushes.White;
                nevmegadas.HorizontalAlignment = HorizontalAlignment.Center;
                nevmegadas.Margin = new Thickness(0, 600, 0, 0);
                start.Children.Add(nevmegadas);
                ////name change//
                ok = new Button();
                ok.Content = "ok";
                ok.Background = lg;
                ok.Width = 20;
                ok.Height = 23;
                ok.HorizontalAlignment = HorizontalAlignment.Center;
                ok.Margin = new Thickness(250, 360, 0, 0);
                ok.Click += Ok_Click;
                start.Children.Add(ok);
            }
            ////character name//
            System.Windows.Controls.Label nev = new System.Windows.Controls.Label();
            nev.SetBinding(System.Windows.Controls.Label.ContentProperty, new Binding("Name") { Source = character });
            nev.HorizontalAlignment = HorizontalAlignment.Center;
            nev.Margin = new Thickness(0, 80, 0, 0);
            nev.FontSize = 50;
            nev.Foreground = Brushes.White;
            start.Children.Add(nev);
            ////chose label//
            chose = new System.Windows.Controls.Label();
            chose.Content = "Choose a map to START!";
            chose.HorizontalAlignment = HorizontalAlignment.Center;
            chose.Margin = new Thickness(-20, 620, 0, 0);
            chose.FontSize = 20;
            chose.Foreground = Brushes.Red;
            start.Children.Add(chose);
            ////character level//
            System.Windows.Controls.Label level = new System.Windows.Controls.Label();
            level.SetBinding(System.Windows.Controls.Label.ContentProperty, new Binding("LVL") { Source = character });
            level.HorizontalAlignment = HorizontalAlignment.Center;
            level.Margin = new Thickness(0, 180, 0, 0);
            level.FontSize = 50;
            level.Foreground = Brushes.White;
            start.Children.Add(level);
            ////firstmap optional//
            firstmaplabel = new System.Windows.Controls.Label();
            firstmaplabel.Width = 300;
            firstmaplabel.Height = 150;
            firstmaplabel.HorizontalAlignment = HorizontalAlignment.Center;
            firstmaplabel.Margin = new Thickness(0, -600, 600, 0);
            firstmaplabel.BorderBrush = null;             
            firstmaplabel.Content = "Naratos Map";
            firstmaplabel.Foreground = Brushes.Gold;
            firstmaplabel.FontSize = 40;
            firstmaplabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            firstmaplabel.VerticalContentAlignment = VerticalAlignment.Center;
            firstmaplabel.Background = new ImageBrush(new BitmapImage(new Uri(@"images\Naratos.PNG", UriKind.Relative)));
            firstmaplabel.BorderThickness = new Thickness(2, 2, 2, 2);
            start.Children.Add(firstmaplabel);
            firstmaplabel.MouseDoubleClick += Firstmaplabel_MouseDoubleClick;
            ////secmap optional//
            secmaplabel = new System.Windows.Controls.Label();
            secmaplabel.Width = 300;
            secmaplabel.Height = 150;
            secmaplabel.HorizontalAlignment = HorizontalAlignment.Center;
            secmaplabel.Margin = new Thickness(0, -200, 600, 0);
            secmaplabel.BorderBrush = null;
            secmaplabel.Content = "Mariel Map";
            secmaplabel.Foreground = Brushes.Gold;
            secmaplabel.FontSize = 40;
            secmaplabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            secmaplabel.VerticalContentAlignment = VerticalAlignment.Center;
            secmaplabel.Background = new ImageBrush(new BitmapImage(new Uri(@"images\Mariel.PNG", UriKind.Relative)));
            secmaplabel.BorderThickness = new Thickness(2, 2, 2, 2);
            start.Children.Add(secmaplabel);
            secmaplabel.MouseDoubleClick += Secmaplabel_MouseDoubleClick;
            ////thirdmap optional//
            thirdmaplabel = new System.Windows.Controls.Label();
            thirdmaplabel.Width = 300;
            thirdmaplabel.Height = 150;
            thirdmaplabel.HorizontalAlignment = HorizontalAlignment.Center;
            thirdmaplabel.Margin = new Thickness(0, 200, 600, 0);
            thirdmaplabel.BorderBrush = null;
            thirdmaplabel.BorderThickness = new Thickness(2, 2, 2, 2);
            thirdmaplabel.Content = "DeathKing Map";
            thirdmaplabel.Foreground = Brushes.Gold;
            thirdmaplabel.FontSize = 40;
            thirdmaplabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            thirdmaplabel.VerticalContentAlignment = VerticalAlignment.Center;
            thirdmaplabel.Background = new ImageBrush(new BitmapImage(new Uri(@"images\Deathking.PNG", UriKind.Relative)));
            start.Children.Add(thirdmaplabel);
            thirdmaplabel.MouseDoubleClick += Thirdmaplabel_MouseDoubleClick;
            ////previus game stats//
            System.Windows.Controls.Label prevstats = new System.Windows.Controls.Label();
            prevstats.Content = "Previus Game Stats";
            prevstats.HorizontalAlignment = HorizontalAlignment.Center;
            prevstats.Margin = new Thickness(600, 200, 0, 0);
            prevstats.BorderBrush = null;
            prevstats.Foreground = Brushes.Turquoise;
            prevstats.FontSize = 25;
            start.Children.Add(prevstats);
            ////killed monsters//
            System.Windows.Controls.Label killmons = new System.Windows.Controls.Label();
            killmons.Content = "Killed Monsters:";
            killmons.HorizontalAlignment = HorizontalAlignment.Center;
            killmons.Margin = new Thickness(390, 250, 0, 0);
            killmons.BorderBrush = null;
            killmons.Foreground = Brushes.White;
            killmons.FontSize = 15;
            start.Children.Add(killmons);
            System.Windows.Controls.Label killmonsnumb = new System.Windows.Controls.Label();
            killmonsnumb.SetBinding(System.Windows.Controls.Label.ContentProperty, new Binding("Killedmons") { Source = main.Vml.User.Chars });
            killmonsnumb.HorizontalAlignment = HorizontalAlignment.Center;
            killmonsnumb.Margin = new Thickness(850, 250, 0, 0);
            killmonsnumb.BorderBrush = null;
            killmonsnumb.Foreground = Brushes.Gold;
            killmonsnumb.FontSize = 15;
            start.Children.Add(killmonsnumb);
            ////Total Damage dealt to monsters//
            System.Windows.Controls.Label damtomons = new System.Windows.Controls.Label();
            damtomons.Content = "Total Damage Dealt to Monsters:";
            damtomons.HorizontalAlignment = HorizontalAlignment.Center;
            damtomons.Margin = new Thickness(500, 350, 0, 0);
            damtomons.BorderBrush = null;
            damtomons.Foreground = Brushes.White;
            damtomons.FontSize = 15;
            start.Children.Add(damtomons);
            System.Windows.Controls.Label damtomonsnumb = new System.Windows.Controls.Label();
            damtomonsnumb.SetBinding(System.Windows.Controls.Label.ContentProperty, new Binding("Damagedealt") { Source = main.Vml.User.Chars });
            damtomonsnumb.HorizontalAlignment = HorizontalAlignment.Center;
            damtomonsnumb.Margin = new Thickness(850, 350, 0, 0);
            damtomonsnumb.BorderBrush = null;
            damtomonsnumb.Foreground = Brushes.Goldenrod;
            damtomonsnumb.FontSize = 15;
            start.Children.Add(damtomonsnumb);
            ////total damage taken from monsters//
            System.Windows.Controls.Label damfrommons = new System.Windows.Controls.Label();
            damfrommons.Content = "Total Damage Taken from Monsters:";
            damfrommons.HorizontalAlignment = HorizontalAlignment.Center;
            damfrommons.Margin = new Thickness(530, 450, 0, 0);
            damfrommons.BorderBrush = null;
            damfrommons.Foreground = Brushes.White;
            damfrommons.FontSize = 15;
            start.Children.Add(damfrommons);
            System.Windows.Controls.Label damfrommonsnumb = new System.Windows.Controls.Label();
            damfrommonsnumb.SetBinding(System.Windows.Controls.Label.ContentProperty, new Binding("Damagetaken") { Source = main.Vml.User.Chars });
            damfrommonsnumb.HorizontalAlignment = HorizontalAlignment.Center;
            damfrommonsnumb.Margin = new Thickness(850, 450, 0, 0);
            damfrommonsnumb.BorderBrush = null;
            damfrommonsnumb.Foreground = Brushes.Red;
            damfrommonsnumb.FontSize = 15;
            start.Children.Add(damfrommonsnumb);
        }
        #endregion
        ////events//
        #region
        private void Exitbutton_Click(object sender, RoutedEventArgs e)
        {
            main.Vml.User.Save();
            Application.Current.Shutdown();
        }

        private void Thirdmaplabel_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            firstmap = false;
            secmap = false;
            thirdmap = true;
            firstmaplabel.BorderBrush = null;
            thirdmaplabel.BorderBrush = Brushes.Green;
            secmaplabel.BorderBrush = null;
        }

        private void Secmaplabel_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            firstmap = false;
            secmap = true;
            thirdmap = false;
            firstmaplabel.BorderBrush = null;
            thirdmaplabel.BorderBrush = null;
            secmaplabel.BorderBrush = Brushes.Green;
        }

        private void Firstmaplabel_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            firstmap = true;
            secmap = false;
            thirdmap = false;
            firstmaplabel.BorderBrush = Brushes.Green;
            thirdmaplabel.BorderBrush = null;
            secmaplabel.BorderBrush = null;
        }

        private void Startbutton_Click(object sender, RoutedEventArgs e)
        {
            ////start game//
            try
            {
                if (!start.Children.Contains(ok))
                {
                    if (firstmap)
                    {
                        main.Vml.Map = firtmap;
                        main.Vml.Display.Map = main.Vml.Map;
                        main.Vml.Display.Initalize();
                        main.Content = main.Vml.Display;
                    }
                    else if (secmap)
                    {
                        main.Vml.Map = smap;
                        main.Vml.Display.Map = main.Vml.Map;
                        main.Vml.Display.Initalize();
                        main.Content = main.Vml.Display;
                    }
                    else if (thirdmap)
                    {
                        main.Vml.Map = thmap;
                        main.Vml.Display.Map = main.Vml.Map;
                        main.Vml.Display.Initalize();
                        main.Content = main.Vml.Display;
                    }
                }
            }
            catch (Exception k)
            {
                using (StreamWriter sw = new StreamWriter("hiba.txt"))
                {
                    sw.WriteLine(k.Message);
                }
            }
        }

        private void Tb_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;                
            }
            else
            {                
                if (tb.Text.Length == 8 && e.Key != System.Windows.Input.Key.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text != null && tb.Text != string.Empty)
            {
                character.Name = tb.Text;
                start.Children.Remove(tb);
                start.Children.Remove(nevmegadas);
                start.Children.Remove(ok);
                main.Vml.User.Save();
            }
        }
        #endregion
    }
}
