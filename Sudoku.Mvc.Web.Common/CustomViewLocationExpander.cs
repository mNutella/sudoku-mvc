using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Sudoku.Mvc.Web.Common
{
    public class CustomViewLocationExpander: IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // do nothing - not required
        }
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            yield return "/Controllers/Mvc/{1}/{0}/{0}.cshtml";
            yield return "/Shared/Views/{0}.cshtml";

            if (!context.IsMainPage && context.ActionContext.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                yield return "/Controllers/Mvc/{1}/" + $"{descriptor.ActionName}" +
                             "/{0}.cshtml";
            }
        }
    }
}
