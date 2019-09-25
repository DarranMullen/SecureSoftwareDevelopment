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

        public void Login()
        {
            if (!Globals.User.Authenticated)
            {
                string[] arr = Globals.UserInput.RawText.Split(' ');
                if (arr.Length != 3)
                    Console.WriteLine("Error: incorrect syntax");
                else
                {
                    for (int i = 1; i < arr.Length; i++)
                        Globals.UserInput.Parameters.Add(arr[i]);
                    var users = File.ReadLines(Constants.USER_CSV_FILE);//TODO wrap in try
                    foreach (var user in users)
                    {
                        if (user.Split(',')[0].Equals(Globals.UserInput.Parameters[0]) && user.Split(',')[1].Equals(Globals.UserInput.Parameters[1]))
                        {
                            Globals.User.Authenticated = true;
                            Globals.User.Username = Globals.UserInput.Parameters[0];
                            Globals.User.Password = Globals.UserInput.Parameters[1];
                            if (bool.TryParse(user.Split(',')[2], out bool isAdmin))
                                Globals.User.IsAdmin = bool.Parse((user.Split(',')[2]));
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: user already logged in.");
                Console.Write("Would you like to logout now? (y/n): ");

                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Logout();
                }
            }

        }

        public void Logout()
        {
            if (Globals.User.Authenticated)
            {
                if (Globals.UserInput.RawTextArr.Length == 1)
                {
                    Globals.User = null;
                }
                else if ((Globals.UserInput.RawTextArr.Length == 2) && (Globals.UserInput.RawTextArr[1] == "e"))
                    Exit();
                else
                    Console.WriteLine("Error: incorrect syntax");
            }
            else
            {
                Console.WriteLine("Error: not logged in");
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
