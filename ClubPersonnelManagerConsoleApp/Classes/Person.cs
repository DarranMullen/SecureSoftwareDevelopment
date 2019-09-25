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
            {
                Globals.Person.Id = id;
            }
            else
            {
                Globals.Person.Id = -1;
            }
        }
        public void DeletePerson(bool isEdit = false)
        {
            if (Globals.Person.Id != -1) 
            {
                bool personFound = false;
                bool invalidResponse = false;
                string file = "";
                List<string> list = new List<string>();
                //TRY GET FILE AND LIST OF LINES IN IT
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
                    {
                        Console.WriteLine("Syntax Error");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //SEARCH FOR PERSON BY ID
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
                                        Console.WriteLine("Delete successful");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                else if (key == ConsoleKey.N)
                                    Console.WriteLine("Delete aborted");
                                else
                                {
                                    invalidResponse = true;
                                    Console.WriteLine("Invalid Response");
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

                //IF NO PERSON FOUND
                if (!personFound)
                {
                    Console.WriteLine("No match found");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
           // Globals.Person = null;
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
                    {
                        Console.WriteLine("Syntax Error");
                    }

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
                        {
                            Console.WriteLine("ERROR");
                        }
                        
                    }
                }


            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
            Globals.Person = null;
            }

        public void FindPersonByName()
        {
            Globals.Person.Name = Globals.UserInput.RawTextArr[2];
            List<string> foundPeople = new List<string>();

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
                {
                    Console.WriteLine("Syntax Error");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (int i = 0; i < list.Count; i++)
                if (list[i].Split(',')[1].Contains(Globals.Person.Name))
                    foundPeople.Add(list[i]);
            if (foundPeople.Count < 1) 
                Console.WriteLine("No match for that name in database");
            else
            {
                if (file == Constants.PLAYER_CSV_FILE)
                {
                    Console.WriteLine("{0,-5}{1,-25}{2,-25}{3,-5}", "ID","NAME","POSITION","SQUAD NUMBER");
                    foreach (var p in foundPeople)
                    {
                        var arr = p.Split(',');
                        string playerId = arr[0];
                        string playerName = arr[1];
                        string playerPosition = arr[2];
                        string playerSquadNumber = arr[3];

                        Console.WriteLine("{0,-5}{1,-25}{2,-25}{3,-5}", playerId, playerName, playerPosition, playerSquadNumber);
                    }
                }
                else
                {
                    Console.WriteLine("{0,-5}{1,-25}{2,-25}", "ID", "NAME", "ROLE");
                    foreach (var s in foundPeople)
                    {
                        var arr = s.Split(',');
                        string staffId = arr[0];
                        string staffName = arr[1];
                        string staffRole= arr[2];

                        Console.WriteLine("{0,-5}{1,-25}{2,-25}", staffId, staffName, staffRole);
                    }
                }
            }
            Globals.Person = null;
        }  
    }
}
