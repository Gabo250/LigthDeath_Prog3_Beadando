using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// vm_charstat class
    /// </summary>
    public class Viewmodel_charstatview : Bindable
    {
        private Character_classes aktchar;

        /// <summary>
        /// vm cons
        /// </summary>
        /// <param name="aktchar">aktchar of the statview</param>
        public Viewmodel_charstatview(Character_classes aktchar)
        {
            this.aktchar = aktchar;
        }

        /// <summary>
        /// Gets or sets actual character
        /// </summary>
        public Character_classes Aktchar
        {
            get
            {
                return this.aktchar;
            }

            set
            {
                this.aktchar = value;
                Onpropertychange();
            }
        }
    }
}
