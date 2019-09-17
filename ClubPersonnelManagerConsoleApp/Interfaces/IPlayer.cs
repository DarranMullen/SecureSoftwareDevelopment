using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IPlayer
    {
        string Position { get; set; }
        string SquadNumber { get; set; }

        void GetPosition(UserInput u);
        void GetSquadNumber(UserInput u);
        void AddPlayer();
    }
}
