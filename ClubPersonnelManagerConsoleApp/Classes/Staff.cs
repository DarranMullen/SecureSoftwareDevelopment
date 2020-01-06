using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Staff : Person, IStaff
    {
        public enum Roles
        {
            M,  /*  Manager     */
            A,  /*  Assistant   */
            C,  /*  Coach       */
            S   /*  Scout       */
        }

        public string Role { get; set; }

        public Staff()
        {
            Globals.Staff = this;
            Globals.Staff.Id = int.Parse(File.ReadLines(Constants.STAFF_CSV_FILE).Last().Split(',')[0]) + 1;
            Globals.Staff.Name = Globals.UserInput.RawTextArr[1];
            GetRole();
        }

        public Staff(int id)
        {
            Globals.Staff = this;
            Globals.Staff.Id = id;
        }

        public void GetRole()
        {
            if (Enum.TryParse(Globals.UserInput.RawTextArr[3], out Roles r))
            {
                switch (r)
                {
                    case Roles.M:
                        Globals.Staff.Role = "Manager";
                        break;
                    case Roles.A:
                        Globals.Staff.Role = "Assistant";
                        break;
                    case Roles.C:
                        Globals.Staff.Role = "Coach";
                        break;
                    case Roles.S:
                        Globals.Staff.Role = "Scout";
                        break;
                    default:
                        Globals.Staff.Role = string.Empty;
                        Console.WriteLine(Constants.INTERNAL_ERROR);
                        break;
                }
            }
            else
            {
                Globals.Staff.Role = string.Empty;
                Console.WriteLine(Constants.ROLE_ERROR);
            }
        }

        public void AddStaff(bool isEdit = false)
        {
            string file = Constants.STAFF_CSV_FILE;
            string line = string.Format("{0},{1},{2}\n", Globals.Staff.Id.ToString(), Globals.Staff.Name, Globals.Staff.Role);
            string[] lines;
            try
            {
                File.AppendAllText(file, line);
                if (!isEdit)
                    Console.WriteLine("Staff added");
                lines = File.ReadAllLines(file);
                Array.Sort(lines);
                File.WriteAllLines(file, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Globals.Staff = null;
        }

        public void EditStaff()
        {
            foreach (var option in Globals.UserInput.Options)
            {
                switch (option.Key)
                {
                    case "n":
                        Globals.Staff.Name = option.Value;
                        break;
                    case "r":
                        Globals.Staff.Role = option.Value;
                        break;
                    default:
                        Console.WriteLine(Constants.INTERNAL_ERROR);
                        break;
                }
                Globals.Person = new Person();
                DeletePerson(true);
                AddStaff(true);
                Console.WriteLine("Staff Edited Successfully");
            }
        }
    }
}
