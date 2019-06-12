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
    /// an boss
    /// </summary>
    public class DeathKing : Bosses
    {
        /// <summary>
        /// deeeath boss
        /// </summary>
        /// <param name="chare">chare of cins</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public DeathKing(Character_classes chare, double x, double y, Maps map, double dirX, double dirY) : base("DeathKing", chare.LVL, 50 + (chare.LVL * 10), 500 + (chare.LVL * 650), 150, 25 + (chare.LVL * 5), x, y, map, chare, (chare.LVL * 50))
        {
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 60, 80);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Deathking.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            DirX = dirX;
            DirY = dirY;
            this.Map.Monsters.Add(this);
        }      
    }
}
