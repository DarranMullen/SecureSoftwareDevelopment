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

        /// <summary>
        /// Constrructor
        /// </summary>
        public Staff()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Role = string.Empty;
        }

        public void GetRole(UserInput u)
        {
            if (Enum.TryParse(u.RawTextArr[2], out Roles r))
            {
                switch (r)
                {
                    case Roles.M:
                        this.Role = "Manager";
                        break;
                    case Roles.A:
                        this.Role = "Assistant";
                        break;
                    case Roles.C:
                        this.Role = "Coach";
                        break;
                    case Roles.S:
                        this.Role = "Scout";
                        break;
                    default:
                        this.Role = string.Empty;
                        break;
                }
            }
            else
            {
                this.Role = string.Empty;
                Console.WriteLine("Error: invalid role");
            }
        }

        public void AddStaff()
        {
            string staff;
            string staffs = Constants.STAFF_CSV_FILE;
            if (this.FirstName != string.Empty)
                staff = string.Format("{0} {1},{2}\n", this.FirstName, this.LastName, this.Role);
            else
                staff = string.Format("{0},{1}\n", this.LastName, this.Role);

            File.AppendAllText(staffs, staff);
        }
    }
}
