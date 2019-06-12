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
    /// firelod boss
    /// </summary>
    public class FireLord : Monsters
    {
        /// <summary>
        /// fireee boss of game
        /// </summary>
        /// <param name="chare">chare of cons</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param> 
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public FireLord(Character_classes chare, double x, double y, Maps map, double dirX, double dirY) : base(45 + (chare.LVL * 20), 10, 10 + (chare.LVL * 4), 8 + (chare.LVL * 5), chare.LVL, "FireLord", x, y, map, chare, 30)
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 25, 30);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Firelord.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            DirX = dirX;
            DirY = dirY;
            this.Map.Monsters.Add(this);
        }       
    }
}
