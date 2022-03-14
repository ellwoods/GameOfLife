//  --------------------------------------------------------------------------------------------------------------------
//  Solution : -  GameOfLife
// 
//  </copyright>
//  <summary>
// 
//  Altered - 16/03/2022 17:15 - Stephen Ellwood
// 
//  Project : - GameOfLife.Tests
// 
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using GameOfLife.Library;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     This is by no means a full or in any sense complete test suite,
    ///     it is merely intended to be indicative of some of the tests that may be used and to provide some coverage
    /// </remarks>
    [TestFixture]
    public class GolLibTests
    {
        [Test]
        public void Generate_Block_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var gen0 = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };
            var generations = 1;
            // Act
            var result = sut.Generate(gen0, generations);

            var gen1 = result[1];

            var gen1IsValid = sut.IsGenValid(gen1);

            // Assert
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(gen1IsValid, Is.True);
        }

        [Test]
        public void Generate_ReturnsExpectedGenerations_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var gen0 = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };
            var generations = 5;
            var expectedGenerations = generations + 1; // include gen0
            // Act
            var result = sut.Generate(gen0, generations);

            var actualGenerations = result.Count;

            var gen1 = result[1];

            var gen1IsValid = sut.IsGenValid(gen1);

            // Assert
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(gen1IsValid, Is.True);
            Assert.That(actualGenerations, Is.EqualTo(expectedGenerations));
        }

        [Test]
        public void NextGeneration_Block_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var gen0 = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };

            // Act
            var result = sut.NextGeneration(
                gen0);

            // Assert
            Assert.That(result, Is.Not.Null.Or.Empty);
        }

        [Test]
        [Sequential]
        public void CellOutcomeFalse_StateUnderTest_ExpectedBehavior([Values(0, 1, 5, 8)] int aliveNeighbours)
        {
            // Arrange
            var sut = new GolLib();
            var currentCellState = true;

            var expected = false;

            // Act
            var actual = sut.CellOutcome(
                currentCellState,
                aliveNeighbours);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Sequential]
        public void CellOutcomeTrue_StateUnderTest_ExpectedBehavior([Values(2, 3)] int aliveNeighbours)
        {
            // Arrange
            var sut = new GolLib();
            var currentCellState = true;

            var expected = true;

            // Act
            var actual = sut.CellOutcome(
                currentCellState,
                aliveNeighbours);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AliveNeighbours_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();


            var genblock = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };


            var generation = sut.RowsToGeneration(genblock);
            var row = 0;
            var col = 0;

            var expected = 1;

            // Act
            var actual = sut.AliveNeighbours(
                generation,
                row,
                col);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AliveNeighbours_MidBlock_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();

            var genblock = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };


            var generation = sut.RowsToGeneration(genblock);
            var row = 1;
            var col = 2;

            var expected = 3;

            // Act
            var actual = sut.AliveNeighbours(
                generation,
                row,
                col);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AliveNeighbours_All_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();

            var genblock = new List<string>
            {
                "00000",
                "01110",
                "01110",
                "01110",
                "00000"
            };


            var generation = sut.RowsToGeneration(genblock);
            var row = 2;
            var col = 2;

            var expected = 8;

            // Act
            var actual = sut.AliveNeighbours(
                generation,
                row,
                col);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsNeighbourAlive_Up_Left_Corner_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            bool[,] generation =
            {
                { false, true, false, true, false, true, false, true },
                { true, true, true, true, true, true, true, true },
                { false, false, false, false, false, false, false, false },
                { false, true, true, false, true, false, true, false },
                { false, true, true, false, true, false, true, false },
                { false, false, false, false, false, false, false, false },
            };

            var row = 0;
            var col = 0;
            var relativeRow = 0;
            var relativeCol = 1;

            var expected = true;

            // Act
            var actual = sut.IsNeighbourAlive(
                generation,
                row,
                col,
                relativeRow,
                relativeCol);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }


        [Test]
        public void IsNeighbourAlive_Left_Side_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            bool[,] generation =
            {
                { false, true, false, true, false, true, false, true },
                { true, true, true, true, true, true, true, true },
                { false, false, false, false, false, false, false, false },
                { false, true, true, false, true, false, true, false },
                { false, true, true, false, true, false, true, false },
                { false, false, false, false, false, false, false, false },
            };

            var row = 2;
            var col = 0;
            var relativeRow = 0;
            var relativeCol = 1;

            var expected = false;

            // Act
            var actual = sut.IsNeighbourAlive(
                generation,
                row,
                col,
                relativeRow,
                relativeCol);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsGenValid_JaggedIsFalse_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var gen = new List<string> { "123", "4567" };

            var expected = false;

            // Act
            var actual = sut.IsGenValid(
                gen);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }


        [Test]
        public void IsGenValid_NonJaggedIsTrue_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var gen = new List<string> { "1234", "4567", "abcd", "a1b2", "£$%^" };

            var expected = true;

            // Act
            var actual = sut.IsGenValid(
                gen);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RowsToGeneration_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = new GolLib();
            var generation = new List<string>
            {
                "0000",
                "0110",
                "0110",
                "0000"
            };


            var expected = new bool[4, 4]
            {
                { false, false, false, false },
                { false, true, true, false },
                { false, true, true, false },
                { false, false, false, false },
            };


            // Act
            var actual = sut.RowsToGeneration(
                generation);


            // Assert
            Assert.That(actual, Is.Not.Null.Or.Empty);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}