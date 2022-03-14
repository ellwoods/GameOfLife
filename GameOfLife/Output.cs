//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file=Output.cs company="North Lincolnshire Council">
//  Solution : -  GameOfLife
// 
//  </copyright>
//  <summary>
// 
//  Created - 16/03/2022 01:38
//  Altered - 16/03/2022 17:13 - Stephen Ellwood
// 
//  Project : - GameOfLife
// 
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife;

/// <summary>
///     console output
/// </summary>
public class Output
{

    /// <summary>
    ///     output a generation
    /// </summary>
    /// <param name="generation">the generation to output <seealso cref="PrintRow" /></param>
    private void PrintGeneration(List<string> generation)
    {
        foreach (var row in generation) Console.WriteLine(row);
    }

    /// <summary>
    ///     output all generations
    /// </summary>
    /// <param name="generations">the generations to output <seealso cref="PrintGeneration" /></param>
    public void PrintGenerations(List<List<string>> generations)
    {
        var i = 0;

        foreach (var gen in generations)
        {
            Console.WriteLine("Generation " + i);
            PrintGeneration(gen);

            i++;
        }
    }
}