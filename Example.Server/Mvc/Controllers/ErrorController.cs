namespace Example.Server.Mvc.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
