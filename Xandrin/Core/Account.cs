using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.Core
{
    public class Account
    {

        private string username;
        private string password;
        private int isLoggedIn;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }

        public Account(string username, string password, int isLoggedIn)
        {
            this.username = username;
            this.password = password;
            this.isLoggedIn = isLoggedIn;
        }

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public Account()
        {

        }

    }
}
