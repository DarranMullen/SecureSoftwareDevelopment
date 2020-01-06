using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IAuth
    {
        byte[] salt { get; set; }

        void GenerateSalt();

    }
}
