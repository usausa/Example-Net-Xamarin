namespace Example.Server.Api
{
    using AutoMapper;

    using Example.Server.Api.Models;
    using Example.Server.Models.Entity;
    using Example.Server.Models.View;

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
