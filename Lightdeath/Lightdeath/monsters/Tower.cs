using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lightdeath
{
    /// <summary>
    /// the tower
    /// </summary>
    public class Tower : Monsters
    {
        /// <summary>
        /// the tower
        /// </summary>
        /// <param name="chare">the character</param>
        /// <param name="map">the map</param>
        public Tower(Character_classes chare, Maps map) : base((chare.LVL * 350), 0, (chare.LVL * 5), 0, chare.LVL, "Tower", map.Width - 100, (map.Height / 2) - 40, map, chare, (chare.LVL * 45))
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(map.Width - 60, (map.Height / 2) - 40), 35, 55);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Tower.PNG", UriKind.Relative)));
            Actpoint = new Point(map.Width - 60, (map.Height / 2) - 40);
            this.Map.Monsters.Add(this);
        }        
    }
}
