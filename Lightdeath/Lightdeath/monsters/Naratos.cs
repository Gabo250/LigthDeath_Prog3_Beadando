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
    /// boss naratos
    /// </summary>
    public class Naratos : Bosses
    {
        /// <summary>
        /// boss narratos
        /// </summary>
        /// <param name="chare">chare of cons</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public Naratos(Character_classes chare, double x, double y, Maps map, double dirX, double dirY) : base("Naratos", chare.LVL, 10 + (chare.LVL * 15), 500 + (chare.LVL * 350), 150, 10 + (chare.LVL * 10), x, y, map, chare, (chare.LVL * 25))
        {
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Naratos.PNG", UriKind.Relative)));
            Geometry = new EllipseGeometry(new Point(x, y), 45, 60);
            Actpoint = new Point(x, y);
            DirX = DirX;
            DirY = DirY;
            map.Monsters.Add(this);
        }       
    }
}
