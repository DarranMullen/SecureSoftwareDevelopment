using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    abstract class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void GetName(UserInput u)
        {
            string[] names;

            if (u.RawTextArr[1].Contains("."))

            {
                names = u.RawTextArr[1].Split('.');
                if (names.Length != 2)
                    Console.WriteLine("Error: incorrect syntax");
                else
                {
                    this.FirstName = names[0];
                    this.LastName = names[1];
                }
            }
            else
            {
                this.FirstName = string.Empty;
                this.LastName = u.RawTextArr[1];
            }
        }
    }
}
