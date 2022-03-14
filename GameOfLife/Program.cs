//  --------------------------------------------------------------------------------------------------------------------
//  Solution : -  GameOfLife
// 
//  </copyright>
//  <summary>
// 
//  Altered - 16/03/2022 17:13 - Stephen Ellwood
// 
//  Project : - GameOfLife
// 
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

using GameOfLife;
using GameOfLife.Library;

// Introduction and instructions

Console.WriteLine("Hello, Welcome to the Game of Life");

Console.WriteLine(
    "The first generation is Generation 0, how many other generations would you like?");
Console.WriteLine(" Please enter a number then press ENTER, or enter a non numeric to quit");
var genCount = Console.ReadLine();

if (!int.TryParse(genCount, out var generationCount))
{
    Console.WriteLine("Non numeric value entered. Exiting Game of Life");
    Environment.Exit(0);
}

Console.WriteLine(generationCount + " Generations");

var gen0 = new List<string>();

Console.WriteLine(
    "To create generation 0, enter any string and press ENTER,");
Console.WriteLine(" when you have finished just press enter without entering a string,");
Console.WriteLine(" your strings should all have the same number of characters, ");
Console.WriteLine(
    " 0 will be interpreted as a dead cell and anything else, including spaces, will be interpreted as a live cell (1)");

var exit = false;
while (!exit)
{
    var input = Console.ReadLine();

    if (input == null || input.Trim() == "")
        exit = true;
    else
        gen0.Add(input);
}

// is the gen valid
var gol = new GolLib();

var isValid = gol.IsGenValid(gen0);

if (!isValid)
{
    Console.WriteLine("input appears to be invalid, exiting");
    Environment.Exit(0);
}

// create and output the number of generations requested
var allGenerations = gol.Generate(gen0, generationCount);

var printer = new Output();

printer.PrintGenerations(allGenerations);