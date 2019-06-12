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
    /// datas of character class
    /// </summary>
    public abstract class Character_classes : Bindable
    {
        ////private varriables//
        #region
        private int hp;

        private string name;

        private int level;

        private int exp;

        private int expneed;

        private int resource;

        private double dmg;

        private double criticaldmg;

        private int defense;

        private double criticalrate;

        private bool alive;      

        private int str;

        private int intellect;

        private int vit;

        private int men;

        private int wit;

        private int skillpoint;

        private int statpoint;

        private bool skillvisible;

        private bool statvisible;

        private Geometry geometry;

        private ImageBrush image;

        private Point actpoint;

        private int killedmons;

        private int damagedealt;

        private int damagetaken;

        #endregion
        /// <summary>
        /// cons of the character class
        /// </summary>
        /// <param name="hp">the hp</param>
        /// <param name="resource">the resource</param>
        /// <param name="damage">the damage</param>
        /// <param name="criticaldmg">the ciriticaldmg</param>
        /// <param name="criticalchance">the crit chance</param>
        /// <param name="defense">the defense</param>
        /// <param name="str">the str</param>
        /// <param name="intellect">the intellect</param>
        /// <param name="vit">the vit</param>
        /// <param name="wit">the wit</param>
        /// <param name="men">the men</param>
        /// <param name="lvl">the lvl</param>
        /// <param name="exp">the exp</param>                                            
        public Character_classes(int hp, int resource, double damage, double criticaldmg, double criticalchance, int defense, int str, int intellect, int vit, int wit, int men, int lvl, int exp)
        {
            this.hp = hp;
            this.resource = resource;
            this.dmg = damage;
            this.criticaldmg = criticaldmg;
            this.criticalrate = criticalchance;
            this.defense = defense;
            this.STR = str;
            this.INT = intellect;
            this.VIT = vit;
            this.WIT = wit;
            this.Men = men;
            this.alive = true;
            this.level = lvl;
            this.exp = exp;
            this.expneed = 100;
            EllipseGeometry rg = new EllipseGeometry(new Point(60, 350), 30, 35);
            image = new ImageBrush(new BitmapImage(new Uri(@"images\Darkmage.PNG", UriKind.Relative)));
            actpoint.X = 60;
            actpoint.Y = 350;
            this.geometry = rg;
            this.MaxHP = hp;
            this.MaxResource = resource;
            statpoint = 0;
            skillpoint = 0;
        }
        ////properties//
        #region        
        /// <summary> 
        /// Gets or sets the hp of character
        /// </summary>
        public int HP
        {
            get
            {
                return this.hp;
            }

            set
            {
                this.hp = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the maxhp
        /// </summary>
        public int MaxHP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of player 
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the lvl of character
        /// </summary>
        public int LVL
        {
            get
            {
                return this.level;
            }

            set
            {
                this.level = value;
                if (exp > expneed)
                {
                    exp = exp - expneed;                   
                }
                else
                {      
                    exp = 0;
                }

                Expneed += (int)Math.Pow(level, 2) + (10 * level);
                MaxHP += 10;               
                skillpoint += 1;
                statpoint += 5;
                hp = MaxHP;
                resource = MaxResource;
                skillvisible = true;
                statvisible = true;                
                Onpropertychange();
                Onpropertychange("Skillpoint");
                Onpropertychange("Skillvisible");
                Onpropertychange("Exp");
                Onpropertychange("Statpoint");
                Onpropertychange("Statvisible");
                Onpropertychange("HP");
                Onpropertychange("MaxResource");
                Onpropertychange("MaxHP");
            }
        }

        /// <summary>
        /// Gets or sets the current experience of character
        /// </summary>
        public int Exp
        {
            get
            {
                return this.exp;
            }

            set
            {
                this.exp = value;
                if (exp >= expneed)
                {
                    LVL++;
                }

                Onpropertychange();
                Onpropertychange("LVL");
            }
        }

        /// <summary>
        /// Gets or sets the expereience need to next lvl
        /// </summary>
        public int Expneed
        {
            get
            {
                return this.expneed;
            }

            set
            {
                this.expneed = value;
            }
        }

        /// <summary>
        /// Gets or sets the max mana
        /// </summary>
        public int MaxResource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets resource point of character
        /// </summary>
        public int Resource
        {
            get
            {
                return this.resource;
            }

            set
            {
                this.resource = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the damage of characer
        /// </summary>
        public double DMG
        {
            get
            {
                return this.dmg;
            }

            set
            {
                this.dmg = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the critical damage of character
        /// </summary>
        public double CriticalDMG
        {
            get
            {
                return this.criticaldmg;
            }

            set
            {
                this.criticaldmg = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the defense of character
        /// </summary>
        public int Def
        {
            get
            {
                return this.defense;
            }

            set
            {
                this.defense = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the critical rate of character
        /// </summary>
        public double CritRate
        {
            get
            {
                return this.criticalrate;
            }

            set
            {
                this.criticalrate = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show you that character is alive
        /// </summary>
        public bool Alive
        {
            get
            {
                return this.alive;
            }

            set
            {
                this.alive = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the strength of character
        /// </summary>
        public int STR
        {
            get
            {
                return this.str;
            }

            set
            {
                this.str = value;
                this.defense += 5;
                Onpropertychange();
                Onpropertychange("Def");
            }
        }

        /// <summary>
        /// Gets or sets the intellect of character
        /// </summary>
        public int INT
        {
            get
            {
                return this.intellect;
            }

            set
            {
                this.intellect = value;
                if (this is DarkMage)
                {
                    this.dmg *= 1.02;
                }

                Onpropertychange();
                Onpropertychange("DMG");
            }
        }

        /// <summary>
        /// Gets or sets the vitality of character
        /// </summary>
        public int VIT
        {
            get
            {
                return this.vit;
            }

            set
            {
                this.vit = value;
                this.MaxHP += 3;
                Onpropertychange();
                Onpropertychange("HP");
                Onpropertychange("MaxHP");
            }
        }

        /// <summary>
        /// Gets or sets the wit of character
        /// </summary>
        public int WIT
        {
            get
            {
                return this.wit;
            }

            set
            {
                this.wit = value;
                criticaldmg += (0.05 * criticaldmg);
                if (this.criticalrate + (0.03 * criticalrate) <= 1)
                {
                    this.criticalrate += (0.03 * criticalrate);
                }
                else
                {
                    criticalrate = 1;
                }

                Onpropertychange();
                Onpropertychange("CritRate");
                Onpropertychange("CriticalDMG");
            }
        }

        /// <summary>
        /// Gets or sets the mentality of character
        /// </summary>
        public int Men
        {
            get
            {
                return this.men;
            }

            set
            {
                this.men = value;
                this.MaxResource += 10;
                Onpropertychange();
                Onpropertychange("Resource");
                Onpropertychange("MaxResource");
            }
        }      

        /// <summary>
        /// Gets or sets the skill point
        /// </summary>
        public int Skillpoint
        {
            get
            {
                return skillpoint;
            }

            set
            {
                skillpoint = value;
                if (skillpoint == 0)
                {
                    skillvisible = false;
                }

                Onpropertychange();
                Onpropertychange("Skillvisible");
            }
        }

        /// <summary>
        /// Gets or sets the statpoint
        /// </summary>
        public int Statpoint
        {
            get
            {
                return statpoint;
            }

            set
            {
                statpoint = value;
                if (statpoint == 0)
                {
                    statvisible = false;
                }

                Onpropertychange();
                Onpropertychange("Statvisible");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the "+" button visible in skill window
        /// </summary>
        public bool Skillvisible
        {
            get
            {
                return skillvisible;
            }

            set
            {
                skillvisible = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether statvisible
        /// </summary>
        public bool Statvisible
        {
            get
            {
                return statvisible;
            }

            set
            {
                statvisible = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the geometry of character
        /// </summary>
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

        /// <summary>
        /// Gets image
        /// </summary>
        public ImageBrush Image
        {
            get { return image; }
        }

        /// <summary>
        /// Gets or sets the killed mons of actual map
        /// </summary>
        public int Killedmons
        {
            get
            {
                return killedmons;
            }

            set
            {
                killedmons = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets damage dealt to mons
        /// </summary>
        public int Damagedealt
        {
            get
            {
                return damagedealt;
            }

            set
            {
                damagedealt = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the damage taken from mons
        /// </summary>
        public int Damagetaken
        {
            get
            {
                return damagetaken;
            }

            set
            {
                damagetaken = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets the actpoint of character
        /// </summary>
        public Point Actpoint
        {
            get { return actpoint; }
            set { actpoint = value; }
        }
        #endregion
        /// <summary>
        /// sebzést kap
        /// </summary>
        /// <param name="value">damage value</param>
        public abstract void Getdmg(int value);

        /// <summary>
        /// move character
        /// </summary>
        /// <param name="trans">the transformations</param>
        public abstract void Move(Transform trans);
    }
}
