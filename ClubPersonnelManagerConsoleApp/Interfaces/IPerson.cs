using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}

/*
 
     
     abstract class Personnel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Player : Personnel
    {
        public string Position { get; set; }
        public int SquadNumber { get; set; }
    }

    class Staff : Personnel
    {
        public string Role { get; set; }
    }
     
     
     
     */
