namespace Example.Server.Mvc.Controllers
{
    using System.Threading.Tasks;

    using Example.Server.Services;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///
    /// </summary>
    public class AdminController : Controller
    {
        private StorageService StorageService { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="storageService"></param>
        public AdminController(StorageService storageService)
        {
            StorageService = storageService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Reset()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Reset")]
        public async Task<IActionResult> ResetExecute()
        {
            await StorageService.DeleteStorageAll();

            return RedirectToAction("Index", "Storage");
        }
    }
}
