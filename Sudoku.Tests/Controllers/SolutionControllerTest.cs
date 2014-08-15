using System;
using System.Web.Http;
using NUnit.Framework;
using Sudoku.Controllers;
using Sudoku.Solve;

namespace Sudoku.Tests.Controllers
{
    [TestFixture]
    public class SolutionControllerTest : ApiController
    {
        private readonly SolutionController controller = new SolutionController();

        [Test]
        public void PostValidSudokuToSolve()
        {
            // From http://www.sudoku.ws/hard-1.htm
            var validSudoku = new int?[][]
            {
                new int?[] {null, null, null, 2, null, null, null, 6, 3},
                new int?[] {3, null, null, null, null, 5, 4, null, 1},
                new int?[] {null, null, 1, null, null, 3, 9, 8, null},
                new int?[] {null, null, null, null, null, null, null, 9, null},
                new int?[] {null, null, null, 5, 3, 8, null, null, null},
                new int?[] {null, 3, null, null, null, null, null, null, null},
                new int?[] {null, 2, 6, 3, null, null, 5, null, null},
                new int?[] {5, null, 3, 7, null, null, null, null, 8},
                new int?[] {4, 7, null, null, null, 1, null, null, null}
            };

            var result = controller.Solution(validSudoku);

            AssertSudokuAreEqual(new int[][]
            {
                new int[] {8, 5, 4, 2, 1, 9, 7, 6, 3},
                new int[] {3, 9, 7, 8, 6, 5, 4, 2, 1},
                new int[] {2, 6, 1, 4, 7, 3, 9, 8, 5},
                new int[] {7, 8, 5, 1, 2, 6, 3, 9, 4},
                new int[] {6, 4, 9, 5, 3, 8, 1, 7, 2},
                new int[] {1, 3, 2, 9, 4, 7, 8, 5, 6},
                new int[] {9, 2, 6, 3, 8, 4, 5, 1, 7},
                new int[] {5, 1, 3, 7, 9, 2, 6, 4, 8},
                new int[] {4, 7, 8, 6, 5, 1, 2, 3, 9}
            }, result);
        }

        private void AssertSudokuAreEqual(int[][] expected, int[][] actual)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.AreEqual(expected[i][j], actual[i][j], String.Format("Entry [{0},{1}] is unexpected", i, j));
                }
            }
        }

        [Test]
        public void PostBlankSudokuToSolve()
        {
            var emptySudoku = new int?[][]
            {
                new int?[] {}
            };

            var result = controller.Solution(emptySudoku);

            AssertSudokuAreEqual(new int[][]
            {
                new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
                new int[] {4, 5, 6, 7, 8, 9, 1, 2, 3},
                new int[] {7, 8, 9, 1, 2, 3, 4, 5, 6},
                new int[] {2, 1, 4, 3, 6, 5, 8, 9, 7},
                new int[] {3, 6, 5, 8, 9, 7, 2, 1, 4},
                new int[] {8, 9, 7, 2, 1, 4, 3, 6, 5},
                new int[] {5, 3, 1, 6, 4, 2, 9, 7, 8},
                new int[] {6, 4, 2, 9, 7, 8, 5, 3, 1},
                new int[] {9, 7, 8, 5, 3, 1, 6, 4, 2}
            }, result);
        }

        [Test]
        [ExpectedException(typeof (InvalidSudokuException))]
        public void PostInValidSudokuWithConflict()
        {
            // From http://www.sudoku.ws/hard-1.htm
            var inValidSudoku = new int?[][]
            {
                new int?[] {null, 2, 2}
            };

            controller.Solution(inValidSudoku);
        }

        [Test]
        [ExpectedException(typeof(InvalidSudokuException))]
        public void PostInValidSudokuWithEntryTooLarge()
        {
            // From http://www.sudoku.ws/hard-1.htm
            var inValidSudoku = new int?[][]
            {
                new int?[] {null, 2, 15}
            };

            controller.Solution(inValidSudoku);
        }

        [Test]
        [ExpectedException(typeof(InvalidSudokuException))]
        public void PostInValidSudokuWithEntryNegative()
        {
            // From http://www.sudoku.ws/hard-1.htm
            var inValidSudoku = new int?[][]
            {
                new int?[] {null, 2, -9}
            };

            controller.Solution(inValidSudoku);
        }
    }
}