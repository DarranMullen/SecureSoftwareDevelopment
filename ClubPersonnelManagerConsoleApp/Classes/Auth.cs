using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Globals.Auth.salt = Encoding.ASCII.GetBytes("LFC2005!LFC2019!");
        }
    }
}
