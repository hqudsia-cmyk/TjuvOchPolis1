using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TjuvOchPolis1;

namespace TjuvOchPolis
{
    class Program
    {
        static Random rand = new Random();

        static void Main()
        {

           

            int width = 100;
            int height = 25;

            int prisonWidth = 10;
            int prisonHeight = 10;
            int startY = height + 1;
            DrawBorder(width, height);

            DrawPrisonBorder(prisonWidth, prisonHeight, startY);

            var people = new List<Person>();

            // Skapa personer 
            for (int i = 0; i < 10; i++)
                people.Add(new Police($"Polis{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));
            for (int i = 0; i < 20; i++)
                people.Add(new Thief($"Tjuv{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));
            for (int i = 0; i < 30; i++)
                people.Add(new Citizen($"Medborgare{i + 1}", new Position(rand.Next(1, width - 1), rand.Next(1, height - 1))));

            // Rita första gången 
            foreach (var person in people)
            {
                Console.SetCursorPosition(person.Position.X, person.Position.Y);
                Console.Write(person.Symbol);
            }

            int stepCounter = 0;
            int stepsBeforeChange = 5;

            // Huvudloop 
            while (true)
            {
                //  Radera gamla positioner
                foreach (var person in people)
                {
                    Console.SetCursorPosition(person.Position.X, person.Position.Y);
                    Console.Write(" ");
                }

                // Byt riktning ibland
                stepCounter++;
                if (stepCounter % stepsBeforeChange == 0)
                    foreach (var person in people)
                        person.ChangeDirection();

                //  Flytta och wrap-around
                foreach (var person in people)
                {
                    person.Move();

                    if (person.Position.X <= 0) person.Position = new Position(width - 2, person.Position.Y);
                    else if (person.Position.X >= width - 1) person.Position = new Position(1, person.Position.Y);

                    if (person.Position.Y <= 0) person.Position = new Position(person.Position.X, height - 2);
                    else if (person.Position.Y >= height - 1) person.Position = new Position(person.Position.X, 1);
                }

                // Interaktioner
                foreach (var thief in people.OfType<Thief>())
                {
                    foreach (var citizen in people.OfType<Citizen>())
                    {
                        if (thief.Position.X == citizen.Position.X && thief.Position.Y == citizen.Position.Y)
                        {
                            Interactions.HandleRobbery(thief, citizen, people, width, height);
                        }
                    }

                    foreach (var police in people.OfType<Police>())
                    {
                        if (thief.Position.X == police.Position.X && thief.Position.Y == police.Position.Y)
                        {
                            Interactions.HandleArrest(police, thief, people, width, height);
                        }
                    }
                }



                // Rita nya positioner
                foreach (var person in people)
                {
                    Console.SetCursorPosition(person.Position.X, person.Position.Y);
                    Console.Write(person.Symbol);
                }

                Thread.Sleep(400);
            }
        }


       

        // Rita upp staden
        static void DrawBorder(int width, int height)
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(2, 0);
            Console.Write("City");
        }


        static void DrawPrisonBorder(int prisonWidth, int prisonHeight, int startY)
        {
            for (int y = 0; y < prisonHeight; y++)
            {
                for (int x = 0; x < prisonWidth; x++)
                {
                    Console.SetCursorPosition(x, y + startY);
                    if (y == 0 || y == prisonHeight - 1 || x == 0 || x == prisonWidth - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
            }

            Console.SetCursorPosition(2, startY); // Titel på fängelset
            Console.Write("Prison");
        }


    }
}
