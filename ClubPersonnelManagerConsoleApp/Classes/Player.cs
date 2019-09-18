using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Player : Person, IPlayer
    {
        public enum Positions
        {
            G,  /* Goalkeeper */
            D,  /* Defender */
            M,  /* Midfielder */
            F   /* Forward */
        }

        public string Position { get; set; }
        public string SquadNumber { get; set; }
        public int Id { get; set; }

        
         

        //Constructor
        public Player()
        {
            this.Id = int.Parse(File.ReadLines(Constants.PLAYER_CSV_FILE).Last().Split(',')[0]) + 1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Position = string.Empty;
            this.SquadNumber = string.Empty;
        }

        public void GetPosition(UserInput u)
        {
            if (Enum.TryParse(u.RawTextArr[3], out Positions p))
            {
                switch (p)
                {
                    case Positions.G:
                        this.Position = "Goalkeeper";
                        break;
                    case Positions.D:
                        this.Position = "Defender";
                        break;
                    case Positions.M:
                        this.Position = "Midfielder";
                        break;
                    case Positions.F:
                        this.Position = "Forward";
                        break;
                    default:
                        this.Position = string.Empty;
                        break;
                }
            }
            else
            {
                this.Position = string.Empty;
                Console.WriteLine("Error: invalid position");
            }
        }

        public void GetSquadNumber(UserInput u)
        {
            if (int.TryParse(u.RawTextArr[4], out int number))
                this.SquadNumber = number.ToString();
            else
            {
                this.SquadNumber = string.Empty;
                Console.WriteLine("Error: incorrect syntax");
            }
        }

        public void AddPlayer()
        {
            string player;
            string players = Constants.PLAYER_CSV_FILE;
            if (this.FirstName != string.Empty)
                player = string.Format("{0},{1} {2},{3},{4}\n", Id.ToString(), this.FirstName, this.LastName, this.Position, this.SquadNumber);
            else
                player = string.Format("{0},{1},{2},{3}\n", this.Id.ToString(), this.LastName, this.Position, this.SquadNumber);

            try
            {
                File.AppendAllText(players, player);
                Console.WriteLine("Player added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
