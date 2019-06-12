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
    /// skeleton monster
    /// </summary>
    public class Skeleton : Monsters
    {
        /// <summary>
        /// skeleton cons
        /// </summary>
        /// <param name="chare">chare of cons</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public Skeleton(Character_classes chare, int x, int y, Maps map, double dirX, double dirY) : base(10 + (chare.LVL * 10), 0, 10 + (chare.LVL * 3), 8 + (chare.LVL * 3), chare.LVL, "Skeleton", x, y, map, chare, 10)
        {
            Geometry = new EllipseGeometry(new Point(x, y), 25, 30);
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\skeleton.PNG", UriKind.Relative)));
            DirX = dirX;
            DirY = dirY;
            Actpoint = new Point(x, y);
            this.Map.Monsters.Add(this);
        }           
    }
}
