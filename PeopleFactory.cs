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

            // Skapa poliser
            for (int i = 0; i < 10; i++)
                people.Add(new Police($"Polis{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            // Skapa tjuvar
            for (int i = 0; i < 20; i++)
                people.Add(new Thief($"Tjuv{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            // Skapa medborgare
            for (int i = 0; i < 30; i++)
                people.Add(new Citizen($"Medborgare{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            return people;
        }
    }
}

