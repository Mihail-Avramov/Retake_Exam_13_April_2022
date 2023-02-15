using System;
using System.Linq;

namespace TruffleHunter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());

            char[,] forest = new char[size, size];

            int bCounter = 0;
            int sCounter = 0;
            int wCounter = 0;
            int wbCounter = 0;

            for (int row = 0; row < size; row++)
            {
                char[] trufflesInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                
                for (int col = 0; col < size; col++)
                {
                    forest[row,col] = trufflesInfo[col];
                }
            }

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "Stop the hunt")
            {
                string[] commandSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = commandSplit[0];

                if (command == "Collect")
                {
                    int currentRow = int.Parse(commandSplit[1]);
                    int currentCol = int.Parse(commandSplit[2]);
                    char currentValue = forest[currentRow, currentCol];

                    switch (currentValue)
                    {
                        case 'B':
                            bCounter++;
                            break;
                        case 'S':
                            sCounter++;
                            break;
                        case 'W':
                            wCounter++;
                            break;
                    }

                    forest[currentRow, currentCol] = '-';
                }
                else if(command == "Wild_Boar")
                {
                    int currentRow = int.Parse(commandSplit[1]);
                    int currentCol = int.Parse(commandSplit[2]);
                    string direction = commandSplit[3];
                    char currentValue = forest[currentRow, currentCol];

                    if (currentValue == 'B' || currentValue == 'S' || currentValue == 'W')
                    {
                        wbCounter++;
                        forest[currentRow, currentCol] = '-';
                    }

                    while (IsNextIndexValid(direction, currentRow, currentCol, forest))
                    {
                        switch (direction)
                        {
                            case "up":
                                currentRow -= 2;
                                break;
                            case "down":
                                currentRow += 2;
                                break;
                            case "left":
                                currentCol -= 2;
                                break;
                            case "right":
                                currentCol += 2;
                                break;
                        }

                        //currentValue = forest[currentRow, currentCol];

                        if (currentValue == 'B' || currentValue == 'S' || currentValue == 'W')
                        {
                            wbCounter++;
                            forest[currentRow, currentCol] = '-';
                        }
                    }
                }
            }

            Console.WriteLine($"Peter manages to harvest {bCounter} black, {sCounter} summer, and {wCounter} white truffles.");
            Console.WriteLine($"The wild boar has eaten {wbCounter} truffles.");
            PrintMatrix(forest);
        }

        private static bool IsNextIndexValid(string direction, int row, int col, char[,] forest)
        {
            switch (direction)
            {
                case "up":
                    row -= 2;
                    return (row >= 0 && row < forest.GetLength(0));
                case "down":
                    row += 2;
                    return (row >= 0 && row < forest.GetLength(0));
                case "left":
                    col -= 2;
                    return (col >= 0 && col < forest.GetLength(1));
                case "right":
                    col += 2;
                    return (col >= 0 && col < forest.GetLength(1));
                default:
                    return false;
            }
        }

        private static void PrintMatrix(char[,] forest)
        {
            for (int row = 0; row < forest.GetLength(0); row++)
            {
                for (int col = 0; col < forest.GetLength(1); col++)
                {
                    Console.Write($"{forest[row, col]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
