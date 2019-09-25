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
            Globals.UserInput = this;

            Globals.UserInput.Options = new Dictionary<string, string>();
            Globals.UserInput.Parameters = new List<string>();


            if (Globals.User.Authenticated)
                Console.Write("{0}> ",Globals.User.Username);
            else
                Console.Write("> ");
            Globals.UserInput.RawText = Console.ReadLine().Trim();


            if (Globals.UserInput.RawText != "") 
            {
                Globals.UserInput.RawTextArr = Globals.UserInput.RawText.Split(' ');
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

            if (!Globals.UserInput.RawText.Equals(strCommand))
                strCommand = Globals.UserInput.RawTextArr[0];

            Enum.TryParse(strCommand, out Commands command);
            Globals.UserInput.Command = command;
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
                    Globals.User.Login();
                    break;
                case Commands.logout:
                    Globals.User.Logout();
                    break;
                case Commands.exit:
                    Globals.User.Exit();
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
            bool error = false;
            for (int i = 0; i < Globals.UserInput.RawTextArr.Length; i++)
            {
                if (Globals.UserInput.RawTextArr[i].Contains(':'))
                {
                    string key = Globals.UserInput.RawTextArr[i].Split(':')[0];
                    string value = Globals.UserInput.RawTextArr[i].Split(':')[1];
                    Globals.UserInput.Options.Add(key, value);
                }
            }
            if (Globals.UserInput.Options == null)
            {
                Console.WriteLine("ERROR");
                error = true;
            }

            if (!error)
            {
                if (Globals.User.IsAdmin)
                {
                    Globals.Person = new Person();
                    Globals.Person.FindPersonById();
                }
                else
                {
                    Console.WriteLine("ADMIN ONLY");
                }

                if (Globals.UserInput.RawTextArr[1][1] == 'p')
                {
                    Globals.Player.EditPlayer();
                }
                else if (Globals.UserInput.RawTextArr[1][1] == 's')
                {
                    Globals.Staff.EditStaff();
                }
            }

            
        }

        private void Find()
        {
            Globals.Person = new Person();
            Globals.Person.FindPersonByName();
        }

        /// <summary>
        /// delete {-s|-p} squadnumber
        /// </summary>
        private void Delete()
        {
            if (Globals.User.IsAdmin)
            {
                Globals.Person = new Person();
                Globals.Person.DeletePerson();
            }
            else
                Console.WriteLine("Error: Only admins can delete personnel");
        }

        /// <summary>
        /// show valid commands
        /// </summary>
        private void Help()
        {
            if (Globals.UserInput.RawTextArr.Length == 2) 
            {
                Enum.TryParse(Globals.UserInput.RawTextArr[1], out Commands command);

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
                if (Globals.UserInput.RawTextArr[2] == "-p")
                {
                    Globals.Player = new Player();
                    Globals.Player.AddPlayer();
                }
                else if (Globals.UserInput.RawTextArr[2] == "-s")
                {
                    Globals.Staff = new Staff();
                    Globals.Staff.AddStaff();
                }
                else
                    Console.WriteLine("Error: incorrect syntax");
            }
            else
                Console.WriteLine("Error: Only admins can add personnel");
        }

        /// <summary>
        /// ERROR MESSAGE
        /// </summary>
        private void None()
        {
            Console.WriteLine("Error: Command not found");
        }

    } 
}
