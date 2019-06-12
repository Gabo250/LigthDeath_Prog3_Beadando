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
    /// DM skill
    /// </summary>
    public class Darkball : DarkMage_skill
    {
        private double dirX;

        private double dirY;

        private bool crash;

        private int width;

        private int height;

        private DispatcherTimer time;

        private EllipseGeometry elip;

        /// <summary>
        /// darkball cons
        /// </summary>
        /// <param name="damage">damage of skill</param>
        /// <param name="x">x position of skill</param>
        /// <param name="y">y position of skill</param>
        /// <param name="map">actual map</param>
        /// <param name="dirX">x dir</param>
        /// <param name="dirY">y dor</param>
        public Darkball(int damage, double x, double y, Maps map, double dirX, double dirY) : base(damage, 10, map)
        {
            width = 20;
            height = 20;
            elip = new EllipseGeometry(new Point(x, y), 20, 20); 
            this.Actpoint = new Point(x, y);
            this.Image = new ImageBrush(new BitmapImage(new Uri(@"images\DarkBall_skill.PNG", UriKind.Relative)));
            Geometry = elip;
            this.dirX = dirX;
            this.dirY = dirY;
            time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 0, 0, 2);
            time.Tick += Time_Tick;
            map.Skill.Add(this);
            crash = false;
            Removeable = false;
        }      

        /// <summary>
        /// Gets or sets crashee
        /// </summary>
        public bool Crashee
        {
            get
            {
                return crash;
            }

            set
            {
                crash = value;
                time.Start();
            }
        }

        /// <summary>
        /// Gets or sets the removeable
        /// </summary>
        public bool Removeable
        {
            get;
            set;
        }
        
        /// <summary>
        /// move skill
        /// </summary>        
        public void Move()
        {
            Skillmove(new TranslateTransform(dirX, dirY));
            this.Actpoint = new Point(Actpoint.X + dirX, Actpoint.Y + dirY);
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            if (height < 150 && width < 150)
            {
                height += 15;
                width += 15;
                elip.RadiusX = width;
                elip.RadiusY = height;
                elip.Center = new Point(Actpoint.X, Actpoint.Y);
                Geometry = elip;
            }
            else
            {
                crash = false;
                Removeable = true;
                time.Stop();
            }
        }
    }
}
