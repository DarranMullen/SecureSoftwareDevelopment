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
        public int SquadNumber { get; set; }

        //Constructor
        public Player()
        {
            Globals.Player = this;
            Globals.Player.Id = int.Parse(File.ReadLines(Constants.PLAYER_CSV_FILE).Last().Split(',')[0]) + 1;
            Globals.Player.Name = Globals.UserInput.RawTextArr[1];
            GetPosition();
            GetSquadNumber();
        }
        public Player(int id)
        {
            Globals.Player = this;
            Globals.Player.Id = id;
        }

        public void GetPosition()
        {
            if (Enum.TryParse(Globals.UserInput.RawTextArr[3], out Positions pos))
            {
                switch (pos)
                {
                    case Positions.G:
                        Globals.Player.Position = "Goalkeeper";
                        break;
                    case Positions.D:
                        Globals.Player.Position = "Defender";
                        break;
                    case Positions.M:
                        Globals.Player.Position = "Midfielder";
                        break;
                    case Positions.F:
                        Globals.Player.Position = "Forward";
                        break;
                    default:
                        Globals.Player.Position = string.Empty;
                        break;
                }
            }
            else
            {
                Globals.Player.Position = string.Empty;
                Console.WriteLine("Error: invalid position");
            }
        }

        public void GetSquadNumber()
        {
            if (int.TryParse(Globals.UserInput.RawTextArr[4], out int num))
                this.SquadNumber = num;
            else
            {
                this.SquadNumber = -1;
                Console.WriteLine("Error: invalid squad number");
            }
        }

        public void AddPlayer(bool isEdit = false)
        {
            string file = Constants.PLAYER_CSV_FILE;
            string line = string.Format("{0},{1},{2},{3}\n", Globals.Player.Id.ToString(), Globals.Player.Name, Globals.Player.Position, Globals.Player.SquadNumber);

            try
            {
                File.AppendAllText(file, line);
                if (!isEdit)
                    Console.WriteLine("Player added");
                string[] lines = File.ReadAllLines(file);
                Array.Sort(lines);
                File.WriteAllLines(file, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditPlayer()
        {
            foreach (var option in Globals.UserInput.Options)
            {
                switch (option.Key)
                {
                    case "n":
                        Globals.Player.Name = option.Value;
                        break;
                    case "p":
                        Globals.Player.Position = option.Value;
                        break;
                    case "s":
                        Globals.Player.SquadNumber = int.Parse(option.Value);
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                
            }
            Globals.Person = new Person();
            DeletePerson(true);
            AddPlayer(true);
            Globals.Player = null;

        }


    }
}
        