using System;
using NUnit.Framework;

namespace Sudoku.Tests.Solve
{
    /// <summary>
    /// Some hard Sudoku from http://www.sudoku.ws/
    /// </summary>
    [TestFixture]
    public class SolverValidTest
    {
        private Solver solver;

        [TestFixtureSetUp]
        public void setup()
        {
            solver = new Solver();
        }

        [Test]
        public void Hard1IsSolvedCorrectly()
        {
            TestInputSudokuProducesGivenSolution(new int?[,]
            {
                {0, 0, 0, 2, 0, 0, 0, 6, 3},
                {3, 0, 0, 0, 0, 5, 4, 0, 1},
                {0, 0, 1, 0, 0, 3, 9, 8, 0},
                {0, 0, 0, 0, 0, 0, 0, 9, 0},
                {0, 0, 0, 5, 3, 8, 0, 0, 0},
                {0, 3, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 6, 3, 0, 0, 5, 0, 0},
                {5, 0, 3, 7, 0, 0, 0, 0, 8},
                {4, 7, 0, 0, 0, 1, 0, 0, 0}
            }, new [,]
            {
                {8, 5, 4, 2, 1, 9, 7, 6, 3},
                {3, 9, 7, 8, 6, 5, 4, 2, 1},
                {2, 6, 1, 4, 7, 3, 9, 8, 5},
                {7, 8, 5, 1, 2, 6, 3, 9, 4},
                {6, 4, 9, 5, 3, 8, 1, 7, 2},
                {1, 3, 2, 9, 4, 7, 8, 5, 6},
                {9, 2, 6, 3, 8, 4, 5, 1, 7},
                {5, 1, 3, 7, 9, 2, 6, 4, 8},
                {4, 7, 8, 6, 5, 1, 2, 3, 9}
            });
        }

        [Test]
        public void Hard2IsSolvedCorrectly()
        {
            TestInputSudokuProducesGivenSolution(new int?[,]
            {
                {0, 1, 0, 0, 0, 4, 0, 0, 0},
                {0, 0, 6, 8, 0, 5, 0, 0, 1},
                {5, 0, 3, 7, 0, 1, 9, 0, 0},
                {8, 0, 4, 0, 0, 7, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 3, 0, 0, 6, 0, 9},
                {0, 0, 1, 5, 0, 8, 2, 0, 4},
                {6, 0, 0, 4, 0, 3, 1, 0, 0},
                {0, 0, 0, 2, 0, 0, 0, 5, 0}
            }, new [,]
            {
                {2,1,8,9,6,4,5,3,7},
                {9,7,6,8,3,5,4,2,1},
                {5,4,3,7,2,1,9,8,6},
                {8,9,4,6,5,7,3,1,2},
                {3,6,2,1,4,9,8,7,5},
                {1,5,7,3,8,2,6,4,9},
                {7,3,1,5,9,8,2,6,4},
                {6,2,5,4,7,3,1,9,8},
                {4,8,9,2,1,6,7,5,3}
            });
        }

        [Test]
        public void Extreme1IsSolvedCorrectly()
        {
            TestInputSudokuProducesGivenSolution(new int?[,]
            {
                {0, 0, 9, 7, 4, 8, 0, 0, 0},
                {7, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 0, 1, 0, 9, 0, 0, 0},
                {0, 0, 7, 0, 0, 0, 2, 4, 0},
                {0, 6, 4, 0, 1, 0, 5, 9, 0},
                {0, 9, 8, 0, 0, 0, 3, 0, 0},
                {0, 0, 0, 8, 0, 3, 0, 2, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 6},
                {0, 0, 0, 2, 7, 5, 9, 0, 0}
            }, new [,]
            {
                {5,1,9,7,4,8,6,3,2},
                {7,8,3,6,5,2,4,1,9},
                {4,2,6,1,3,9,8,7,5},
                {3,5,7,9,8,6,2,4,1},
                {2,6,4,3,1,7,5,9,8},
                {1,9,8,5,2,4,3,6,7},
                {9,7,5,8,6,3,1,2,4},
                {8,3,2,4,9,1,7,5,6},
                {6,4,1,2,7,5,9,8,3}
            });
        }

        [Test]
        public void Extreme2IsSolvedCorrectly()
        {
            TestInputSudokuProducesGivenSolution(new int?[,]
            {
                {0, 0, 0, 3, 0, 8, 0, 7, 0},
                {3, 0, 0, 7, 1, 0, 0, 0, 4},
                {6, 0, 0, 0, 4, 0, 0, 0, 0},
                {1, 0, 0, 0, 0, 0, 6, 3, 0},
                {2, 0, 6, 0, 0, 0, 5, 0, 8},
                {0, 5, 3, 0, 0, 0, 0, 0, 7},
                {0, 0, 0, 0, 8, 0, 0, 0, 1},
                {7, 0, 0, 0, 6, 4, 0, 0, 5},
                {0, 1, 0, 2, 0, 7, 0, 0, 0}
            }, new [,]
            {
                {5,4,1,3,9,8,2,7,6},
                {3,2,9,7,1,6,8,5,4},
                {6,8,7,5,4,2,3,1,9},
                {1,9,4,8,7,5,6,3,2},
                {2,7,6,4,3,1,5,9,8},
                {8,5,3,6,2,9,1,4,7},
                {4,6,5,9,8,3,7,2,1},
                {7,3,2,1,6,4,9,8,5},
                {9,1,8,2,5,7,4,6,3}
            });
        }

        private void TestInputSudokuProducesGivenSolution(int?[,] sudoku, int[,] solution)
        {
            AssertSudokuAreEqual(solution, solver.Solve(sudoku));
        }

        private void AssertSudokuAreEqual(int[,] expected, int[,] actual)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.AreEqual(expected[i,j], actual[i,j], String.Format("Entry [{0},{1}] is unexpected", i, j));
                }
            }
        }
    }
}
