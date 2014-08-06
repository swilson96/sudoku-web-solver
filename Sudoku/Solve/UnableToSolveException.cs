using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Sudoku
{
    public class UnableToSolveException : HttpResponseException
    {
        public UnableToSolveException() : base(HttpStatusCode.NotImplemented)
        {
        }
    }
}