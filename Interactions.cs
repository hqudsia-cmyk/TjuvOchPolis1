using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TjuvOchPolis;

namespace TjuvOchPolis1
{
    internal class Interactions
    {
        private static Random rand = new Random();

        public static void CitizenGreetings(Citizen citizen, Police police, List<Person> people, int width, int height)
        {
           

            Console.SetCursorPosition(police.Position.X, police.Position.Y);
            Console.Write(police.Symbol);
            Console.SetCursorPosition(citizen.Position.X, citizen.Position.Y);
            Console.Write(citizen.Symbol);

           
            Console.SetCursorPosition(0, height + 4);
            Console.Write($"Medborgaren {citizen.Name} hälsar på polisen {police.Name}.                  ");

            RedrawPeople(people);
            Thread.Sleep(1000);

        }


        public static void HandleRobbery(Thief thief, Citizen citizen, List<Person> people, int width, int height)
        {
            
            ClearCityArea(width, height);

            // Visa bara rånaren och offret
            Console.SetCursorPosition(thief.Position.X, thief.Position.Y);
            Console.Write(thief.Symbol);
            Console.SetCursorPosition(citizen.Position.X, citizen.Position.Y);
            Console.Write(citizen.Symbol);

            ThiefStealsFromCitizen(thief, citizen);

            Thread.Sleep(1000); 
            
            Console.SetCursorPosition(2, 0);
            Console.Write(" City ");
            RedrawPeople(people);
        }


        public static void HandleArrest(Police police, Thief thief, List<Person> people,
        int width, int height, int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight)
        {
            ClearCityArea(width, height);

            Console.SetCursorPosition(police.Position.X, police.Position.Y);
            Console.Write(police.Symbol);
            Console.SetCursorPosition(thief.Position.X, thief.Position.Y);
            Console.Write(thief.Symbol);

            PoliceCatchesThief(police, thief, people, prisonStartX, prisonStartY, prisonWidth, prisonHeight);

            Thread.Sleep(1000);
            RedrawPeople(people);
        }

        private static void ClearCityArea(int width, int height)
        {
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        private static void RedrawPeople(List<Person> people)
        {
            foreach (var p in people)
            {
                Console.SetCursorPosition(p.Position.X, p.Position.Y);
                Console.Write(p.Symbol);
            }
        }

        private static void ThiefStealsFromCitizen(Thief thief, Citizen citizen)
        {
            if (citizen.Inventory.Items.Count == 0) return;

            int index = rand.Next(citizen.Inventory.Items.Count);
            string stolenItem = citizen.Inventory.Items[index];

            thief.Inventory.Items.Add(stolenItem);
            citizen.Inventory.Items.RemoveAt(index);

            Console.SetCursorPosition(0, 25);
            Console.WriteLine($"Tjuven {thief.Name} stal {stolenItem} från medborgaren!");
        }

        private static void PoliceCatchesThief(Police police, Thief thief, List<Person> people,
    int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight)
        {
            if (thief.Inventory.Items.Count == 0) return;

            double chance = 0.7;
            if (rand.NextDouble() < chance)
            {
                police.Inventory.Items.AddRange(thief.Inventory.Items);
                Console.SetCursorPosition(0, 26);
                Console.WriteLine($"Polisen {police.Name} konfiskerade: {string.Join(", ", thief.Inventory.Items)}          ");
                thief.Inventory.Items.Clear();

                // Flytta tjuven till fängelset
                int prisonX = prisonStartX + rand.Next(1, prisonWidth - 2);
                int prisonY = prisonStartY + rand.Next(1, prisonHeight - 2);

                thief.Position = new Position(prisonX, prisonY);
                thief.Symbol = 'F'; // Markera att tjuven är fånge

                thief.Position = new Position(rand.Next(102, 113), rand.Next(11, 23)); // slump inom prison-rutan
                thief.Direction = Direction.RandomDirection();

                Console.SetCursorPosition(prisonX, prisonY);
                Console.Write(thief.Symbol);

                Console.SetCursorPosition(0, 27);
                Console.WriteLine($"Tjuven {thief.Name} har gripits och placerats i fängelset!");
            }
            else
            {
                Console.SetCursorPosition(0, 27);
                Console.WriteLine("Tjuven lyckades undkomma!");
            }
        }
    }
}
    

