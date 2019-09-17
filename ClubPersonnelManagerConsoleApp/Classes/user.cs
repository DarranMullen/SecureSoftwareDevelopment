using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class User : IUser
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool Authenticated { get; set; }

        //Constructor
        public User()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.IsAdmin = false;
            this.Authenticated = false;
        }

        public void Login(UserInput userInput)
        {
            var users = File.ReadLines(Constants.USER_CSV_FILE);
            foreach (var user in users)
            {
                if (user.Split(',')[0].Equals(userInput.Parameters[0]) && user.Split(',')[1].Equals(userInput.Parameters[1]))
                {
                    this.Authenticated = true;
                    this.Username = userInput.Parameters[0];
                    this.Password = userInput.Parameters[1];
                    if (bool.TryParse(user.Split(',')[2], out bool isAdmin))
                        this.IsAdmin = bool.Parse((user.Split(',')[2]));
                    break;
                }

            }

            Globals.User = this;

            if (this.Authenticated)
            {
                Console.WriteLine("YES {0}", this.IsAdmin);
            }
            else
            {
                Console.WriteLine("NO {0}", this.IsAdmin);
            }

        }
    }
}
