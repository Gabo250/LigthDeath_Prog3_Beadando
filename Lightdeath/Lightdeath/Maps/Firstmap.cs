using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// first map
    /// </summary>
    public class Firstmap : Maps
    {
        private Bosses boss;

        /// <summary>
        /// first map constructor
        /// </summary>
        /// <param name="width">width of map</param>
        /// <param name="height">height of map</param>
        /// <param name="player">player of map</param>
        public Firstmap(int width, int height, Character_classes player) : base(width, height, player)
        {
        }

        /// <summary>
        /// Gets or sets the naratos boss
        /// </summary>
        public Bosses Boss
        {
            get { return boss; }
            set { boss = value; }
        }
    }
}
