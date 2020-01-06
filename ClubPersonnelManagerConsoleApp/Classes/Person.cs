using ClubPersonnelManagerConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPersonnelManagerConsoleApp.Classes
{
    class Person : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person()
        {
            Globals.Person = this;
            GetId();
        }

        public void GetId()
        {
            if (int.TryParse(Globals.UserInput.RawTextArr[2], out int id))
                Globals.Person.Id = id;
            else
                Globals.Person.Id = -1;
        }

        public void DeletePerson(bool isEdit = false)
        {
            if (Globals.Person.Id != -1) 
            {
                bool personFound = false;
                bool invalidResponse = false;
                string file = "";
                List<string> list = new List<string>();
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

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Split(',')[0].Equals(Globals.Person.Id.ToString()))
                    {
                        personFound = true;
                        if (!isEdit)
                        {
                            do
                            {
                                Console.WriteLine("Delete {0}? (Y/N)", list[i].Split(',')[1]);
                                ConsoleKey key = Console.ReadKey().Key;
                                if (key == ConsoleKey.Y)
                                {
                                    list.RemoveAt(i);
                                    try
                                    {
                                        File.WriteAllLines(file, list);
                                        Console.WriteLine(Constants.DELETE_SUCCESSFUL_FEEDBACK);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                else if (key == ConsoleKey.N)
                                    Console.WriteLine(Constants.DELETE_ABORT_FEEDBACK);
                                else
                                {
                                    invalidResponse = true;
                                    Console.WriteLine(Constants.INVALID_RESPONSE_ERROR);
                                }
                                i = list.Count;
                            } while (invalidResponse);
                        }
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

                if (!personFound)
                    Console.WriteLine(Constants.NO_MATCH_FEEBACK);
            }
            else
                Console.WriteLine(Constants.INVALID_ID_ERROR);
        }

        public void FindPersonById()
        {
            if (Globals.Person.Id != -1)
            {
                string file = "";
                List<string> list = new List<string>();
                
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

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Split(',')[0].Equals(Globals.Person.Id.ToString()))
                    {
                        if (file==Constants.PLAYER_CSV_FILE)
                        {
                            Globals.Player = new Player(Globals.Person.Id);
                            string[] playerDetails = list[i].Split(',');
                            Globals.Player.Name = playerDetails[1];
                            Globals.Player.Position = playerDetails[2];
                            Globals.Player.SquadNumber = int.Parse(playerDetails[3]);
                            break;
                        }
                        else if (file == Constants.STAFF_CSV_FILE)
                        {
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

        public void FindPersonByName()
        {
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

            Globals.Person.Name = Globals.UserInput.RawTextArr[2];
            
            List<string> list = new List<string>();
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

            for (int i = 0; i < list.Count; i++) try
                {

                    if (list[i].Split(',')[1].Contains(Globals.Person.Name))
                        foundPeople.Add(list[i]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error at line " + i.ToString() + " of csv file");

                }
            if (foundPeople.Count < 1) 
                Console.WriteLine(Constants.NO_MATCH_FEEBACK);
            else
            {
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
