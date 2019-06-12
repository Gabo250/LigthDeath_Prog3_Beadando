using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lightdeath
{
    /// <summary>
    /// darkmage class
    /// </summary>
    public class DarkMage : Character_classes
    {
        private int darkballskillpoint;

        private int lightingskillpoint;

        private int ionshieldskillpoint;

        private int contractskillpoint;

        /// <summary>
        /// mage cons
        /// </summary>       
        public DarkMage() : base(150, 150, 10, 1.5, 0.1, 20, 8, 18, 12, 10, 15, 1, 0)
        {            
        }
        ////properties//
        #region
        /// <summary>
        /// Gets or sets Lighting skill point
        /// </summary>
        public int LightingSkillPoint
        {
            get
            {
                return lightingskillpoint;
            }

            set
            {
                lightingskillpoint = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets ion shield skill point
        /// </summary>
        public int IonShieldSkillPoint
        {
            get
            {
                return ionshieldskillpoint;
            }

            set
            {
                ionshieldskillpoint = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the darkball skill point
        /// </summary>
        public int DarkBallSkillPoint
        {
            get
            {
                return darkballskillpoint;
            }

            set
            {
                darkballskillpoint = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets contract skill
        /// </summary>
        public int ContractSkillPoint
        {
            get
            {
                return contractskillpoint;
            }

            set
            {
                contractskillpoint = value;
                Onpropertychange();
            }
        }
        #endregion
        /// <summary>
        /// get damage
        /// </summary>
        /// <param name="value">damage value</param>
        public override void Getdmg(int value)
        {
            int calcdmg = value - (int)(0.1 * Def);
            Damagetaken += calcdmg;
            if (HP - calcdmg >= 0 && Alive)
            {
                HP -= calcdmg;
            }
            else if (HP - calcdmg < 0 && Alive)
            {
                HP -= calcdmg - (calcdmg - HP);
                Alive = false;
            }
        }
        ////methods//
        #region
        /// <summary>
        /// move character
        /// </summary>
        /// <param name="trans">the transformations</param>
        public override void Move(Transform trans)
        {
            this.Geometry.Transform = trans;
            this.Geometry = this.Geometry.GetFlattenedPathGeometry();
        }

        /// <summary>
        /// crash or no
        /// </summary>
        /// <param name="rect">the rect</param>
        /// <returns>the value</returns>
        public bool Crash(Rect rect)
        {
            if (this.Geometry.Bounds.IntersectsWith(rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// refresh hp, mp and geometry
        /// </summary>
        public void Refresh()
        {
            EllipseGeometry rg = new EllipseGeometry(new Point(60, 350), 30, 35);
            Geometry = rg;
            HP = MaxHP;
            Alive = true;
            Resource = MaxResource;
            Onpropertychange("HP");
            Onpropertychange("Resource");
        }

        /// <summary>
        /// a critical rate tizedes pont utáni számjegyre emeli a 10-et, ez critical hut számitáshoz kell
        /// </summary>
        /// <returns>the denominator</returns>
        public int Critdenominator()
        {
            if (CritRate < 1)
            {
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                string s = CritRate.ToString("N", nfi);              
                string[] afterdot = s.Split('.');
                
                int sz = afterdot[1].Length;
                double denominator = Math.Pow(10, sz);
                return (int)denominator;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// a critical rate tizedes pont utáni száőmot adja vissza
        /// </summary>
        /// <returns>the counter</returns>
        public int Counter()
        {
            if (CritRate < 1)
            {
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                string s = CritRate.ToString("N", nfi);
                string[] afterdot = s.Split('.');
                return int.Parse(afterdot[1]);
            }
            else
            {
                return 1;
            }         
        }
        #endregion
    }
}
