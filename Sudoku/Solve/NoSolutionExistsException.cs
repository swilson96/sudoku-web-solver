﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Sudoku.Solve
{
    public class NoSolutionExistsException : HttpResponseException
    {
        public NoSolutionExistsException() : base(HttpStatusCode.Conflict)
        {
        }
    }
}