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
            G,
            D,
            M,
            F
        }
        public string Position { get; set; }
        public string SquadNumber { get; set; }

        //Constructor
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Position = string.Empty;
            this.SquadNumber = string.Empty;
        }

        public void GetName(UserInput u)
        {
            string[] names;

            if (u.RawTextArr[1].Contains("."))

            {
                names = u.RawTextArr[1].Split('.');
                if (names.Length != 2)
                    Console.WriteLine("Error: incorrect syntax");
                else
                {
                    this.FirstName = names[0];
                    this.LastName = names[1];
                }
            }
            else
            {
                this.FirstName = string.Empty;
                this.LastName = u.RawTextArr[1];
            }
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
                player = string.Format("{0} {1},{2},{3}", this.FirstName, this.LastName, this.Position, this.SquadNumber);
            else
                player = string.Format("{0},{1},{2}",this.LastName,this.Position,this.SquadNumber);

            File.AppendAllText(players, player);


        }
    }
}
