using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    /// <summary>
    /// Player inherents from Person.
    /// A player has a position and a squadnumber.
    /// </summary>
    class Player : Person
    {
        /// <summary>
        /// There are four valid positions:
        /// G = Goalkeeper,
        /// D = Defender,
        /// M = Midfielder,
        /// F = Forward.
        /// </summary>
        public enum Positions
        {
            G,  
            D,  
            M,  
            F   
        }

        // Variables
        public string Position { get; set; }
        public int SquadNumber { get; set; }

        /// <summary>
        /// Genereate a player with an id and name, 
        /// then gets the positions and squadnumber
        /// </summary>
        public Player()
        {
            Globals.Player = this;
            Globals.Player.Id = int.Parse(File.ReadLines(Globals.Auth.Dir + Constants.PLAYER_CSV_FILE).Last().Split(',')[0]) + 1;
            Globals.Player.Name = Globals.UserInput.RawTextArr[1];
            GetPosition();
            GetSquadNumber();
        }

        /// <summary>
        /// generates a player with a given Id
        /// </summary>
        /// <param name="id"></param>
        public Player(int id)
        {
            Globals.Player = this;
            Globals.Player.Id = id;
        }

        /// <summary>
        /// get the position from the raw text
        /// </summary>
        private void GetPosition()
        {
            //parse raw text position
            if (Enum.TryParse(Globals.UserInput.RawTextArr[3], out Positions pos))
            {
                // assign position to player
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
                        Console.WriteLine(Constants.INTERNAL_ERROR);
                        break;
                }
            }
            // set position to empty string and provide error feedback
            else
            {
                Globals.Player.Position = string.Empty;
                Console.WriteLine(Constants.POSITION_ERROR);
            }
        }

        /// <summary>
        /// get the squad number from the raw text
        /// </summary>
        private void GetSquadNumber()
        {
            //parse the raw text squad number
            if (int.TryParse(Globals.UserInput.RawTextArr[4], out int num))
                //assign squad number to player
                this.SquadNumber = num;
            else
            {
                //set squad number to defaul error number and display error
                this.SquadNumber = -1;
                Console.WriteLine(Constants.SQUAD_NUMBER_ERROR);
            }
        }

        /// <summary>
        /// add a player to player csv file
        /// if isEdit, display feedback that player was added. 
        /// Otherwise its been added as part of an update.
        /// </summary>
        /// <param name="isEdit"></param>
        public void AddPlayer(bool isEdit = false)
        {
            //Concat the line for adding to file
            string file = Globals.Auth.Dir + Constants.PLAYER_CSV_FILE;
            string line = string.Format("{0},{1},{2},{3}\n", Globals.Player.Id.ToString(), Globals.Player.Name, Globals.Player.Position, Globals.Player.SquadNumber);
            string[] lines;

            //try add line to file
            try 
            {
                File.AppendAllText(file, line);
                if (!isEdit)
                    Console.WriteLine("Player added");
                lines = File.ReadAllLines(file);
                //sort lines by squad number
                Array.Sort(lines);
                File.WriteAllLines(file, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// update a players details
        /// </summary>
        public void EditPlayer()
        {
            //for each option provided assign that to the player
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
                        Console.WriteLine(Constants.INTERNAL_ERROR);
                        break;
                }
            }
            Globals.Person = new Person();
            DeletePerson(true);
            AddPlayer(true);
            Globals.Player = null;
            Console.WriteLine("Player Edited Successfully");
        }
    }
}
        