namespace Inventory.Server.Mvc.Controllers
{
    using System.Threading.Tasks;

    using Inventory.Server.Services;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///
    /// </summary>
    public class StorageController : Controller
    {
        private StorageService StorageService { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="storageService"></param>
        public StorageController(StorageService storageService)
        {
            StorageService = storageService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await StorageService.QueryStorageViewList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            var storage = await StorageService.QueryStorageView(id);
            if (storage == null)
            {
                return NotFound();
            }

            ViewBag.Storage = storage;

            return View(await StorageService.QueryStorageDetailList(id));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await StorageService.DeleteStorage(id);

            return RedirectToAction("Index");
        }
    }
}
