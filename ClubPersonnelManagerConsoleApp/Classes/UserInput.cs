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
        /// <summary>
        /// The valid commands
        /// </summary>
        public enum Commands
        {
            none,//done
            login,//done
            logout,//done
            exit,//done
            help,//TODO help
            add,//TODO add staff
            find,//TODO find player and statt
            edit,//TODO edit player and staff
            delete//TODO delete player and staff
        }

        /// <summary>
        /// what the user enters in console
        /// </summary>
        public string RawText { get; set; }

        /// <summary>
        /// the raw text split into array 
        /// </summary>
        public string[] RawTextArr { get; set; }

        /// <summary>
        /// raw text is converted into a Command
        /// </summary>
        public Commands Command { get; set; }

        /// <summary>
        /// Options are not required
        /// </summary>
        public Dictionary<string, string> Options { get; set; }
        
        /// <summary>
        /// Parameters are required
        /// </summary>
        public List<string> Parameters { get; set; }
        
        /// <summary>
/// Constructor
/// </summary>
        public UserInput()
        {
            this.Options = new Dictionary<string, string>();
            this.Parameters = new List<string>();
            
            Console.Write('>');
            this.RawText = Console.ReadLine();
            this.RawTextArr = this.RawText.Split(' ');
            GetCommandName();
            ProcessCommand();
        }

        /// <summary>
        /// get the command and parse it against commands enum
        /// </summary>
        public void GetCommandName()
        {
            string strCommand = string.Empty;

            if (!this.RawText.Equals(strCommand))
                strCommand = this.RawTextArr[0];

            Enum.TryParse(strCommand, out Commands command);
            this.Command = command;
        }

        /// <summary>
        /// switch to run correct command method
        /// </summary>
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
                case Commands.add:
                    Add();
                    break;
                case Commands.find:
                    break;
                case Commands.edit:
                    break;
                case Commands.delete:
                    break;
                default:
                    None();
                    break;
            }
        }

        /// <summary>
        /// add [Firstname.]Lastname {-p position number|-s role}
        /// e.g. Staff
        /// add Jurgen.Klopp -s m
        /// </summary>
        private void Add()
        {
            if (Globals.User.IsAdmin)
            {
                if (this.RawTextArr[2] == "-p") 
                {
                    AddPlayer();
                }
                else if (this.RawTextArr[2] == "-s")
                {
                    AddStaff();
                }
                else
                    Console.WriteLine("Error: incorrect syntax");

            }
            else
                Console.WriteLine("Error: Only admins can add personnel");
        }

        /// <summary>
        /// add [Firstname.]Lastname -p position squadnumber
        /// e.g.
        /// add Allison.Becker -p g 1
        /// </summary>
        private void AddPlayer()
        {
            Player p = new Player();
            p.GetName(this);
            p.GetPosition(this);
            p.GetSquadNumber(this);
            p.AddPlayer();
           
        }

        /// <summary>
        /// logout [e]
        /// </summary>
        private void Logout()
        {
            if (Globals.User.Authenticated)
            {
                if (this.RawTextArr.Length == 1)
                {
                    Globals.User.Authenticated = false;
                    Globals.User.IsAdmin = false;
                    Globals.User.Username = string.Empty;
                    Globals.User.Password = string.Empty;
                }
                else if ((this.RawTextArr.Length == 2) && (this.RawTextArr[1] == "e")) 
                    Exit();
                else
                    Console.WriteLine("Error: incorrect syntax");
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
                    Console.WriteLine("Error: incorrect syntax");
                }
                else
                    for (int i = 1; i < arr.Length; i++)
                        this.Parameters.Add(arr[i]);

            }
            else
            {
                Console.WriteLine("Error: user already logged in.");
                Console.Write("Would you like to logout now? (y/n): ");
                
                if (Console.ReadKey().Key==ConsoleKey.Y)
                {
                    Logout();
                }
            }
            Globals.User.Login(this);
        }

        /// <summary>
        /// exit
        /// </summary>
        private void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// ERROR MESSAGE
        /// </summary>
        private void None()
        {
            Console.WriteLine("Error: Command not found");
        }

        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            this.RawText = string.Empty;
            this.Command = Commands.none;
            this.Options.Clear();
            this.Parameters.Clear();

        }
    } 
}
