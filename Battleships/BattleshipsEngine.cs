using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Objects;

namespace Battleships
{
    internal class BattleshipsEngine
    {
        private const int ShipTotalCount = 4;

        private readonly int[] shipsCountByDecks = {0, 1, 1, 1, 1};

        // -1 - no ship, >= 0 - shipIndex
        private readonly int[,] board;
        
        public BattleshipsEngine()
        {
            this.board = new int[10, 10];
        }

        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses
        public int Play(string[] shipsStr, string[] guessStr)
        {
            this.ResetBoard();

            if (shipsStr.Length != ShipTotalCount)
                throw new FormatException($"Bad number of ships: {shipsStr.Length}, should be exactly {ShipTotalCount}");

            var shipsParsed = new Ship[ShipTotalCount];
            for (int i = 0; i < ShipTotalCount; i++)
            {
                shipsParsed[i] = Ship.Parse(shipsStr[i]);
            }

            for (int i = 1; i <= 4; i++)
            {
                if (shipsParsed.Count(sh => sh.Length == i) != this.shipsCountByDecks[i])
                    throw new FormatException($"{i} deck ships count wrong, should be: {this.shipsCountByDecks[i]}");
            }

            for (int i = 0; i < shipsParsed.Length; i++)
            {
                this.TryPlaceShip(shipsParsed[i], i);
            }

            var guessesParsed = new Coordinates[guessStr.Length];
            for (int i = 0; i < guessStr.Length; i++)
                guessesParsed[i] = Coordinates.Parse(guessStr[i]);


            var count = 0;
            foreach (var g in guessesParsed)
            {
                if (this.board[g.Row, g.Col] == -1) continue;
                
                var index = this.board[g.Row, g.Col];
                shipsParsed[index].Length--;
                this.board[g.Row, g.Col] = -1;
                if (shipsParsed[index].Length == 0)
                    count++;
            }

            return count;
        }

        private bool IsShipCell(int r, int c)
        {
            if (r < 0 || r > 9 || c < 0 || c > 9)
                return false;
            return this.board[r, c] != -1;
        }
        
        private void TryPlaceShip(Ship ship, int shipIndex)
        {
            for (var r = ship.Start.Row; r <= ship.End.Row; r++)
            {
                for (var c = ship.Start.Col; c <= ship.End.Col; c++)
                {
                    for (var dr = -1; dr <= 1; dr++)
                    for (var dc = -1; dc <= 1; dc++)
                        if (IsShipCell(r + dr, c + dc))
                            throw new FormatException("There are adjacent ships on the board");
                }
            }
            
            for (var r = ship.Start.Row; r <= ship.End.Row; r++)
            {
                for (var c = ship.Start.Col; c <= ship.End.Col; c++)
                    this.board[r, c] = shipIndex;
            }
        }

        private void ResetBoard()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    this.board[r, c] = -1;
                }
            }
        }
    }
}