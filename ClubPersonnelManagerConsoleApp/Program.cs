using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp
{
    class Program
    {
        static void Main()
        {
            Globals.Auth = new Auth();
            do
            {
                ///This following region was used to hash the existing passwords in the database and is left here to show my work only.
                #region
                //Auth auth = new Auth();
                //var pbkdf2 = new Rfc2898DeriveBytes("bar", Globals.Auth.salt, 10000);
                //byte[] hash = pbkdf2.GetBytes(20);
                //byte[] hashBytes = new byte[36];
                //Array.Copy(Globals.Auth.salt, 0, hashBytes, 0, 16);
                //Array.Copy(hash, 0, hashBytes, 16, 20);
                //string savedPasswordHash = Convert.ToBase64String(hashBytes);
                //Console.WriteLine(savedPasswordHash);
                #endregion
                
                // if user doesnt exist (ie not logged in) create an null user
                if (Globals.User == null)
                    Globals.User = new User();
                //Get the users input, i.e. the command
                Globals.UserInput = new UserInput();
                //Clear the command
                Globals.UserInput = null;
            } while (true);//redo
        }

    }
}
