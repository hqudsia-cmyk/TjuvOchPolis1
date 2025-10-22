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

        public static void CitizenGreetings(Citizen citizen, Police police, List<Person> people, int width, int height, List<string> newsFeed)
        {

            Display.DrawPerson(police);
            Display.DrawPerson(citizen);

            newsFeed.Add($"Medborgaren {citizen.Name} hälsar på polisen {police.Name}");

            if (newsFeed.Count > 10)
                newsFeed.RemoveAt(0);

            RedrawPeople(people);
            Thread.Sleep(1000); // ändra till 1000

        }

        public static void HandleRobbery(Thief thief, Citizen citizen, List<Person> people, int width, int height, List<string> newsFeed)
        {

            ClearCityArea(width, height);

            Display.DrawPerson(thief);
            Display.DrawPerson(citizen);

            ThiefStealsFromCitizen(thief, citizen, newsFeed);

            Thread.Sleep(1000); // ändra till 1000

            Console.SetCursorPosition(2, 0);
            Console.Write(" City ");
            RedrawPeople(people);
        }


        public static void HandleArrest(Police police, Thief thief, List<Person> people,
        int width, int height, int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight, List<string> newsFeed)
        {
            ClearCityArea(width, height);

            Display.DrawPerson(police);
            Display.DrawPerson(thief);

            PoliceCatchesThief(police, thief, people, prisonStartX, prisonStartY, prisonWidth, prisonHeight, newsFeed);
           

            Thread.Sleep(1000); // ändra till 1000
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

        private static void ThiefStealsFromCitizen(Thief thief, Citizen citizen, List<string> newsFeed)
        {
            if (citizen.Inventory.Items.Count == 0) return;

            int index = rand.Next(citizen.Inventory.Items.Count);
            string stolenItem = citizen.Inventory.Items[index];

            thief.Inventory.Items.Add(stolenItem);
            citizen.Inventory.Items.RemoveAt(index);

            newsFeed.Add($"Tjuven {thief.Name} stal {stolenItem} från medborgaren!");

            if (newsFeed.Count > 10)
                newsFeed.RemoveAt(0);
        }

        private static void PoliceCatchesThief(Police police, Thief thief, List<Person> people,
    int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight, List<string> newsFeed)
        {
            
            if (thief.Inventory.Items.Count == 0) return;

            double chance = 0.7;
            if (rand.NextDouble() < chance)
            {
                
                int stolenCount = thief.Inventory.Items.Count;

                // Polisen tar allt
                police.Inventory.Items.AddRange(thief.Inventory.Items);
                thief.Inventory.Items.Clear();

                // Straff baserat på hur mycket tjuven stulit
                thief.PrisonTime = stolenCount * 50;
                thief.PrisonCounter = 0;

                // Flytta tjuven till fängelset
                int prisonX = prisonStartX + rand.Next(1, prisonWidth - 2);
                int prisonY = prisonStartY + rand.Next(1, prisonHeight - 2);
                thief.Position = new Position(prisonX, prisonY);
                thief.Symbol = 'F'; // Markera tjuv som fånge

                newsFeed.Add($"Polisen {police.Name} grep {thief.Name} för {stolenCount} stölder!");
                newsFeed.Add($"Tjuven sitter i fängelse i {stolenCount} min (= {thief.PrisonTime} frames).");

                if (newsFeed.Count > 10)
                    newsFeed.RemoveAt(0);
            }
            else
            {
                newsFeed.Add("Tjuven lyckades undkomma!");

                if (newsFeed.Count > 10)
                    newsFeed.RemoveAt(0);
            }
        }
    }
}

    

