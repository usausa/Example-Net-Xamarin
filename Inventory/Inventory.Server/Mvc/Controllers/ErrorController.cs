namespace Inventory.Server.Mvc.Controllers
{
    using System.Diagnostics;

    using Inventory.Server.Mvc.Models;

    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
