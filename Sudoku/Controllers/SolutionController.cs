using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sudoku.Filters;

namespace Sudoku.Controllers
{
    [ValidateHttpAntiForgeryToken]
    public class SolutionController : ApiController
    {
        // POST api/Solution
        [HttpPost]
        public HttpResponseMessage Solution()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            // TODO solve 'em

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}