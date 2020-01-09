using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Auth : IAuth
    {
        public byte[] salt { get; set; }

        public Auth()
        {
            Globals.Auth = this;
            GenerateSalt();
        }

        public void GenerateSalt()
        {
            Globals.Auth.salt = Encoding.ASCII.GetBytes(Constants.SALT);
        }

        public void HashPassword(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Globals.Auth.salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(Globals.Auth.salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            Globals.User.Password = Convert.ToBase64String(hashBytes);
            Globals.UserInput.RawText = Globals.UserInput.RawText.Remove((Globals.UserInput.RawText.Length - (Globals.UserInput.RawText.Split(' ')[2].Length + 1)));

        }
    }
}
