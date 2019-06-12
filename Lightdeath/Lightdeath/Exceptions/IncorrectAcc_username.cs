using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// wrong passwd/username exception
    /// </summary>
    public class IncorrectAcc_username : Exception
    {
        /// <summary>
        /// cons of exception
        /// </summary>
        public IncorrectAcc_username() : base("Hibás jelszó vagy felhasználónév!!!")
        {
        }
    }
}
