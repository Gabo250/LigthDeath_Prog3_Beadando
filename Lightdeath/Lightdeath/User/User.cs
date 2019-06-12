using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// users for game
    /// </summary>
    public class User : Bindable
    {
        private string accname;

        private string passwd;

        private Character_classes chars;

        private DataRead_Write rw;

        /// <summary>
        /// user cons
        /// </summary>
        /// <param name="accname">accname of user</param>
        /// <param name="passwd">passwd of user</param>
        public User(string accname, string passwd)
        {
            this.accname = accname;
            this.passwd = passwd;
        }

        /// <summary>
        /// empty cons
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Gets or sets account name of user
        /// </summary>                                                       
        public string Accname
        {
            get
            {
                return this.accname;
            }

            set
            {
                this.accname = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets password of user account
        /// </summary>                                       
        public string Passwd
        {
            get
            {
                return this.passwd;
            }

            set
            {
                this.passwd = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets characters of user
        /// </summary>
        public Character_classes Chars
        {
            get
            {
                return this.chars;
            }

            set
            {
                this.chars = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// if already exists an acc
        /// </summary>
        /// <returns>the haveacc</returns>
        public bool Haveacc()
        {
            if (File.Exists(this.accname + ".txt") && (this.passwd + "\r").Equals((rw = new DataRead_Write(this.accname + ".txt", this.passwd)).Passwd))
            {
                this.chars = rw.Readed;
                return true;
            }
            else if (File.Exists(this.accname + ".txt") && !(this.passwd + "\r").Equals(rw.Passwd))
            {
                throw new IncorrectAcc_username();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// if already exist this acc
        /// </summary>
        public void Createacc()
        {
            if (File.Exists(this.accname + ".txt"))
            {
                throw new Account_already_have();
            }
            else
            {
                chars = new DarkMage();
            }
        }

        /// <summary>
        /// save account info to txt file
        /// </summary>
        public void Save()
        {
            rw = new DataRead_Write(accname + ".txt", chars, accname, passwd);
        }
    }
}
