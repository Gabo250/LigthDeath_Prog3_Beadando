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
    /// the lighting skill
    /// </summary>
    public class Lighting : DarkMage_skill
    {
        private static Random rand;

        private int maxchain;        

        /// <summary>
        /// lightin constructor
        /// </summary>
        /// <param name="damage">the damage of skill</param>
        /// <param name="x">the x cordinate</param>
        /// <param name="y">the y cordinate</param>
        /// <param name="map">the map of skill</param>
        /// <param name="dirX">direction x</param>
        /// <param name="dirY">direction y</param>
        /// <param name="dark">the darkmage</param>
        public Lighting(int damage, double x, double y, Maps map, double dirX, double dirY, DarkMage dark) : base(damage, 15, map)
        {
            Geometry = new EllipseGeometry(new Point(x, y), 25, 25);
            Image = new ImageBrush(new BitmapImage(new Uri(@"images\Lighting.PNG", UriKind.Relative)));
            Actpoint = new Point(x, y);
            DirX = dirX;
            DirY = dirY;
            Inradiusmonsters = new List<Monsters>();
            Removeable = false;            
            this.Map.Skill.Add(this);
            maxchain = 3 + dark.LightingSkillPoint;
            rand = new Random();
            this.Dark = dark;
            Prev = new Monsters(0, 0, 0, 0, 0, "nothing", 0, 0, null, null, 0);
        }

        /// <summary>
        /// Gets or sets the character
        /// </summary>
        public DarkMage Dark
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dirx
        /// </summary>
        public double DirX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the diry
        /// </summary>
        public double DirY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets counter
        /// </summary>
        public int Chaincounter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first hited monster
        /// </summary>
        public Monsters Firsthited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets prev monster
        /// </summary>
        public Monsters Prev
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets next monster
        /// </summary>
        public Monsters Nextmonster
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the inradius monster
        /// </summary>
        public List<Monsters> Inradiusmonsters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ramoveable
        /// </summary>
        public bool Removeable
        {
            get;
            set;
        }

        /// <summary>
        /// move skill and hit
        /// </summary>        
        public void Move()
        {
            Skillmove(new TranslateTransform(DirX, DirY));
            this.Actpoint = new Point(Actpoint.X + DirX, Actpoint.Y + DirY);
            bool damage = false;
            foreach (Monsters mons in Map.Monsters)
            {
                if (Crash(mons.Geometry.Bounds) && mons != Prev)
                {
                    int denominator = Dark.Critdenominator();
                    int random = rand.Next(1, denominator + 1);
                    damage = true;
                    Firsthited = mons;
                    Prev = mons;
                    Nextmonster = null;
                    if (random <= Dark.Counter())
                    {
                        mons.Getdmg((int)(DMG * Dark.CriticalDMG));                       
                    }
                    else
                    {
                        mons.Getdmg(DMG);                      
                    }                
                   
                    if (!mons.Alive)
                    {
                        Map.Removeelement.Add(mons);
                    }
                    break;
                }
            }

            if (damage)
            {
                Chaincounter++;                
            }

            if (Firsthited != null)
            {
                Inradiusmonsters = Map.Indistance(400, Actpoint.X, Actpoint.Y);
                Inradiusmonsters.Remove(Firsthited);                        
            }

            Random r = new Random();
            if (Inradiusmonsters != null && Inradiusmonsters.Count != 0 && Nextmonster == null)
            {
                int kov = r.Next(0, Inradiusmonsters.Count);
                Nextmonster = Inradiusmonsters.ElementAt(kov);                
                Inradiusmonsters = null;
            }

            if (Nextmonster != null && Nextmonster.Alive)
            {
                double angle = Math.Atan2(Nextmonster.Actpoint.Y - Actpoint.Y, Nextmonster.Actpoint.X - Actpoint.X);
                DirX = (12 * Math.Cos(angle));
                DirY = (12 * Math.Sin(angle));
            }           

            if (Chaincounter == maxchain || (Firsthited != null && Inradiusmonsters != null && Inradiusmonsters.Count == 0))
            {
                Removeable = true;            
                Firsthited = null;                       
            }
        }
    }
}
