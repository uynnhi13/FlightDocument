using AuthAPIService.Models;
using AuthAPIService.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthAPIService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUser, UserDto>();
            });
            return mappingConfig;
        }
    }
}
