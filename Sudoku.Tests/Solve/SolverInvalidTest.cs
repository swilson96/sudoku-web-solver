﻿using System;
using NUnit.Framework;
using Sudoku.Solve;

namespace Sudoku.Tests.Solve
{
    [TestFixture]
    public class SolverInvalidTest
    {
        private Solver solver;

        [TestFixtureSetUp]
        public void setup()
        {
            solver = new Solver();
        }

        [Test]
        [ExpectedException(typeof(NoSolutionExistsException))]
        public void HardButInvalidSudokuThrows()
        {
            solver.Solve(new int?[,]
            {
                {0, 0, 0, 2, 0, 0, 0, 6, 3},
                {3, 0, 0, 0, 0, 5, 4, 0, 1},
                {0, 0, 1, 0, 0, 4, 9, 8, 0},
                {0, 0, 0, 0, 0, 0, 0, 9, 0},
                {0, 0, 0, 5, 3, 8, 0, 0, 0},
                {0, 3, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 6, 3, 0, 0, 5, 0, 0},
                {5, 0, 3, 7, 0, 0, 0, 0, 8},
                {4, 7, 0, 0, 0, 1, 0, 0, 0}
            });
        }
    }
}
