using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Sudoku.Mvc.Web.Common
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected IActionResult StatusCode(HttpStatusCode statusCode, object value = null)
        {
            return StatusCode((int)statusCode, value);
        }
    }
}
