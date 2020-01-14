using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    /// <summary>
    /// The Auth class is concerned with getting the salt and hashing passwords.
    /// </summary>
    class Auth
    {
        //Variables
        public byte[] Salt { get; set; }

        /// <summary>
        /// Sets the global auth and generates the salt
        /// </summary>
        public Auth()
        {
            Globals.Auth = this;
            GenerateSalt();
        }

        /// <summary>
        /// Generates the salt by encoding the ASCII salt to a byte array
        /// </summary>
        private void GenerateSalt()
        {
            Globals.Auth.Salt = Encoding.ASCII.GetBytes(Constants.SALT);
        }

        /// <summary>
        /// Salt and hash a given plaintext password by using PBKDF2 (Password-Based Key Derivation Function 2) to reduce vulnerabilities of brute force attacks.
        /// PBKDF2 applies a pseudorandom function, such as hash-based message authentication code (HMAC), to the input password or passphrase along with a salt value and repeats the process many times to produce a derived key, which can then be used as a cryptographic key in subsequent operations. The added computational work makes password cracking much more difficult, and is known as key stretching.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="newUser"></param>
        internal void HashPassword(ref string password, bool newUser)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Globals.Auth.Salt, 10000);
            password = ""; //clear password immediately
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(Globals.Auth.Salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            //if the password is for a new user: just hash the password
            if (newUser)
            {
                password = Convert.ToBase64String(hashBytes);
            }
            //if the password is for login: hash the password and update the raw text
            else
            {
                Globals.User.Password = Convert.ToBase64String(hashBytes);
                Globals.UserInput.RawText = "";
            }
            
        }
    }
}
