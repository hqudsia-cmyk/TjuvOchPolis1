using System;

namespace TjuvOchPolis
{
    internal struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y) { X = x; Y = y; }
    }

    internal struct Direction
    {
        public int X { get; set; }
        public int Y { get; set; }
        private static Random rand = new Random();

        public Direction(int x, int y) { X = x; Y = y; }

        public static Direction RandomDirection()
        {
            return new Direction(rand.Next(-1, 2), rand.Next(-1, 2));
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        public char Symbol { get; set; }
        public Inventory Inventory { get; set; }

        public Person(string name, Position position, Direction direction, char symbol, Inventory inventory)
        {
            Name = name;
            Position = position;
            Direction = direction;
            Symbol = symbol;
            Inventory = inventory;
        }

        public void Move()
        {
            Position = new Position(Position.X + Direction.X, Position.Y + Direction.Y);
        }

        public void MoveInPrison(int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight)
        {
            
            Position = new Position(Position.X + Direction.X, Position.Y + Direction.Y);

            // Vänd riktning om vi träffar fängelsevägg
            if (Position.X <= prisonStartX + 1 || Position.X >= prisonStartX + prisonWidth - 2)
                Direction = new Direction(-Direction.X, Direction.Y);

            if (Position.Y <= prisonStartY + 1 || Position.Y >= prisonStartY + prisonHeight - 2)
                Direction = new Direction(Direction.X, -Direction.Y);

            // Se till att personen är inom gränserna
            Position = new Position(
                Math.Clamp(Position.X, prisonStartX + 1, prisonStartX + prisonWidth - 2),
                Math.Clamp(Position.Y, prisonStartY + 1, prisonStartY + prisonHeight - 2)
            );
        }



        public void ChangeDirection()
        {
            Direction = Direction.RandomDirection();
        }
    }

    internal class Police : Person
    {
        public Police(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'P', new PoliceInventory()) { }
    }

    internal class Thief : Person
    {
        public Thief(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'T', new ThiefInventory()) { }
    }

    internal class Citizen : Person
    {
        public Citizen(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'C', new CitizenInventory()) { }
    }
}
