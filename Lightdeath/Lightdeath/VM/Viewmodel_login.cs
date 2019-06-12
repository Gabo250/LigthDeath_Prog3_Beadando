using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightdeath
{
    /// <summary>
    /// login vm
    /// </summary>
    public class Viewmodel_login : Bindable
    {
        private User user;

        private Maps map;

        private StartGameScreen startgamescreen;

        private Displaying_engine display;        

        /// <summary>
        /// Gets or sets the display
        /// </summary>
        public Displaying_engine Display
        {
            get { return display; }
            set { display = value; }
        }


        /// <summary>
        /// Gets or Sets the screen
        /// </summary>
        public StartGameScreen StartgameScreen
        {
            get { return startgamescreen; }
            set { startgamescreen = value; }
        }


        /// <summary>
        /// cons of the login
        /// </summary>
        public Viewmodel_login()
        {
            this.user = new User();
        }

        /// <summary>
        /// Gets or sets user
        /// </summary>
        public User User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
                Onpropertychange();
            }
        }

        /// <summary>
        /// Gets or sets map
        /// </summary>
        public Maps Map
        {
            get { return map; }
            set { map = value; }
        }      
    }
}
