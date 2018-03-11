using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Sudoku.Mvc.Api.Controllers.Mvc.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
