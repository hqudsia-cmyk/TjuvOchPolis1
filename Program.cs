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
            Console.SetBufferSize(120, 50);
            Console.SetWindowSize(120, 50);


            int width = 100;
            int height = 25;

            int prisonWidth = 15;
            int prisonHeight = 15;
            int startY = height - 15;
            int startX = width + 1;
            DrawBorder(width, height);

            DrawPrisonBorder(prisonWidth, prisonHeight, startY, startX);

            List<Person> people = PeopleFactory.CreatePeople(width, height);

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
                    if (person.Symbol == 'F') // fångar
                    {
                        ((Thief)person).MoveInPrison(startX, startY, prisonWidth, prisonHeight);
                    }
                    else
                    {
                        person.Move();

                        if (person.Position.X <= 0) person.Position = new Position(width - 2, person.Position.Y);
                        else if (person.Position.X >= width - 1) person.Position = new Position(1, person.Position.Y);

                        if (person.Position.Y <= 0) person.Position = new Position(person.Position.X, height - 2);
                        else if (person.Position.Y >= height - 1) person.Position = new Position(person.Position.X, 1);
                    }
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
                            Interactions.HandleArrest(police, thief, people, width, height, startX, startY, prisonWidth, prisonHeight);
                        }
                    }
                }

                foreach (var police in people.OfType<Police>())
                {
                    foreach (var citizen in people.OfType<Citizen>())
                    {
                        if (police.Position.X == citizen.Position.X && police.Position.Y == citizen.Position.Y)
                        {
                            Interactions.CitizenGreetings(citizen, police, people, width, height);
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

            static void DrawPrisonBorder(int prisonWidth, int prisonHeight, int startY, int startX)
            {
                for (int y = 0; y < prisonHeight; y++)
                {
                    for (int x = 0; x < prisonWidth; x++)
                    {
                        Console.SetCursorPosition(startX + x, y + startY);
                        if (y == 0 || y == prisonHeight - 1 || x == 0 || x == prisonWidth - 1)
                            Console.Write("#");
                        else
                            Console.Write(" ");
                    }
                }

                Console.SetCursorPosition(startX, startY);
                Console.Write("Prison");

            }


        }
    }
}
