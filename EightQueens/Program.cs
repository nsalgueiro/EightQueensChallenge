using log4net;
using log4net.Config;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace EightQueens
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static int[,] grid = new int[8, 8];
        static int solutions = 0;

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            XmlConfigurator.Configure(new FileInfo("/Users/nelsonsalgueiro/Projects/EightQueens/EightQueens/log4net.config"));

            placeQueen(0);

            log.Info("Number of solutions found: "+ solutions);
        }

        private static void placeQueen(int queen)
        {
            // Try to place the Queen in the row corresponding to it's number as there will be only one per row
            for(int column=0; column<8;column++)
            {
                
                if ( IsSafeSquare(queen, column))
                {
                    log.InfoFormat("Placing queen {0} at ({0},{1})", queen, column);
                    grid[queen, column] = 1;
                    if (queen + 1 < 8)
                    {
                        placeQueen(queen + 1);
                    }
                    else
                    {
                        Console.WriteLine("Wait do we have a solution???");
                        printGrid();
                        solutions++;
                    }

                    grid[queen, column] = 0;
                }
            }
        }

        private static bool IsSafeSquare(int x, int y)
        {
            return IsRowSafe() && IsColumnSafe(y) && AreDiagonalsSafe(x,y);
        }


        // Since we are going over the rows one by one for placing the queens this will be always safe, just leaving for now for visualization
        private static bool IsRowSafe()
        {
            return true;
        }

        private static bool IsColumnSafe(int y)
        {
            for (int row=0; row<8; row++)
            {
                if ( grid[row, y] == 1 ) {
                    return false;
                }
            }

            return true;
        }

        private static bool AreDiagonalsSafe(int x, int y)
        {
            for(int i=0; i<=7; i++)
            {
                var rowValue1 = i + (x - y);
                var rowValue2 = (x + y) - i;
                var col = i;

                if (rowValue1 >=0 && rowValue1 <=7 )
                {
                    if (grid[rowValue1, col] == 1)
                    {
                        return false;
                    }
                }

                if (rowValue2 >=0 && rowValue2<=7) {
                    if (grid[rowValue2, col] == 1)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        private static void printGrid()
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    Console.Write("{0} ", grid[i, j]);
                }
                Console.WriteLine();
            }
            
        }
    }
}
