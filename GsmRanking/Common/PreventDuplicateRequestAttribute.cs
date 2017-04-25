using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GsmRanking.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDuplicateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Form.ContainsKey("__RequestVerificationToken"))
                return;

            var currentToken = context.HttpContext.Request.Form["__RequestVerificationToken"].ToString();
            var lastToken = context.HttpContext.Session.GetString("LastProcessedToken");

            if (lastToken == currentToken)
            {
                context.ModelState.AddModelError(string.Empty, "Looks like you accidentally submitted the same form twice.");
                return;
            }

            context.HttpContext.Session.SetString("LastProcessedToken", currentToken);
        }
    }
}
