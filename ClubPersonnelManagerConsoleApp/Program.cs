using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp
{
    class Program
    {
        //TODO CACHE FILES LOCALLY TO PREVENT MULTIPLE CALLS TO FILE
        //TODO DISPLAY ALL COMMAND
        static void Main()
        {
            do
            {
                if (Globals.User == null)
                    Globals.User = new User();
                Globals.UserInput = new UserInput();
                Globals.UserInput = null;
            } while (true);
        }

    }
}
