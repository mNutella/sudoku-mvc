using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Sudoku.Mvc.Api.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Action<TController>(this IUrlHelper urlHelper, Expression<Action<TController>> expression, object parameters)
        where TController : Controller
        {
            var controller = expression.Type.GenericTypeArguments[0].Name.Replace("Controller", string.Empty);
            var member = expression.Body as MethodCallExpression;
            var actionName = member.Method.Name;

            return urlHelper.Action(actionName, controller, parameters, urlHelper.ActionContext.HttpContext.Request.IsHttps ? "https" : "http");
        }

        public static string Action<TController>(this IUrlHelper urlHelper, Expression<Action<TController>> expression)
            where TController : Controller
        {
            return urlHelper.Action(expression, null);
        }
    }
}
