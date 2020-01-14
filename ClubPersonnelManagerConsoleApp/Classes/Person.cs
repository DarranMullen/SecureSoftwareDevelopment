using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    /// <summary>
    /// The Person class is the superclass of Player and Staff Classes
    /// Every player and staff has both an Id and a Name
    /// The Person class is concerned with getting an Id for a person, deleting a person, finding a person by id and or name
    /// </summary>
    class Person
    {
        //Variables
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Set global Person and get an Id
        /// </summary>
        public Person()
        {
            Globals.Person = this;
            GetId();
        }

        /// <summary>
        /// get the id from the raw text and parse it
        /// </summary>
        private void GetId()
        {
            if (int.TryParse(Globals.UserInput.RawTextArr[2], out int id))
                Globals.Person.Id = id;
            else
                Globals.Person.Id = -1;
        }

        /// <summary>
        /// Delete a person
        /// isEdit is true when updating a row, ie delete the old and replace with the new
        /// </summary>
        /// <param name="isEdit"></param>
        public void DeletePerson(bool isEdit = false)
        {
            // the Id will only be -1 if no id has been found (invalid id) by the GetId method
            if (Globals.Person.Id != -1) 
            {
                // Variables
                bool personFound = false;
                bool invalidResponse = false;
                string file = String.Empty;
                List<string> list = new List<string>();
                //try get the lines from the correct csv file
                try
                {
                    //player
                    if (Globals.UserInput.RawTextArr[1][1] == 'p')
                    {
                        file = Constants.PLAYER_CSV_FILE;
                        list = File.ReadLines(file).ToList();
                    }
                    //staff
                    else if (Globals.UserInput.RawTextArr[1][1] == 's')
                    {
                        file = Constants.STAFF_CSV_FILE;
                        list = File.ReadLines(file).ToList();
                    }
                    else
                        Console.WriteLine(Constants.SYNTAX_ERROR);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //step through each line looking for the matching Id
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Split(',')[0].Equals(Globals.Person.Id.ToString()))
                    {
                        personFound = true;
                        //Delete
                        if (!isEdit)
                        {
                            do
                            {
                                //Confirm deletion
                                Console.WriteLine("Delete {0}? (Y/N)", list[i].Split(',')[1]);
                                ConsoleKey key = Console.ReadKey().Key;
                                if (key == ConsoleKey.Y)
                                {
                                    //Do deletion
                                    list.RemoveAt(i);
                                    try
                                    {
                                        File.WriteAllLines(file, list);
                                        //Display feedback
                                        Console.WriteLine(Constants.DELETE_SUCCESSFUL_FEEDBACK);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                else if (key == ConsoleKey.N)
                                    //abort deletion and display feedback
                                    Console.WriteLine(Constants.DELETE_ABORT_FEEDBACK);
                                else
                                {
                                    invalidResponse = true;
                                    Console.WriteLine(Constants.INVALID_RESPONSE_ERROR);
                                }
                                //exit the for loop
                                i = list.Count;

                                //Keep prompting user until valid key pressed
                            } while (invalidResponse);
                        }
                        //delete for updating
                        else
                        {
                            list.RemoveAt(i);
                            try
                            {
                                File.WriteAllLines(file, list);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            i = list.Count;
                        }
                    }
                }

                // if no person found, display feedback
                if (!personFound)
                    Console.WriteLine(Constants.NO_MATCH_FEEBACK);
            }
            //Invalid Id
            else
                Console.WriteLine(Constants.INVALID_ID_ERROR);
        }

        /// <summary>
        /// Finds a user by Id
        /// </summary>
        public void FindPersonById()
        {
            // the Id will only be -1 if no id has been found (invalid id) by the GetId method
            if (Globals.Person.Id != -1)
            {
                //Variables
                string file = "";
                List<string> list = new List<string>();
                
                //try get rows from correct csv file
                try
                {
                    //player
                    if (Globals.UserInput.RawTextArr[1][1] == 'p')
                    {
                        file = Constants.PLAYER_CSV_FILE;
                        list = File.ReadLines(file).ToList();
                    }
                    //staff
                    else if (Globals.UserInput.RawTextArr[1][1] == 's')
                    {
                        file = Constants.STAFF_CSV_FILE;
                        list = File.ReadLines(file).ToList();
                    }
                    else
                        Console.WriteLine(Constants.SYNTAX_ERROR);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //step through each line until we find a matching Id
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Split(',')[0].Equals(Globals.Person.Id.ToString()))
                    {
                        if (file==Constants.PLAYER_CSV_FILE)
                        {
                            //Generate the Player object
                            Globals.Player = new Player(Globals.Person.Id);
                            string[] playerDetails = list[i].Split(',');
                            Globals.Player.Name = playerDetails[1];
                            Globals.Player.Position = playerDetails[2];
                            Globals.Player.SquadNumber = int.Parse(playerDetails[3]);
                            break;
                        }
                        else if (file == Constants.STAFF_CSV_FILE)
                        {
                            //generate the staff object
                            Globals.Staff = new Staff(Globals.Person.Id);
                            string[] staffDetails = list[i].Split(',');
                            Globals.Staff.Name = staffDetails[1];
                            Globals.Staff.Role = staffDetails[2];
                            break;
                        }
                        else
                            Console.WriteLine(Constants.INTERNAL_ERROR);
                    }
                }
            }
            else
                Console.WriteLine(Constants.INVALID_ID_ERROR);
            Globals.Person = null;
            }

        /// <summary>
        /// Finds a user by name
        /// </summary>
        public void FindPersonByName()
        {
            // Variables
            string[] arr;
            string playerId;
            string playerName;
            string playerPosition;
            string playerSquadNumber;
            string staffId;
            string staffName;
            string staffRole;
            string file = "";
            List<string> foundPeople = new List<string>();
            List<string> list = new List<string>();

            Globals.Person.Name = Globals.UserInput.RawTextArr[2];
            
            // try get the lies from correct file
            try
            {
                if (Globals.UserInput.RawTextArr[1][1] == 'p')
                {
                    file = Constants.PLAYER_CSV_FILE;
                    list = File.ReadLines(file).ToList();
                }
                else if (Globals.UserInput.RawTextArr[1][1] == 's')
                {
                    file = Constants.STAFF_CSV_FILE;
                    list = File.ReadLines(file).ToList();
                }
                else
                    Console.WriteLine(Constants.SYNTAX_ERROR);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // step through each line, looking for a match or partial match of the name provided
            for (int i = 0; i < list.Count; i++)
                try
                {
                    //if a match is found add this row to foundpeople list
                    if (list[i].Split(',')[1].Contains(Globals.Person.Name))
                        foundPeople.Add(list[i]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error at line " + i.ToString() + " of csv file");

                }
            //if no mathces found, display feedback
            if (foundPeople.Count < 1) 
                Console.WriteLine(Constants.NO_MATCH_FEEBACK);
            //display found people
            else
            {
                //players
                if (file == Constants.PLAYER_CSV_FILE)
                {
                    Console.WriteLine(Constants.PLAYER_LIST_TITLE);
                    foreach (var p in foundPeople)
                    {
                        arr = p.Split(',');
                        playerId = arr[0];
                        playerName = arr[1];
                        playerPosition = arr[2];
                        playerSquadNumber = arr[3];

                        Console.WriteLine("{0,-5}{1,-25}{2,-25}{3,-5}", playerId, playerName, playerPosition, playerSquadNumber);
                    }
                }
                //staff
                else
                {
                    Console.WriteLine("{0,-5}{1,-25}{2,-25}", "ID", "NAME", "ROLE");
                    foreach (var s in foundPeople)
                    {
                        arr = s.Split(',');
                        staffId = arr[0];
                        staffName = arr[1];
                        staffRole= arr[2];

                        Console.WriteLine("{0,-5}{1,-25}{2,-25}", staffId, staffName, staffRole);
                    }
                }
            }
            Globals.Person = null;
        }  
    }
}
