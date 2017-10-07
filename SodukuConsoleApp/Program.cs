using Sudoku;
using System;
using System.Text.RegularExpressions;

namespace SodukuConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to sudoku console app!");
            Console.WriteLine("Add values using \"add {row} {column} {value}\"");
            Console.WriteLine("Check the board state using \"check\"");
            Console.WriteLine("Quit using \"quit\"");

            var board = new Board();

            var command = Console.ReadLine();

            while (!command.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    if (command.StartsWith("add"))
                    {
                        var arguments = Regex.Match(command, @"add (\d) (\d) (\d)");

                        var rowIndex = int.Parse(arguments.Groups[1].Value);
                        var columnIndex = int.Parse(arguments.Groups[2].Value);
                        var value = int.Parse(arguments.Groups[3].Value);

                        board.SetTileValue(rowIndex, columnIndex, value);
                    }
                    else if (command.StartsWith("check"))
                    {
                        Console.WriteLine(board);
                    }
                    else if (command.StartsWith("path", StringComparison.CurrentCultureIgnoreCase))
                    {
                        foreach (var line in board.GetSolutionPath)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else if (command.Equals("reset", StringComparison.CurrentCultureIgnoreCase))
                    {
                        board = new Board();
                    }
                    else
                    {
                        Console.WriteLine($"Invalid command {command}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid command {command}. Exception throw {ex.Message}");
                    throw;
                }

                command = Console.ReadLine();
            }
        }
    }
}
