namespace Example.Server.Api.Controllers
{
    using System.IO;

    using Example.Server.Settings;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    ///
    /// </summary>
    [Area("api")]
    [Route("[area]/[controller]/[action]")]
    public class MasterController : Controller
    {
        private FileSettings FileSettings { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileSettings"></param>
        public MasterController(IOptions<FileSettings> fileSettings)
        {
            FileSettings = fileSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult Item()
        {
            var file = new FileInfo(FileSettings.ItemMaster);
            return PhysicalFile(file.FullName, "application/x-msdownload");
        }
    }
}
