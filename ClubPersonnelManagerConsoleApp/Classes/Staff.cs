using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Staff : Person, IStaff
    {
        public string Role { get; set; }
    }
}
