using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// mp potion
    /// </summary>
    public class PotionMP : Potion
    {
        private Character_classes player;

        private DispatcherTimer timer;

        /// <summary>
        /// the mp potion cons
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="map">the map</param>
        /// <param name="x">x cord</param>
        /// <param name="y">y cord</param>
        public PotionMP(Character_classes player, Maps map, double x, double y) : base(map)
        {
            this.player = player;            
            Removable = false;
            timer = new DispatcherTimer();
            Geometry = new EllipseGeometry(new Point(x, y), 15, 15);
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\MPpotion.PNG", UriKind.Relative)));
            this.Map.Potions.Add(this);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Gets or sets a value indicating whether removeable
        /// </summary>
        public bool Removable
        {
            get;
            set;
        }        

        /// <summary>
        /// recorver 20% of max hp
        /// </summary>
        public void RecorverMP()
        {
            if (player.Resource + (int)(0.2 * player.MaxResource) <= player.MaxResource)
            {
                player.Resource += (int)(0.2 * player.MaxResource);
            }
            else
            {
                player.Resource = player.MaxResource;
            }           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Removable = true;
            timer.Stop();
        }
    }
}
