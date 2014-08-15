using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Sudoku.Solve
{
    public class SolverWrapper
    {
        public int[][] Solve(int?[][] grid)
        {
            var solver = new Solver();
            var input = ConvertJaggedNullableToTwoDimensionalNullable(grid);

            int[,] result = solver.Solve(input);

            return ConvertTwoDimensionalToJagged(result);
        }

        private static int?[,] ConvertJaggedNullableToTwoDimensionalNullable(int?[][] original)
        {
            int?[,] result = new int?[9, 9];
            for (int i = 0; i < Math.Min(9, original.Length); i++)
            {
                for (int j = 0; j < Math.Min(9, original[i].Length); j++)
                {
                    if (original[i][j] < 1 || original[i][j] > 9)
                    {
                        throw new InvalidSudokuException();
                    }
                    result[i, j] = original[i][j];
                }
            }
            return result;
        }

        private static int[][] ConvertTwoDimensionalToJagged(int[,] original)
        {
            int[][] jagged = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                jagged[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    jagged[i][j] = original[i, j];
                }
            }
            return jagged;
        }
    }
}