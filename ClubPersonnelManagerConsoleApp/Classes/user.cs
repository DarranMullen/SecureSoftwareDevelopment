using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public User()
        {
            Auth auth = new Auth();
            Globals.Auth = auth;
        }

        public void Login()
        {
            if (!Globals.User.Authenticated)
            {
                ///
                //string[] arr = Globals.UserInput.RawText.Split(' ');
                if (Globals.UserInput.RawTextArr.Length != 2)
                    Console.WriteLine(Constants.SYNTAX_ERROR);
                else
                {
                    Globals.UserInput.Parameters.Add(Globals.UserInput.RawTextArr[1]);

                    try
                    {
                        ValidateUser();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }    
                }
            }
            else
                LoginError();
        }

        private void ValidateUser()
        {
            IEnumerable<string> users = File.ReadLines(Constants.USER_CSV_FILE);
            foreach (var user in users)
            {
                if (user.Split(',')[0].Equals(Globals.UserInput.Parameters[0]) && user.Split(',')[1].Equals(Globals.User.Password))
                {
                    Globals.User.Authenticated = true;
                    Globals.User.Username = Globals.UserInput.Parameters[0];
                    if (bool.TryParse(user.Split(',')[2], out bool isAdmin))
                        Globals.User.IsAdmin = bool.Parse((user.Split(',')[2]));
                    Console.WriteLine("Login Successful");
                    break;
                }
            }
        }

        private void HashPassword(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Globals.Auth.salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(Globals.Auth.salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            Globals.UserInput.Parameters[1] = Convert.ToBase64String(hashBytes);
            Globals.User.Password = Globals.UserInput.Parameters[1];
        }

        private void LoginError()
        {
            Console.WriteLine(Constants.LOGIN_ERROR);
            Console.Write(Constants.PROMPT_LOGOUT);
            if (Console.ReadKey().Key == ConsoleKey.Y)
                Logout();
        }

        public void Logout()
        {
            if (Globals.User.Authenticated)
            {
                if (Globals.UserInput.RawTextArr.Length == 1)
                    Globals.User = null;
                else if ((Globals.UserInput.RawTextArr.Length == 2) && (Globals.UserInput.RawTextArr[1] == "e"))
                    Exit();
                else
                    Console.WriteLine(Constants.SYNTAX_ERROR);
            }
            else
                Console.WriteLine("Error: not logged in");
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
