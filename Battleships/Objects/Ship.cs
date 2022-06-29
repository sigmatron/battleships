using System;

namespace Battleships.Objects
{
    public struct Ship
    {
        public Coordinates Start, End;
        public int Length;
        
        public static Ship Parse(string str)
        {
            try
            {
                var coordinates = str.Split(',');
                if (coordinates.Length != 2)
                    throw new FormatException($"Bad length of ship coordinates {coordinates.Length}. Should be 2");

                var start = Coordinates.Parse(coordinates[0]);
                var end = Coordinates.Parse(coordinates[1]);
                
                if (start.Row != end.Row && start.Col != end.Col)
                    throw new FormatException($"Ship is aligned diagonally");

                var length = Math.Abs(start.Row - end.Row) + Math.Abs(start.Col - end.Col) + 1;
                if (length > 4)
                    throw new FormatException($"Length: {length} is too large, should be below 4");

                //Makes ship to always start from lower row + column
                var orderedStart = new Coordinates { Row = Math.Min(start.Row, end.Row), Col = Math.Min(start.Col, end.Col) };
                var orderedEnd = new Coordinates { Row = Math.Max(start.Row, end.Row), Col = Math.Max(start.Col, end.Col) };
                
                return new Ship() { Start = orderedStart, End = orderedEnd, Length = length };
            }
            catch (Exception e)
            {
                throw new FormatException($"Could not create ship: {str}" + e.Message, e);
            }
        }
    }
}