namespace Example.Server.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Example.Server.Api.Models;
    using Example.Server.Models.Entity;
    using Example.Server.Services;

    using Microsoft.AspNetCore.Mvc;

    [Area("api")]
    [Route("[area]/[controller]/[action]")]
    public class StorageController : Controller
    {
        private IMapper Mapper { get; }

        private StorageService StorageService { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="storageService"></param>
        public StorageController(IMapper mapper, StorageService storageService)
        {
            Mapper = mapper;
            StorageService = storageService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await StorageService.QueryStorageViewList();

            return Ok(new StorageResponse { Entries = list.Select(Mapper.Map<StorageResponseEntry>).ToArray() });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var entity = await StorageService.QueryStorageView(id);
            if (entity == null)
            {
                NotFound();
            }

            var list = await StorageService.QueryStorageDetailList(id);

            var response = Mapper.Map<StorageDetailsResponse>(entity);
            response.Entries = list.Select(Mapper.Map<StorageDetailsResponseEntry>).ToArray();
            return Ok(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Details([FromBody] StorageDetailsRequest request)
        {
            var list = request.Entries
                .Select(x =>
                {
                    var entity = Mapper.Map<StorageDetailEntity>(x);
                    entity.StorageNo = request.StorageNo;
                    return entity;
                });

            if (!await StorageService.UpdateStorage(request.Renew, request.StorageNo, request.UserId, list))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
