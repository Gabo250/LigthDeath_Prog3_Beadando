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
    /// basic attack for DarkMage
    /// </summary>
    public class Basic_attack : DarkMage_skill
    {
        private double dirX;

        private double dirY;

        /// <summary>
        /// basic attak cons
        /// </summary>
        /// <param name="dmg">dmg of skill</param>
        /// <param name="x">x cordinate of basic spell</param>
        /// <param name="y">y cordinate of basic spell</param>
        /// <param name="map">actual map</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        public Basic_attack(int dmg, double x, double y, Maps map, double dirX, double dirY) : base(dmg, 0,  map)
        {            
            EllipseGeometry eg = new EllipseGeometry(new Point(x, y), 10, 10);
            Geometry = eg;
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Basicattack.PNG", UriKind.Relative)));
            this.dirX = dirX;
            this.dirY = dirY;
            map.Skill.Add(this);
        }

        /// <summary>
        /// move to the target point
        /// </summary>
        public void Move()
        {
            Skillmove(new TranslateTransform((6 * dirX), (6 * dirY)));
            Actpoint = new Point(Actpoint.X + (6 * dirX), Actpoint.Y + (6 * dirY));
        }
    }
}
