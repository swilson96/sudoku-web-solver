using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Sudoku.Filters;
using Sudoku.Solve;

namespace Sudoku.Controllers
{
    [ValidateHttpAntiForgeryToken]
    public class SolutionController : ApiController
    {
        private SolverWrapper solver = new SolverWrapper();

        // POST api/Solution
        [HttpPost]
        public int[][] Solution(int?[][] grid)
        {
            // TODO: return sensible error if invalid sudoku, or if can't solve
            return solver.Solve(grid);
        }
    }
}