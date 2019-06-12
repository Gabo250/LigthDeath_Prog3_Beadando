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
    /// hell monster
    /// </summary>
    public class HellDog : Monsters
    {
        /// <summary>
        /// helldog cons
        /// </summary>
        /// <param name="chare">chare of cins</param>
        /// <param name="x">x cordinate of monster</param>
        /// <param name="y">y cordinate of monster</param>
        /// <param name="map">the map</param>
        /// <param name="dirX">the direction x</param>
        /// <param name="dirY">the direction y</param>
        public HellDog(Character_classes chare, int x, int y, Maps map, double dirX, double dirY) : base(35 + (chare.LVL * 15), 0, 20 + (chare.LVL * 6), 20 + (chare.LVL * 8), chare.LVL, "HellDog", x, y, map, chare, 15)
        {
            Geometry = new EllipseGeometry(new Point(x, y), 25, 30);
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Helldog.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            DirX = dirX;
            DirY = dirY;
            this.Map.Monsters.Add(this);
        }        

        /// <summary>
        /// move skeleton
        /// </summary>
        public void Move()
        {
            Monsmove(new TranslateTransform(DirX, DirY));
            Actpoint = new Point(Actpoint.X + DirX, Actpoint.Y + DirY);
        }       
    }
}
