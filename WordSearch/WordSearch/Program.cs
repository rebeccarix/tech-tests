using System;

namespace WordSearch
{
    class Program
    {
        static char[,] Grid = new char[,] {
            {'C', 'P', 'K', 'X', 'O', 'I', 'G', 'H', 'S', 'F', 'C', 'H'},
            {'Y', 'G', 'W', 'R', 'I', 'A', 'H', 'C', 'Q', 'R', 'X', 'K'},
            {'M', 'A', 'X', 'I', 'M', 'I', 'Z', 'A', 'T', 'I', 'O', 'N'},
            {'E', 'T', 'W', 'Z', 'N', 'L', 'W', 'G', 'E', 'D', 'Y', 'W'},
            {'M', 'C', 'L', 'E', 'L', 'D', 'N', 'V', 'L', 'G', 'P', 'T'},
            {'O', 'J', 'A', 'A', 'V', 'I', 'O', 'T', 'E', 'E', 'P', 'X'},
            {'C', 'D', 'B', 'P', 'H', 'I', 'A', 'W', 'V', 'X', 'U', 'I'},
            {'L', 'G', 'O', 'S', 'S', 'B', 'R', 'Q', 'I', 'A', 'P', 'K'},
            {'E', 'O', 'I', 'G', 'L', 'P', 'S', 'D', 'S', 'F', 'W', 'P'},
            {'W', 'F', 'K', 'E', 'G', 'O', 'L', 'F', 'I', 'F', 'R', 'S'},
            {'O', 'T', 'R', 'U', 'O', 'C', 'D', 'O', 'O', 'F', 'T', 'P'},
            {'C', 'A', 'R', 'P', 'E', 'T', 'R', 'W', 'N', 'G', 'V', 'Z'}
        };

        static string[] Words = new string[] 
        {
            "CARPET",
            "CHAIR",
            "DOG",
            "BALL",
            "DRIVEWAY",
            "FISHING",
            "FOODCOURT",
            "FRIDGE",
            "GOLF",
            "MAXIMIZATION",
            "PUPPY",
            "SPACE",
            "TABLE",
            "TELEVISION",
            "WELCOME",
            "WINDOW"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Word Search");

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    Console.Write(Grid[y, x]);
                    Console.Write(' ');
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("Found Words");
            Console.WriteLine("------------------------------");

            FindWords();

            Console.WriteLine("------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        private static void FindWords()
        {
            foreach (string word in Words)
            {
                FindWord(word);
            }
        }

        static void FindWord(string word)
        {
            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (SearchFromPosition(word, row, col))
                    {
                        Console.WriteLine($"{word} found at ({row + 1},{col + 1}) to ({row + 1},{col - word.Length + 2})");
                        return;
                    }
                }
            }

            Console.WriteLine($"{word} not found.");
        }

        static bool SearchFromPosition(string word, int startRow, int startCol)
        {
            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);

            if (startCol + word.Length <= cols)
            {
                bool match = true;
                for (int i = 0; i < word.Length; i++)
                {
                    if (Grid[startRow, startCol + i] != word[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return true;
            }

            if (startRow + word.Length <= rows)
            {
                bool match = true;
                for (int i = 0; i < word.Length; i++)
                {
                    if (Grid[startRow + i, startCol] != word[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return true;
            }

            if (startRow + word.Length <= rows && startCol + word.Length <= cols)
            {
                bool match = true;
                for (int i = 0; i < word.Length; i++)
                {
                    if (Grid[startRow + i, startCol + i] != word[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return true;
            }

            return false;
        }
    }
}
