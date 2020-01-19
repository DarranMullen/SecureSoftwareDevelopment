using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp
{
    //TODO: add password expiry
    //TODO: log user out if inactive for 60 seconds
    //TODO: encrypt entire csv files
    class Program
    {
        static void Main()
        {
            //Initialise the auth class
            Globals.Auth = new Auth();
            ///This keeps asking for user input
            do
            {                
                // if user doesnt exist (ie not logged in) create an null user
                if (Globals.User == null)
                    Globals.User = new User();
                //Get the users input, i.e. the command
                Globals.UserInput = new UserInput();
                //Clear the command
                Globals.UserInput = null;
                GC.Collect();
            } while (true);
        }
    }
}
