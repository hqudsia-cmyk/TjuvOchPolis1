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

        
        public static void HandleRobbery(Thief thief, Citizen citizen, List<Person> people, int width, int height)
        {
            
            ClearCityArea(width, height);

            // Visa bara rånaren och offret
            Console.SetCursorPosition(thief.Position.X, thief.Position.Y);
            Console.Write(thief.Symbol);
            Console.SetCursorPosition(citizen.Position.X, citizen.Position.Y);
            Console.Write(citizen.Symbol);

            ThiefStealsFromCitizen((ThiefInventory)thief.Inventory, (CitizenInventory)citizen.Inventory);

            Thread.Sleep(3000); 

            
            Console.SetCursorPosition(2, 0);
            Console.Write(" City ");
            RedrawPeople(people);
        }

        
        public static void HandleArrest(Police police, Thief thief, List<Person> people, int width, int height)
        {
            ClearCityArea(width, height);

            Console.SetCursorPosition(police.Position.X, police.Position.Y);
            Console.Write(police.Symbol);
            Console.SetCursorPosition(thief.Position.X, thief.Position.Y);
            Console.Write(thief.Symbol);

            PoliceCatchesThief((PoliceInventory)police.Inventory, (ThiefInventory)thief.Inventory);
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

        private static void ThiefStealsFromCitizen(ThiefInventory thief, CitizenInventory citizen)
        {
            if (citizen.Items.Count == 0) return;

            int index = rand.Next(citizen.Items.Count);
            string stolenItem = citizen.Items[index];

            thief.Items.Add(stolenItem);
            citizen.Items.RemoveAt(index);

            Console.SetCursorPosition(0, 25);
            Console.WriteLine($"Tjuven stal {stolenItem} från medborgaren!");
        }

        private static void PoliceCatchesThief(PoliceInventory police, ThiefInventory thief)
        {
            if (thief.Items.Count == 0) return;

            double chance = 0.7;
            if (rand.NextDouble() < chance)
            {
                police.Items.AddRange(thief.Items);
                Console.SetCursorPosition(0, 26);
                Console.WriteLine($"Polisen konfiskerade: {string.Join(", ", thief.Items)}          ");
                thief.Items.Clear();
            }
            else
            {
                Console.SetCursorPosition(0, 27);
                Console.WriteLine("Tjuven lyckades undkomma!");
            }
        }
    }
}
    

