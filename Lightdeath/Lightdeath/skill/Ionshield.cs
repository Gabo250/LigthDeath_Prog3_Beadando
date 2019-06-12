using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// ionshield skill
    /// </summary>
    public class Ionshield : DarkMage_skill
    {
        private int hold;

        private DispatcherTimer time;

        /// <summary>
        /// skill sonctructor
        /// </summary>
        /// <param name="map">the map</param>
        /// <param name="x">x cordinate</param>
        /// <param name="y">y cordinate</param>
        public Ionshield(Maps map, double x, double y) : base(0, 100, map)
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 30, 35);
            Geometry = eg;           
            Image = null;
            Actpoint = new Point(x, y);
            hold = 5;
            Removeable = false;
            this.Map.Skill.Add(this);
            time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 0, 1);
            time.Tick += Time_Tick;
            time.Start();
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
        /// transform geometry
        /// </summary>
        /// <param name="trans">the transform</param>
        public void Move(Transform trans)
        {
            Geometry.Transform = trans;
            Geometry = Geometry.GetFlattenedPathGeometry();
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            if (hold != 0)
            {
                hold--;
            }

            if (hold == 0)
            {
                Removeable = true;
                time.Stop();
            }
        }
    }
}
