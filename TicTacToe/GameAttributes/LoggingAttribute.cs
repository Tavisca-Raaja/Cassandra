using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DataAccess;

namespace TicTacToe.GameAttributes
{
    public class LoggingAttribute : ResultFilterAttribute, IActionFilter,IExceptionFilter
    {
        Logger Instance = new Logger();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception==null)
            Instance.LogException(context.RouteData.Values["action"].ToString(),"success","None","Successfully executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Instance.LogException(context.RouteData.Values["action"].ToString(), "started", "None", "Process started");
        }

        public void OnException(ExceptionContext context)
        {
            Instance.LogException(context.RouteData.Values["action"].ToString(), "Exception", context.Exception.Message.ToString(), "Error");
        }
    }
}
