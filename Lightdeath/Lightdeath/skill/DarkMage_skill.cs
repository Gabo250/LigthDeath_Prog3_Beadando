using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// dark ksill
    /// </summary>
    public abstract class DarkMage_skill : Skills
    {
        /// <summary>
        /// cons of darmage skill
        /// </summary>
        /// <param name="dmg">dmg of skill</param>
        /// <param name="cost">cost of spell</param>
        /// <param name="map">actual map</param>
        public DarkMage_skill(int dmg, int cost, Maps map) : base(dmg, cost, map)
        {
        }
    }
}
