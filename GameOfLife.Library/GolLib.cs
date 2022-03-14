//  --------------------------------------------------------------------------------------------------------------------
//  Solution : -  GameOfLife
// 
//  </copyright>
//  <summary>
// 
//  Altered - 16/03/2022 17:13 - Stephen Ellwood
// 
//  Project : - GameOfLife.Library
// 
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife.Library;

/// <summary>
///     Game of Life Library code
/// </summary>
/// <remarks>Handles generation functions where a generation is a 2D bool array</remarks>
public class GolLib
{
    public int RowCount(bool[,] generation)
    {
        return generation.GetLength(0);
    }

    public int ColCount(bool[,] generation)
    {
        return generation.GetLength(1);
    }


    /// <summary>
    ///     Generate the output
    /// </summary>
    /// <param name="gen0">First generation as provided</param>
    /// <param name="generations">number of generations</param>
    public List<List<string>> Generate(List<string> gen0, int generations)
    {
        var allGenerations = new List<List<string>> { gen0 };

        var currentGen = gen0;


        for (var gen = 1; gen <= generations; gen++)
        {
            var nextGen = NextGeneration(currentGen);
            allGenerations.Add(nextGen);
            currentGen = nextGen;
        }

        return allGenerations;
    }

    /// <summary>
    ///     Create the next generation based on an existing one
    /// </summary>
    /// <param name="generation">existing generation</param>
    /// <returns> the next generation based on the existing one and the rules <seealso cref="CellOutcome" /></returns>
    public List<string> NextGeneration(List<string> generation)
    {
        var result = new List<string>();

        var boolGeneration = RowsToGeneration(generation);

        for (var row = 0; row < RowCount(boolGeneration); row++)
        {
            var newRow = "";

            for (var col = 0; col < ColCount(boolGeneration); col++)
            {
                var aliveNeighbours = AliveNeighbours(boolGeneration, row, col);
                var currentState = boolGeneration[row, col];

                var cellOutcome = CellOutcome(currentState, aliveNeighbours);

                if (cellOutcome)
                    newRow += "1";
                else
                    newRow += "0";
            }

            result.Add(newRow);
        }

        return result;
    }


    /// <summary>
    ///     Determine cell outcome
    /// </summary>
    /// <param name="currentCellState">is current cell alive</param>
    /// <param name="aliveNeighbours">how many live neighbours does current cell have</param>
    /// <returns>
    ///     1. If a cell is live and it has less than 2 live neighbours, it dies
    ///     2. If a cell is live and has more than 3  live neighbours, it dies
    ///     3. If a cell is live and it has 2 or 3 live neighbours, it lives
    ///     4. If a cell is dead and it has 3 live neighbours, it lives
    ///     5. a dead cell becomes a live cell 10% of the time
    /// </returns>
    public bool CellOutcome(bool currentCellState, int aliveNeighbours)
    {
        if (currentCellState && aliveNeighbours < 2 || currentCellState && aliveNeighbours > 3) return false;

        if (currentCellState && aliveNeighbours == 2 || currentCellState && aliveNeighbours == 3) return true;

        if (!currentCellState && aliveNeighbours == 3) return true;

        // simplistic random number generator

        var rnd = new Random();
        var selection = rnd.Next(0, 9);


        return !currentCellState && selection == 3;
    }

    /// <summary>
    ///     Determine how many neighbours a cell has that are alive
    /// </summary>
    /// <param name="generation">The generation to check</param>
    /// <param name="row">row number</param>
    /// <param name="col">column number</param>
    /// <returns>a count of the number of neighbours of a cell that are alive</returns>
    public int AliveNeighbours(bool[,] generation, int row, int col)
    {
        var upRowCount = 0;
        var sameRowCount = 0;
        var downRowCount = 0;

        // row above
        for (var i = -1; i <= 1; i++)
            if (IsNeighbourAlive(generation, row, col, -1, i))
                upRowCount++;

        // same row left
        if (IsNeighbourAlive(generation, row, col, 0, -1)) sameRowCount++;
        // same row right
        if (IsNeighbourAlive(generation, row, col, 0, 1)) sameRowCount++;

        // row below
        for (var i = -1; i <= 1; i++)
            if (IsNeighbourAlive(generation, row, col, 1, i))
                downRowCount++;

        return upRowCount + sameRowCount + downRowCount;
    }

    /// <summary>
    ///     is a relative neighbour alive
    /// </summary>
    /// <param name="row">row of cell to check</param>
    /// <param name="col">column of cell to check</param>
    /// <param name="relativeRow">-1 = row above, 0 = same row, 1 = row below</param>
    /// <param name="relativeCol">-1 = left column, 0 = same column, 1 = right column</param>
    /// <returns>
    ///     true if the neighbour is alive, false if the relative cell is out of bounds, the same as the cell to check or
    ///     if the relative cell is not alive
    /// </returns>
    public bool IsNeighbourAlive(bool[,] generation, int row, int col, int relativeRow, int relativeCol)
    {
        var maxRows = RowCount(generation);
        var maxCols = ColCount(generation);

        // eliminate same cell
        if (relativeCol == 0 && relativeRow == 0) return false;

        // now do edges 
        //top
        if (row == 0 && relativeRow == -1) return false;

        // bottom
        if (row == maxRows - 1 && relativeRow == 1) return false;

        // left 
        if (col == 0 && relativeCol == -1) return false;

        // right
        if (col == maxCols - 1 && relativeCol == 1) return false;

        return generation[row + relativeRow, col + relativeCol];
    }


    /// <summary>
    ///     Is the generation valid
    /// </summary>
    /// <param name="gen">generation to check</param>
    /// <returns>true if all strings have same length, false otherwise</returns>
    public bool IsGenValid(List<string> gen)
    {
        var first = gen.First().Length;

        var output = true;

        foreach (var row in gen)
            if (row.Length != first)
                output = false;
        return output;
    }


    /// <summary>
    ///     Convert string rows to a generation
    /// </summary>
    /// <param name="generation">the strings to convert</param>
    /// <returns>the input data represented as a 2d array of bool <seealso cref="RowToCells" /></returns>
    public bool[,] RowsToGeneration(List<string> generation)
    {
        var first = generation.First();

        var numRows = generation.Count;
        var numCols = first.Length;

        var input = generation.ToArray();

        var result = new bool[numRows, numCols];

        for (var row = 0; row < numRows; row++)
        {
            var newRow = RowToCells(input[row]);
            for (var i = 0; i < newRow.Length; i++) result[row, i] = newRow[i];
        }

        return result;
    }


    /// <summary>
    ///     Converts a string to a bool array
    /// </summary>
    /// <param name="input">the string to convert</param>
    /// <returns>any 0 in the string is converted to false (dead), any other character is converted to true (alive)</returns>
    public bool[] RowToCells(string input)
    {
        var cells = new bool[input.Length];

        for (var i = 0; i < input.Length; i++)
            if (input.Substring(i, 1) == "0")
                cells[i] = false;
            else
                cells[i] = true;

        return cells;
    }
}