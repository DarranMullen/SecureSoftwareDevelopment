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
    }
}
