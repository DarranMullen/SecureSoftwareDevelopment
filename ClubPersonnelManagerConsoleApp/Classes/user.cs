using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    /// <summary>
    /// A User has a username, password, IsAdmin is is an administrator and Authentiacted if authenticated.
    /// The user class does not inherit from the person class like Player ad Staff does.
    /// The user class is concerned with, adding a new user, logging in and out, and exiting the program.
    /// </summary>
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool Authenticated { get; set; }

        //Constructor
        public User(){}

        /// <summary>
        /// Provide prompts to get the data needed to add a new user
        /// </summary>
        public void AddUser()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            IEnumerable<string> users = File.ReadLines(Globals.Auth.Dir + Constants.USER_CSV_FILE);
            //check if user exists
            foreach (var user in users)
            {
                if (user.Split(',')[0].Equals(name))
                {
                    Console.WriteLine("\nERROR: Username Exists.");
                    AddUser();
                }
            }

            Console.Write("Password: ");
            string pass = "";
            //get password securely
            GetPassword(ref pass);
            //invalid if less than 16 chars
            if(pass.Length < 16) 
            {
                Console.WriteLine("\nMin of 8 characters.");
                Console.Write("Password: ");
                pass = "";
                GetPassword(ref pass);
            }
            else
            {
                //invalid if does not contain at least 1 upper case letter
                if (!pass.Any(char.IsUpper))
                {
                    Console.WriteLine("\nMin of 1 upper case character.");
                    Console.Write("Password: ");
                    pass = "";
                    GetPassword(ref pass);
                }
                else
                {
                    //invalid if does not include at least 1 lower case letter
                    if (!pass.Any(char.IsLower))
                    {
                        Console.WriteLine("\nMin of 1 lower case character.");
                        Console.Write("Password: ");
                        pass = "";
                        GetPassword(ref pass);
                    }
                    else
                    {
                        //invalid if does not contain at least 1 special character or number
                        if ((!pass.Any(char.IsSymbol) && (!pass.Any(char.IsNumber))))
                        {
                            Console.WriteLine("\nMin of 1 special character.");
                            Console.Write("Password: ");
                            pass = "";
                            GetPassword(ref pass);
                        }
                        else
                        {
                            //valid password: insecure example: Password1Secure1, secure example: wvEya6EVmkiyFmfV
                            //hash the password
                            Globals.Auth.HashPassword(ref pass, true);
                            //Auth auth = new Auth();
                            //auth.HashPassword(ref pass, true);
                        }
                    }
                }
            }
            Console.Write("Admin? (Y/N) :");
            ConsoleKey key = Console.ReadKey().Key;
            bool isAdmin = false;
            if (key == ConsoleKey.Y)
            {
                isAdmin = true;
            }
            else if (key == ConsoleKey.N)
            {
                isAdmin = false;
            }
            else
            {
                Console.WriteLine("ERROR: Try Again.");
                AddUser();
            }
            //Add USER TO DB
            AddUserToCSV(name,pass,isAdmin);
        }

        private void AddUserToCSV(string name, string pass, bool isAdmin)
        {
            string file = Globals.Auth.Dir + Constants.USER_CSV_FILE;
            string line = string.Format("{0},{1},{2}\n", name, pass, isAdmin);
            string[] lines;
            try
            {
                File.AppendAllText(file, line);
                lines = File.ReadAllLines(file);
                Array.Sort(lines);
                File.WriteAllLines(file, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Login()
        {
            if (!Globals.User.Authenticated)
            {
                Console.Write("Username: ");
                Globals.UserInput.Parameters.Add(Console.ReadLine());
                Console.Write("Password: ");
                string pass = "";
                GetPassword(ref pass);
                Globals.Auth.HashPassword(ref pass, false);
                GC.Collect();
                Globals.UserInput.Parameters.Add(pass);
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

        private void GetPassword(ref string pass)
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
        }

        private void ValidateUser()
        {
            IEnumerable<string> users = File.ReadLines(Globals.Auth.Dir + Constants.USER_CSV_FILE);
            foreach (var user in users)
            {
                if (user.Split(',')[0].Equals(Globals.UserInput.Parameters[0]) && user.Split(',')[1].Equals(Globals.User.Password))
                {
                    Globals.User.Authenticated = true;
                    Globals.User.Username = Globals.UserInput.Parameters[0];
                    if (bool.TryParse(user.Split(',')[2], out bool isAdmin))
                        Globals.User.IsAdmin = bool.Parse((user.Split(',')[2]));
                    Console.WriteLine("\nLogin Successful");
                    break;
                }
            }
        }

        private void HashPassword(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Globals.Auth.Salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(Globals.Auth.Salt, 0, hashBytes, 0, 16);
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
