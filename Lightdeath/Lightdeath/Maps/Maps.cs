using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// maps of project
    /// </summary>
    public class Maps : Bindable
    {
        private int width;

        private int height;

        private Character_classes player;

        private List<Monsters> monsters;

        private List<Skills> skill;

        private List<Potion> potions;

        /// <summary>
        /// map cons
        /// </summary>
        /// <param name="width">map width</param>
        /// <param name="height">map height</param>
        /// <param name="player">map player character</param>
        public Maps(int width, int height, Character_classes player)
        {
            this.width = width;
            this.height = height;
            this.player = player;
            this.monsters = new List<Monsters>();
            this.skill = new List<Skills>();
            this.potions = new List<Potion>();
            this.Removeelement = new List<object>();
        }

        /// <summary>
        /// Gets or sets width of map
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets height of map
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets monsters of map
        /// </summary>
        public List<Monsters> Monsters
        {
            get
            {
                return this.monsters;
            }

            set
            {
                this.monsters = value;               
            }
        }

        /// <summary>
        /// Gets or sets the skill
        /// </summary>
        public List<Skills> Skill
        {
            get
            {
                return skill;
            }

            set
            {
                skill = value;               
            }
        }

        /// <summary>
        /// Gets or sets the potions list
        /// </summary>
        public List<Potion> Potions
        {
            get { return potions; }
            set { potions = value; }
        }

        /// <summary>
        /// Gets or sets the remove elemnt list
        /// </summary>
        public List<object> Removeelement
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets player character of map
        /// </summary>
        public Character_classes Player
        {
            get
            {
                return this.player;
            }

            set
            {
                this.player = value;
            }
        }

        /// <summary>
        /// monsters in distance
        /// </summary>
        /// <param name="distance">distance of monster</param>
        /// <param name="x">x cordinate of object</param>
        /// <param name="y">y cordinate of object</param>
        /// <returns>monsters list</returns>
        public List<Monsters> Indistance(double distance, double x, double y)
        {
            List<Monsters> mons = new List<Monsters>();
            foreach (Monsters mon in monsters)
            {        
                if (Math.Sqrt(Math.Pow(mon.Actpoint.X - x, 2) + Math.Pow(mon.Actpoint.Y - y, 2)) <= distance)
                {
                    mons.Add(mon);
                }
            }

            return mons;
        }
    }
}
