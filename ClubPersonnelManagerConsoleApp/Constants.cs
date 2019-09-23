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
        public const string EXIT_SYNTAX = "logout";
        public const string HELP_SYNTAX = "help c\nc: command";
        public const string ADD_SYNTAX = "add [f.] {-p p n|-s r}\nf: first name(s)\nl: last name(s)\n(for multiple names dont use spaces and capitalise each name\np: indicates adding a player\n-s: indicates adding staff\np: position\nn: number\nr: role";
        public const string FIND_SYNTAX = "";
        public const string EDIT_SYNTAX = "";
        public const string DELETE_SYNTAX = "delete {-s|-p} \n-s: indicates deleting a player\n-s: indicated deleting staff\ni: id number";

        //FILES
        public const string USER_CSV_FILE = "C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\UserDB.csv";
        public const string PLAYER_CSV_FILE = "C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\PlayerDB.csv";
        public const string STAFF_CSV_FILE = "C:\\Users\\Darran\\source\\repos\\Secure Software Development\\ClubPersonnelManager\\ClubPersonnelManagerConsoleApp\\bin\\Debug\\StaffDB.csv";
    }
}
