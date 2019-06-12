using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// monsters of game
    /// </summary>
    public class Monsters : Bindable
    {
        private string name;

        private int hp;

        private int mp;

        private int defense;

        private int damage;

        private int lvl;

        private int giveexp;

        private bool alive;

        private double dirX;

        private double dirY;

        private Point actpoint;

        private Geometry geometry;

        private ImageBrush image;

        private Maps map;

        private Character_classes chare;

        private DispatcherTimer timer;

        private bool hitable;

        /// <summary>
        /// monsters cons
        /// </summary>
        /// <param name="hp">hp of monster</param>
        /// <param name="mp">mp of monster</param>
        /// <param name="defense">defense of monster</param>
        /// <param name="damage">damage of monster</param>
        /// <param name="lvl">lvl of monster</param>
        /// <param name="name">name of monster</param>
        /// <param name="x">x codinate</param>
        /// <param name="y">y cordinate</param>
        /// <param name="map">the map</param>
        /// <param name="chare">the character</param>
        /// <param name="giveexp">the given exp</param>
        public Monsters(int hp, int mp, int defense, int damage, int lvl, string name, double x, double y, Maps map, Character_classes chare, int giveexp)
        {
            this.hp = hp;
            this.mp = mp;
            this.defense = defense;
            this.damage = damage;
            this.lvl = lvl;
            this.name = name;
            this.alive = true;
            this.map = map;
            this.chare = chare;
            this.giveexp = giveexp;
            this.MaxHP = hp;
            timer = new DispatcherTimer();
            hitable = false;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }        

        /// <summary>
        /// Gets or sets name of monster
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
        /// Gets or sets health point of monster
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
        /// Gets or sets mana point of monster
        /// </summary>
        public int MP
        {
            get
            {
                return this.mp;
            }

            set
            {
                this.mp = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets defense of monster
        /// </summary>
        public int Defense
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
        /// Gets or sets damage of monster
        /// </summary>
        public int DMG
        {
            get
            {
                return this.damage;
            }

            set
            {
                this.damage = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets level of monster
        /// </summary>
        public int Lvl
        {
            get
            {
                return this.lvl;
            }

            set
            {
                this.lvl = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show you that monster is alive or no
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
                chare.Killedmons++;
            }
        }

        /// <summary>
        /// Gets or sets give exp the character
        /// </summary>
        public int Giveexp
        {
            get { return giveexp; }
            set { giveexp = value; }
        }

        /// <summary>
        /// Gets or sets max hp
        /// </summary>
        public int MaxHP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dirx
        /// </summary>
        public double DirX
        {
            get { return dirX; }
            set { dirX = value; }
        }

        /// <summary>
        /// Gets or sets the diry
        /// </summary>
        public double DirY
        {
            get { return dirY; }
            set { dirY = value; }
        }

        /// <summary>
        /// Gets or sets the actpoint
        /// </summary>
        public Point Actpoint
        {
            get { return actpoint; }
            set { actpoint = value; }
        }

        /// <summary>
        /// Gets or sets the geometry of monster
        /// </summary>
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

        /// <summary>
        /// Gets or sets image
        /// </summary>
        public ImageBrush Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Maps Map
        {
            get { return map; }
            set { map = value; }
        }       

        /// <summary>
        /// sebzést kap
        /// </summary>
        /// <param name="value">damage value</param>
        public void Getdmg(int value)
        {
            chare.Damagedealt += value;
            int calcdmg = value - (int)(0.05 * Defense);
            if (HP - calcdmg >= 0 && Alive)
            {
                HP -= calcdmg;
            }
            else if (HP - calcdmg < 0 && Alive)
            {
                HP -= calcdmg - (calcdmg - HP);
                Alive = false;
                chare.Exp += giveexp;
            }
        }

        /// <summary>
        /// move monster
        /// </summary>
        /// <param name="trans">the transform</param>
        public void Monsmove(Transform trans)
        {
            this.Geometry.Transform = trans;
            this.Geometry = Geometry.GetFlattenedPathGeometry();
            Actpoint = new Point(Actpoint.X + DirX, Actpoint.Y + DirY);
        }

        /// <summary>
        /// Hit player
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="damage">damage value</param>
        public void Hit(Character_classes player, int damage)
        {
            if (hitable && alive)
            {
                player.Getdmg(damage);
                hitable = false;
            }
            else if (!alive)
            {
                timer.Stop();
            }           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            hitable = !hitable;
        }
    }
}
