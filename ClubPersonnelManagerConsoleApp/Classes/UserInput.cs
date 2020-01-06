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
            none,
            login,
            logout,
            exit,
            help,
            add,
            find,
            edit,
            delete
        }

        public string RawText { get; set; }
        public string[] RawTextArr { get; set; }
        public Commands Command { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public List<string> Parameters { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserInput()
        {
            //Initialise user input
            Globals.UserInput = this;
            Globals.UserInput.Options = new Dictionary<string, string>();
            Globals.UserInput.Parameters = new List<string>();

            //Console indicator
            if (Globals.User.Authenticated)
                Console.Write("{0}> ", Globals.User.Username);
            else
                Console.Write("> ");
            Globals.UserInput.RawText = Console.ReadLine().Trim();

            //if a command has been given
            if (Globals.UserInput.RawText != "")
            {
                //split command into array
                Globals.UserInput.RawTextArr = Globals.UserInput.RawText.Split(' ');
                //get the command name
                GetCommandName();
                //process the command
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
            if ((this.Command == UserInput.Commands.login) || (this.Command == UserInput.Commands.help))
            {
                switch (this.Command)
                {
                    case Commands.login:
                        Globals.User.Login();
                        break;
                    case Commands.help:
                        Help();
                        break;
                    default:
                        break;
                }
            }
            else if (Globals.User.Authenticated == true)
            {
                switch (this.Command)
                {
                    case Commands.none:
                        Console.WriteLine(Constants.NO_COMMAND_ERROR);
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
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please log in");
            }
        }

        /// <summary>
        /// edit {-p:|-s} id {n:name|p:position|s:squadnumber|r:role}
        /// </summary>
        private void Edit()
        {
            string key;
            string value;
            bool error = false;
            for (int i = 0; i < Globals.UserInput.RawTextArr.Length; i++)
                if (Globals.UserInput.RawTextArr[i].Contains(':'))
                {
                    key = Globals.UserInput.RawTextArr[i].Split(':')[0];
                    value = Globals.UserInput.RawTextArr[i].Split(':')[1];
                    Globals.UserInput.Options.Add(key, value);
                }
            if (Globals.UserInput.Options == null)
            {
                Console.WriteLine(Constants.SYNTAX_ERROR);
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
                    Console.WriteLine(Constants.NOT_AUTHORISED_ERROR);

                if (Globals.UserInput.RawTextArr[1][1] == 'p')
                    Globals.Player.EditPlayer();
                else if (Globals.UserInput.RawTextArr[1][1] == 's')
                    Globals.Staff.EditStaff();
            }

            
        }

        /// <summary>
        /// find {-p|-s} name
        /// </summary>
        private void Find()
        {
            Globals.Person = new Person();
            Globals.Person.FindPersonByName();
        }

        /// <summary>
        /// delete {-s|-p} ID
        /// </summary>
        private void Delete()
        {
            if (Globals.User.IsAdmin)
            {
                Globals.Person = new Person();
                Globals.Person.DeletePerson();
            }
            else
                Console.WriteLine(Constants.NOT_AUTHORISED_ERROR);
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
                        Console.WriteLine(Constants.INTERNAL_ERROR);
                        break;
                }
            }
            else
            {
                Console.WriteLine(Constants.HELP_COMMAND_TITLE);
                foreach (Commands command in Enum.GetValues(typeof(Commands)))
                    if (command != Commands.none)
                        Console.WriteLine(command.ToString());
            }
            
        }

        /// <summary>
        /// add [Firstname.]Lastname {-p position number|-s role}
        /// </summary>
        private void Add()
        {
            if (Globals.User.IsAdmin)
            {
                if (Globals.UserInput.RawTextArr[2][1] == 'p')
                {
                    Globals.Player = new Player();
                    Globals.Player.AddPlayer();
                }
                else if (Globals.UserInput.RawTextArr[2][1] == 's')
                {
                    Globals.Staff = new Staff();
                    Globals.Staff.AddStaff();
                }
                else
                    Console.WriteLine(Constants.SYNTAX_ERROR);
            }
            else
                Console.WriteLine(Constants.NOT_AUTHORISED_ERROR);
        }
    } 
}
