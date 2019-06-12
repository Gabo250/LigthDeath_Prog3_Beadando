using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// már létezik az adott account
    /// </summary>
    public class Account_already_have : Exception
    {
        /// <summary>
        /// továbbitja az üzenetet az ősnek
        /// </summary>
        public Account_already_have() : base("Ez a account már létezik!")
        {
        }
    }
}
