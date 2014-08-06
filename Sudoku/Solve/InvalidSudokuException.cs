using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Sudoku
{
    public class InvalidSudokuException : HttpResponseException
    {
        public InvalidSudokuException() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}