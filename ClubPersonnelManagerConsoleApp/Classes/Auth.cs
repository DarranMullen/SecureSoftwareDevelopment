using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Auth
    {
        internal byte[] Salt { get; set; }

        public Auth()
        {
            Globals.Auth = this;
            GenerateSalt();
        }

        private void GenerateSalt()
        {
            Globals.Auth.Salt = Encoding.ASCII.GetBytes(Constants.SALT);
        }

        internal void HashPassword(string password)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Globals.Auth.Salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(Globals.Auth.Salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            Globals.User.Password = Convert.ToBase64String(hashBytes);
            Globals.UserInput.RawText = Globals.UserInput.RawText.Remove((Globals.UserInput.RawText.Length - (Globals.UserInput.RawText.Split(' ')[2].Length + 1)));
        }
    }
}
