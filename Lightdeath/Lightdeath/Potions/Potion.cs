using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lightdeath
{
    /// <summary>
    /// the potions
    /// </summary>
    public class Potion
    {
        private Geometry geometry;

        private ImageBrush image;        

        private Maps map;

        public Potion(Maps map)
        {
            this.map = map;
        }

        /// <summary>
        /// Gets or sets the geometry
        /// </summary>
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Maps Map
        {
            get { return map; }
            set { map = value; }
        }

        /// <summary>
        /// Gets or sets the image
        /// </summary>
        public ImageBrush Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
