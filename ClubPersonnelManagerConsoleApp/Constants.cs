using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp
{
    class Constants
    {
        //SYNTAX
        public const string LOGIN_SYNTAX = "login u p\nu: username\np: password";
        public const string LOGOUT_SYNTAX = "logout [e]\ne: exit";
        public const string EXIT_SYNTAX = "exit\nexits the app";
        public const string HELP_SYNTAX = "help c\nc: command";
        public const string ADD_SYNTAX = "add [f.] {-p p n|-s r}\nf: first name(s)\nl: last name(s)\n(for multiple names dont use spaces and capitalise each name\np: indicates adding a player\n-s: indicates adding staff\np: position\nn: number\nr: role";
        public const string FIND_SYNTAX = "find {-p|-s} n\n-p: Search for players\n-s: Search for staff\nn: Name";
        public const string EDIT_SYNTAX = "edit {-p:|-s} i {n|p|s|r}\n-p: Edit Player\n-s: Edit Staff\ni: ID\nn: Name\np: Position\ns:Squad Number (Player Only)\nr: Role (Staff Only)";
        public const string DELETE_SYNTAX = "delete {-s|-p} \n-s: indicates deleting a player\n-s: indicated deleting staff\ni: id number";

        //ERROR MESAGES
        public const string SYNTAX_ERROR = "Error: Invalid Syntax";
        public const string LOGIN_ERROR = "Error: Already logged in";
        public const string LOGOUT_ERROR = "Error: Not logged in";
        public const string NO_COMMAND_ERROR = "Error: No command found";
        public const string INTERNAL_ERROR = "Internal Error";
        public const string NOT_AUTHORISED_ERROR = "Error: Not Authorised";
        public const string ROLE_ERROR = "Error: Invalid Role";
        public const string POSITION_ERROR = "Error: Invalid Position";
        public const string SQUAD_NUMBER_ERROR = "Error: Invalid Squad Number";
        public const string INVALID_RESPONSE_ERROR = "Error: Invalid Response";
        public const string INVALID_ID_ERROR = "Error: Invalid ID";

        //FEEDBACK
        public const string DELETE_SUCCESSFUL_FEEDBACK = "Deletion successful";
        public const string DELETE_ABORT_FEEDBACK = "Deletion aborted";
        public const string NO_MATCH_FEEBACK = "No Match Found";

        //USER PROMPTS
        public const string PROMPT_LOGOUT = "Logout? (y/n): ";

        //HEADERS
        public const string HELP_COMMAND_TITLE = "All commands";
        public const string PLAYER_LIST_TITLE = "ID   NAME                     POSITION                 SQUADNUMBER";


        //FILES
        public const string USER_CSV_FILE = "\\UserDb.csv";//"C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\UserDB.csv";
        public const string PLAYER_CSV_FILE = "\\PlayerDb.csv";//"C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\PlayerDB.csv";
        public const string STAFF_CSV_FILE = "\\StaffDb.csv";//"C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\StaffDB.csv";

        //AUTH
        public const string SALT = "LFC2005!LFC2019!";
    }
}
