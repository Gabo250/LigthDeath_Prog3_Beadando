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
    /// boss mariel
    /// </summary>
    public class Mariel : Bosses
    {
        /// <summary>
        /// mariel cons
        /// </summary>
        /// <param name="chare">chare of cons</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">the direction x</param>
        /// <param name="dirY">the direction y</param>
        public Mariel(Character_classes chare, double x, double y, Maps map, double dirX, double dirY) : base("Mariel", chare.LVL, 20 + (chare.LVL * 10), 500 + (chare.LVL * 400), 150, 15 + (chare.LVL * 5), x, y, map, chare, (chare.LVL * 40))
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 50, 65);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Mariel.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            DirX = dirX;
            DirY = dirY;
            this.Map.Monsters.Add(this);
        }        
    }
}
