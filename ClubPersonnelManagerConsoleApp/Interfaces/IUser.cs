using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
        bool IsAdmin { get; set; }
        bool Authenticated { get; set; }

        void Login(UserInput userInput);
    }
}
