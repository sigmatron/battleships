using System;

namespace Battleships.Objects
{
    public struct Coordinates
    {
        public static Coordinates Parse(string str)
        {
            var coordinates = str.Split(':');
            if (coordinates.Length != 2)
                throw new FormatException($"Bad coordinate count");
            if (!Int32.TryParse(coordinates[0], out var row))
                throw new FormatException($"Bad coordinate row: {coordinates[0]}");
            if (!Int32.TryParse(coordinates[1], out var col))
                throw new FormatException($"Bad coordinate col: {coordinates[1]}");

            if (row < 0 || row > 9)
                throw new ArgumentOutOfRangeException($"Bad coordinate row, {row}, should be within range [0, 9]");
            if (col < 0 || col > 9)
                throw new ArgumentOutOfRangeException($"Bd coordinate col, {col}, should be within range [0, 9]");

            return new Coordinates() { Row = row, Col = col };
        }
        public int Row, Col;
    }
}