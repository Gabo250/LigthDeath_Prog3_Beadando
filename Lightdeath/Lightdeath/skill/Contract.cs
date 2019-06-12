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
    /// contarct skill
    /// </summary>
    public class Contract : Skills
    {
        private static Random r;

        private DispatcherTimer timer;

        private int height;

        private int width;

        private int distance;

        private EllipseGeometry elip;

        /// <summary>
        /// contract cons
        /// </summary>
        /// <param name="map">the map</param>
        /// <param name="x">x cord</param>
        /// <param name="y">y cord</param>
        /// <param name="height">the heigfht</param>
        /// <param name="width">the width</param>
        /// <param name="distance">the distance</param>
        public Contract(Maps map, double x, double y, int height, int width, int distance) : base(0, 25, map)
        {
            this.height = height;
            this.width = width;
            elip = new EllipseGeometry(new Point(x, y), width, height);
            Geometry = elip;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Contractskill_icon.png", UriKind.Relative)));
            r = new Random();
            Actpoint = new Point(x, y);
            this.distance = distance;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 2);
            timer.Tick += Timer_Tick;
            this.Map.Skill.Add(this);
            timer.Start();
            Removeable = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the removeable
        /// </summary>
        public bool Removeable
        {
            get;
            set;
        }

        /// <summary>
        /// össze húzza adott sugárba a monstereket
        /// </summary>
        public void Cont()
        {
            List<Monsters> inradius = this.Map.Indistance(distance, Actpoint.X, Actpoint.Y);
            if (inradius != null && inradius.Count > 0)
            {
                foreach (Monsters mon in inradius)
                {
                    if (!(mon is Tower))
                    {
                        int rand = r.Next(0, 4);
                        if (rand == 0)
                        {
                            mon.Actpoint = new Point(Actpoint.X - r.Next(0, 38), Actpoint.Y - r.Next(0, 38));
                            if (!(mon is Bosses))
                            {
                                mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 25, 30);
                            }
                            else
                            {
                                if (mon is Naratos)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 45, 60);
                                }
                                else if (mon is Mariel)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 50, 65);
                                }
                                else
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 60, 80);
                                }
                            }
                        }
                        else if (rand == 1)
                        {
                            mon.Actpoint = new Point(Actpoint.X + r.Next(0, 38), Actpoint.Y + r.Next(0, 38));
                            if (!(mon is Bosses))
                            {
                                mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 25, 30);
                            }
                            else
                            {
                                if (mon is Naratos)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 45, 60);
                                }
                                else if (mon is Mariel)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 50, 65);
                                }
                                else
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 60, 80);
                                }
                            }
                        }
                        else if (rand == 2)
                        {
                            mon.Actpoint = new Point(Actpoint.X - r.Next(0, 38), Actpoint.Y + r.Next(0, 38));
                            if (!(mon is Bosses))
                            {
                                mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 25, 30);
                            }
                            else
                            {
                                if (mon is Naratos)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 45, 60);
                                }
                                else if (mon is Mariel)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 50, 65);
                                }
                                else
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 60, 80);
                                }
                            }
                        }
                        else if (rand == 3)
                        {
                            mon.Actpoint = new Point(Actpoint.X + r.Next(0, 38), Actpoint.Y - r.Next(0, 38));
                            if (!(mon is Bosses))
                            {
                                mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 25, 30);
                            }
                            else
                            {
                                if (mon is Naratos)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 45, 60);
                                }
                                else if (mon is Mariel)
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 50, 65);
                                }
                                else
                                {
                                    mon.Geometry = new EllipseGeometry(new Point(mon.Actpoint.X, mon.Actpoint.Y), 60, 80);
                                }
                            }
                        }
                    }                    
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (width > 0 && height > 0)
            {
                height -= 30;
                width -= 30;
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
