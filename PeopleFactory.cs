using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TjuvOchPolis;

namespace TjuvOchPolis1
{
    internal class PeopleFactory
    {
        private static Random rand = new Random();

        public static List<Person> CreatePeople(int width, int height)
        {
            var people = new List<Person>();

            string[] policeName = { "Anders", "Björn", "Carl", "David", "Erik", "Fredrik", "Gustav", "Henrik", "Isak", "Johan" };

            string[] thiefName = { "Kalle", "Lars", "Mats", "Nisse", "Olle", "Per", "Quentin", "Rolf", "Sven", "Tommy", "Ulf", "Ville", "Willy", "Xander", "Yngve", "Zlatan", "Nemer", "Qudsia", "Dzelila", "Micke"};

            string[] citizenName = { "Anna", "Berit", "Cecilia", "Diana", "Eva", "Frida", "Gunilla", "Helena",
                "Ingrid", "Johanna", "Karin", "Lena", "Maria", "Nina", "Olivia", "Petra", "Qiana", "Rebecca", "Sara", "Therese", "Axel", "Elsa", "Viktor", "Freja", "Leo", "Alma", "Emil", "Nora", "Hugo", "Maja" };


            // Skapa poliser
            for (int i = 0; i < 10; i++)
                people.Add(new Police(policeName[i], new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            // Skapa tjuvar
            for (int i = 0; i < 20; i++)
                people.Add(new Thief(thiefName[i], new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            // Skapa medborgare
            for (int i = 0; i < 30; i++)
                people.Add(new Citizen(citizenName[i], new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            

            return people;
        }
    }
}

