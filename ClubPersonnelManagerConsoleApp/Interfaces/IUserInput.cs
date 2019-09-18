using ClubPersonnelManagerConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Interfaces
{
    interface IUserInput
    {
        string RawText { get; set; }
        string[] RawTextArr { get; set;}

        UserInput.Commands Command { get; set; }
        Dictionary<string, string> Options { get; set; }
        List<string> Parameters { get; set; }
        

        void GetCommandName();
        void ProcessCommand();
        void Clear();
    }


}
