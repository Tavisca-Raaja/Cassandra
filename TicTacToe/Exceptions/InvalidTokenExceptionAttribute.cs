using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Exceptions
{
    public class InvalidTokenAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new JsonResult("Invalid Access Key");
            }

           
        }
    }
}
