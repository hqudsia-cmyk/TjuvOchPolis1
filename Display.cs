using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TjuvOchPolis;

namespace TjuvOchPolis1
{
    internal class Display
    {
        internal List<string> newsFeed = new List<string>();

        internal static void DrawNews(List<string> newsFeed, int width, int height)
        {
            int startLine = height + 1;

            int maxLines = 10;

            // bara de senaste 10 nyheterna
            var recent = newsFeed.TakeLast(maxLines).ToList();

            // Rensa område
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(new string(' ', width));
            }

            // Rita upp
            for (int i = 0; i < recent.Count; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write($"{recent[i]}");
            }

        }

        internal static void DrawStatus(List<Person> people, int width, int height)
        {
            int policeCount = people.Count(person => person is Police);
            int thiefCount = people.Count(person => person is Thief && person.Symbol == 'T');
            int prisonerCount = people.Count(person => person is Thief && person.Symbol == 'F');
            int citizenCount = people.Count(person => person is Citizen);

            int robbedCitizens = people.OfType<Citizen>().Count(citizen => citizen.Inventory.Items.Count < 4);




            Console.SetCursorPosition(60, 25);
            Console.WriteLine("== Status == ");
            Console.SetCursorPosition(60, 26);
            Console.Write($"Poliser: {policeCount}");

            Console.SetCursorPosition(60, 27);
            Console.Write($"Tjuvar: {thiefCount}");

            Console.SetCursorPosition(60, 28);
            Console.Write($"Fångar: {prisonerCount}");

            Console.SetCursorPosition(60, 29);
            Console.Write($"Medborgare: {citizenCount}");

            Console.SetCursorPosition(60, 30);
            Console.Write($"Antal rånade Medborgare: {robbedCitizens}");
        }


        internal static void DisplayResult(List<Person> people, List<string> newsFeed)
        {

            Console.Clear();
            Console.WriteLine("======== SLUTRESULTAT =======");

            int policeCount = people.Count(person => person is Police);
            int thiefCount = people.Count(person => person is Thief && person.Symbol == 'T');
            int prisonerCount = people.Count(person => person is Thief && person.Symbol == 'F');
            int citizenCount = people.Count(person => person is Citizen);

            int robbedCitizens = people.OfType<Citizen>().Count(citizen => citizen.Inventory.Items.Count < 4);

            Console.Write($"Poliser: {policeCount}");
            Console.Write($"Tjuvar: {thiefCount}");
            Console.Write($"Fångar: {prisonerCount}");
            Console.Write($"Medborgare: {citizenCount}");
            Console.Write($"Antal rånade Medborgare: {robbedCitizens}");

            Console.WriteLine("Personer, Status och Inventory");

            int personIndex = 0;
            foreach (var person in people)
            {
                string type = person switch
                {
                    Police => "Polis",
                    Thief => person.Symbol == 'F' ? "Fånge" : "Tjuv",
                    Citizen => "Medborgare",

                };

                Console.WriteLine($"Person {personIndex++}: {type} - {person.Name}");
                Console.WriteLine($"Position: ({person.Position.X}, {person.Position.Y})");
                Console.WriteLine($"Inventory: {string.Join(", ", person.Inventory.Items)}");

            }
            Console.WriteLine("==== Alla Händelser ====");
            if (newsFeed.Count == 0)
            {
                Console.WriteLine("Inga händelser");
            }
            else
            {
                int i = 1;
                foreach (var news in newsFeed)
                {
                    Console.WriteLine($"{i++}. {news}");
                }
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey(true);
        }

        internal static void DrawPerson(Person person)
        {
            int x = Math.Max(0, Math.Min(Console.WindowWidth - 1, person.Position.X));
            int y = Math.Max(0, Math.Min(Console.WindowHeight - 1, person.Position.Y));
            Console.SetCursorPosition(x, y);

            // choose color
            if (person is Police)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (person is Thief && person.Symbol == 'T')
                Console.ForegroundColor = ConsoleColor.Red;
            else if (person is Thief && person.Symbol == 'F')
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (person is Citizen)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;

            Console.Write(person.Symbol);
            Console.ResetColor();
        }

    }
} 
