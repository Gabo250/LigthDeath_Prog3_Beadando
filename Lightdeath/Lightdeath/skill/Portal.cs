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
    /// portal skill for tower
    /// </summary>
    public class Portal : Skills
    {
        private DispatcherTimer timer;

        private int width;

        private int height;

        private bool zoomout;

        private EllipseGeometry elip;

        /// <summary>
        /// cons of portal skill
        /// </summary>
        /// <param name="map">the map</param>
        /// <param name="x">x cordionate</param>
        /// <param name="y">y cordinate</param>
        public Portal(Maps map, double x, double y) : base(0, 0, map)
        {
            width = 1;
            height = 1;
            Removeable = false;
            zoomout = false;
            elip = new EllipseGeometry(new Point(x, y), 1, 1);
            Geometry = elip;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\portal.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 2);
            timer.Tick += Timer_Tick;
            this.Map.Skill.Add(this);
            timer.Start();
        }              

        /// <summary>
        /// Gets or sets the removeable
        /// </summary>
        public bool Removeable
        {
            get;
            set;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (width < 50 && height < 50 && !zoomout)
            {
                width += 10;
                height += 10;
                elip.RadiusX = width;
                elip.RadiusY = height;
                Geometry = elip;
            }
            else
            {
                zoomout = true;
                if (zoomout && width > 0 && height > 0)
                {
                    width -= 2;
                    height -= 2;
                    elip.RadiusX = width;
                    elip.RadiusY = height;
                    Geometry = elip;
                }
                else
                {
                    Removeable = true;
                    timer.Stop();
                }
            }
        }
    }
}
