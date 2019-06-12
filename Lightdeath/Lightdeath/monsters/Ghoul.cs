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
    /// ghoul monster
    /// </summary>
    public class Ghoul : Monsters
    {
        /// <summary>
        /// ghoul cons
        /// </summary>
        /// <param name="chare">chare of cons</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public Ghoul(Character_classes chare, double x, double y, Maps map, double dirX, double dirY) : base(20 + (chare.LVL * 15), 0, 10 + (chare.LVL * 4), 8 + (chare.LVL * 5), chare.LVL, "Ghoul", x, y, map, chare, 25)
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 25, 30);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Ghoul.PNG", UriKind.Relative)));
            DirX = dirX;
            DirY = dirY;
            Actpoint = new Point(x, y);
            this.Map.Monsters.Add(this);
        }        
    }
}
