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
        int SquadNumber { get; set; }

        void GetPosition();
        void GetSquadNumber();

        void AddPlayer(bool isEdit);
        void EditPlayer();
    }
}
