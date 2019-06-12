using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// bosses of game
    /// </summary>
    public abstract class Bosses : Monsters
    {
        /// <summary>
        /// bosses cons
        /// </summary>
        /// <param name="name">name of monster</param>
        /// <param name="lvl">lvl of monster</param>
        /// <param name="dmg">dmg of monster</param>
        /// <param name="hp">hp point of monster</param>
        /// <param name="mp">mp of monster</param>
        /// <param name="defense">defense of monster</param>
        /// <param name="x">x cordinate</param>
        /// <param name="y">y cordinate</param>
        /// <param name="map">the map</param>
        /// <param name="chare">the character</param>
        /// <param name="giveexp">the given exp</param>
        public Bosses(string name, int lvl, int dmg, int hp, int mp, int defense, double x, double y, Maps map, Character_classes chare, int giveexp) : base(hp, mp, defense, dmg, lvl, name, x, y, map, chare, giveexp)
        {
        }
    }
}
