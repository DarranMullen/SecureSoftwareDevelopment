using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IPerson
    {
        int Id { get; set; }
        string Name { get; set; }

        void GetId();
        void DeletePerson(bool isEdit);
        void FindPersonByName();
        void FindPersonById();
    }
}