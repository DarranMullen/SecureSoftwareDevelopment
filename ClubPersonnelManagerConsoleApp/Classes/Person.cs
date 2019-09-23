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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void DeletePerson(int id, char type)
        {
            bool personFound = false;
            bool invalidResponse = false;
            string file = "";
            List<string> list = new List<string>();
            try
            {
                if (type == 'p')
                {
                    file = Constants.PLAYER_CSV_FILE;
                    list = File.ReadLines(file).ToList();
                }
                else if (type == 's')
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
                if (list[i].Split(',')[0].Equals(id.ToString()))
                {
                    personFound = true;
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
            }

            if (!personFound)
            {
                Console.WriteLine("No match found");
            }
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
            FormatName();            
        }
        private void FormatName()
        {
            for (int i = 1; i < this.FirstName.Length; i++)
            {
                if (char.IsUpper(this.FirstName[i]))
                {
                    this.FirstName = this.FirstName.Insert((i - 1), " ");
                }
            }

            for (int i = 1; i < this.LastName.Length; i++)
            {
                if (char.IsUpper(LastName[i]))
                {
                    this.LastName = this.LastName.Insert((i), " ");
                    i++;
                }
            }
        }
    }
}
