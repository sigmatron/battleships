using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships;
using FluentAssertions;
using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void GoodBoard_Success_Hit1Deck()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0", "3:3" };
            Game.Play(ships, guesses).Should().Be(1);
        }
        
        [Fact]
        public void GoodBoard_Success_Hit3Deck()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:1", "3:8", "3:9", "3:7" };
            Game.Play(ships, guesses).Should().Be(1);
        }
        
        [Fact]
        public void GoodBoard_Success_HitNone()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:1", "3:1", "3:6", "3:7" };
            Game.Play(ships, guesses).Should().Be(0);
        }
        
        [Fact]
        public void GoodBoard_Success_HitAll()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0", "0:1", "0:0", "1:6", "1:7", "1:7", "3:9", "3:7", "3:8", "4:0", "4:2", "4:3", "4:1" };
            Game.Play(ships, guesses).Should().Be(4);
        }
        
        [Fact]
        public void BadBoard_Throws_WrongShipsNumber()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_DiagonalShip()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,3:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_AdjacentShips()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:3,3:5", "4:0,4:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_LongShip()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:3,3:5", "4:0,4:5"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_BadShipCoordinatesCount()
        {
            var ships = new[] { "0:0,0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_BadShipCoordinatesFormat1()
        {
            var ships = new[] { "0:0:0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_BadShipCoordinatesFormat2()
        {
            var ships = new[] { "0;0,0;0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_BadGuessCoordinatesFormat()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0,0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_GuessCoordinatesOutOfRange()
        {
            var ships = new[] { "0:0,0:0", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:10" };
            Assert.Throws<ArgumentOutOfRangeException>(() => Game.Play(ships, guesses));
        }
        
        [Fact]
        public void BadBoard_Throws_ShipCoordinatesOutOfRange()
        {
            var ships = new[] { "10:10,10:10", "1:6,1:7", "3:9,3:7", "4:0,4:3"};
            var guesses = new[] { "0:0" };
            Assert.Throws<FormatException>(() => Game.Play(ships, guesses));
        }
    }
}
