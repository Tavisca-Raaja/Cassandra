using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DataAccess;
using TicTacToe.Exceptions;

namespace TicTacToe.Authorize
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext context)
        {
           //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["player1AccessKey"].ToString();
            var apikey2 = context.HttpContext.Request.Headers["player2AccessKey"].ToString();
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apikey2))
            {
               throw new UnauthorizedAccessException("Invalid Api Key");
            }
            else
            {
                UserRegistration user = new UserRegistration();
                if (!user.ValidateAccessToken(apiKey) || !user.ValidateAccessToken(apikey2))
                {
                    throw new UnauthorizedAccessException("Invalid Api Key");
                }
            }
        }

  
    }
}
