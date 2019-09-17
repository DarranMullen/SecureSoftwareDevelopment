using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{

    class UserInput : IUserInput
    {
        public enum Commands
        {
            none,
            login,
            logout,
            exit,
            help,
            create,
            search,
            update,
            delete
        }

        public string RawText { get; set; }
        public Commands Command { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public List<string> Parameters { get; set; }

        //Constructor
        public UserInput()
        {
            this.Options = new Dictionary<string, string>();
            this.Parameters = new List<string>();

            Console.Write('>');
            this.RawText = Console.ReadLine();
            GetCommandName();
            ProcessCommand();
        }

        public void GetCommandName()
        {
            string strCommand = string.Empty;

            if (!this.RawText.Equals(strCommand))
            {
                strCommand = this.RawText.Split(' ')[0];
            }

            Enum.TryParse(strCommand, out Commands command);
            this.Command = command;
        }

        public void ProcessCommand()
        {
            switch (this.Command)
            {
                case Commands.none:
                    None();
                    break;
                case Commands.login:
                    Login();
                    break;
                case Commands.logout:
                    Logout();
                    break;
                case Commands.exit:
                    Exit();
                    break;
                case Commands.help:
                    break;
                case Commands.create:
                    break;
                case Commands.search:
                    break;
                case Commands.update:
                    break;
                case Commands.delete:
                    break;
                default:
                    break;
            }
        }

        private void Logout()
        {
            if (Globals.User.Authenticated)
            {
                Globals.User.Authenticated = false;
                Globals.User.IsAdmin = false;
                Globals.User.Username = string.Empty;
                Globals.User.Password = string.Empty;
            }
        }

        /// <summary>
        /// login username password
        /// </summary>
        private void Login()
        {
            if (!Globals.User.Authenticated)
            {
                string[] arr = this.RawText.Split(' ');
                if (arr.Length != 3)
                {
                    Console.WriteLine("Error: correct login syntax is 'login username password'");
                }
                else
                    for (int i = 1; i < arr.Length; i++)
                        this.Parameters.Add(arr[i]);

            }
            else
            {
                Console.WriteLine("Already logged in.");
            }
            Globals.User.Login(this);
        }

        private void Exit()
        {
            Environment.Exit(0);
        }

        private void None()
        {
            Console.WriteLine("Error: Command not found");
        }

        public void Clear()
        {
            this.RawText = string.Empty;
            this.Command = Commands.none;
            this.Options.Clear();
            this.Parameters.Clear();

        }
    } 
}
