using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IStaff
    {
        string Role { get; set; }

        void GetRole();
        void AddStaff(bool isEdit);
        void EditStaff();
    }
}
