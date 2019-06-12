using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lightdeath
{
    /// <summary>
    /// skills for game
    /// </summary>
    public class Skills : Bindable
    {
        private int dmg;

        private int cost;

        private Maps map;

        private Geometry geometry;

        private ImageBrush image;

        private Point actpoint;

        /// <summary>
        /// skills cons
        /// </summary>
        /// <param name="dmg">dmg of skill</param>
        /// <param name="cost">cost of skill</param>
        /// <param name="map">actual map</param>
        public Skills(int dmg, int cost, Maps map)
        {
            this.dmg = dmg;
            this.cost = cost;
            this.map = map;
        }

        /// <summary>
        /// Gets or sets damage of spell
        /// </summary>
        public int DMG
        {
            get
            {
                return this.dmg;
            }

            set
            {
                this.dmg = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets cost of spell
        /// </summary>
        public int Cost
        {
            get
            {
                return this.cost;
            }

            set
            {
                this.cost = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the geometry of skill
        /// </summary>
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

        /// <summary>
        /// Gets the skill on the map
        /// </summary>
        public Maps Map
        {
            get { return map; }
        }

        /// <summary>
        /// Gets or sets image
        /// </summary>
        public ImageBrush Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// Gets or sets actpoint
        /// </summary>
        public Point Actpoint
        {
            get { return actpoint; }
            set { actpoint = value; }
        }

        /// <summary>
        /// move skill on the map
        /// </summary>
        /// <param name="trans">transofrmation or transformations</param>
        public virtual void Skillmove(Transform trans)
        {
            this.geometry.Transform = trans;
            this.geometry = this.geometry.GetFlattenedPathGeometry();
        }

        public bool Crash(Rect rect)
        {
            if (this.geometry.Bounds.IntersectsWith(rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
