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
        /// Valid commands
        /// </summary>
        public enum Commands
        {
            none,//done
            login,//done
            logout,//done
            exit,//done
            help,//done
            add,//done
            find,
            edit,
            delete//done
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
            if (Globals.User.Authenticated)
                Console.Write("{0}> ",Globals.User.Username);
            else
                Console.Write("> ");
            this.RawText = Console.ReadLine().Trim();
            if (this.RawText != "") 
            {
                this.RawTextArr = this.RawText.Split(' ');
                GetCommandName();
                ProcessCommand();
            }
            
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
                    Help();
                    break;
                case Commands.add:
                    Add();
                    break;
                case Commands.find:
                    Find();
                    break;
                case Commands.edit:
                    Edit();
                    break;
                case Commands.delete:
                    Delete();
                    break;
                default:
                    None();
                    break;
            }
        }
        //TODO EDIT
        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Find()
        {
            Person p = new Person();
            p.FindPerson(this.RawTextArr[1][1], this.RawTextArr[2]);
        }

        /// <summary>
        /// delete {-s|-p} squadnumber
        /// </summary>
        private void Delete()
        {
            if (Globals.User.IsAdmin)
            {
                DeletePerson();
            }
            else
                Console.WriteLine("Error: Only admins can delete personnel");
        }

        private void DeletePerson()
        {
            if (int.TryParse(this.RawTextArr[2], out int id))
            {
                Person p = new Person();
                p.DeletePerson(id, this.RawTextArr[1][1]);
            }
        }

        /// <summary>
        /// show valid commands
        /// </summary>
        private void Help()
        {
            if (RawTextArr.Length == 2) 
            {
                Enum.TryParse(RawTextArr[1], out Commands command);

                switch (command)
                {
                    case Commands.none:
                        break;
                    case Commands.login:
                        Console.WriteLine(Constants.LOGIN_SYNTAX);
                        break;
                    case Commands.logout:
                        Console.WriteLine(Constants.LOGOUT_SYNTAX);
                        break;
                    case Commands.exit:
                        Console.WriteLine(Constants.EXIT_SYNTAX);
                        break;
                    case Commands.help:
                        Console.WriteLine(Constants.HELP_SYNTAX);
                        break;
                    case Commands.add:
                        Console.WriteLine(Constants.ADD_SYNTAX);
                        break;
                    case Commands.find:
                        Console.WriteLine(Constants.FIND_SYNTAX);
                        break;
                    case Commands.edit:
                        Console.WriteLine(Constants.EDIT_SYNTAX);
                        break;
                    case Commands.delete:
                        Console.WriteLine(Constants.DELETE_SYNTAX);
                        break;
                    default:
                        Console.WriteLine("!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Valid Commands");
                foreach (var c in Enum.GetValues(typeof(Commands)))
                    if (c.ToString() != "none")
                        Console.WriteLine(c.ToString());
            }
            
        }

        /// <summary>
        /// add [Firstname.]Lastname {-p position number|-s role}
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
        /// add [Firstname.]Lastname -s role
        /// e.g.
        /// add Jurgen.Klopp -s m 
        /// </summary>
        private void AddStaff()
        {
            Staff s = new Staff();
            s.GetName(this);
            s.GetRole(this);
            s.AddStaff();
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
            else
            {
                Console.WriteLine("Error: not logged in");
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
