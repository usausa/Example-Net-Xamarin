namespace Inventory.Server.Api
{
    using AutoMapper;

    using Inventory.Server.Api.Models;
    using Inventory.Server.Models.Entity;
    using Inventory.Server.Models.View;

    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<StorageView, StorageResponseEntry>();

            CreateMap<StorageView, StorageDetailsResponse>();
            CreateMap<StorageDetailEntity, StorageDetailsResponseEntry>();

            CreateMap<StorageDetailsRequestEntry, StorageDetailEntity>();
        }
    }
}
