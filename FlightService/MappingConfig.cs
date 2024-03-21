using AutoMapper;
using FlightService.Models;
using FlightService.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightServices
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<FlightDto, Flight>();
                config.CreateMap<Flight, FlightDto>();
                config.CreateMap<AirportDto, Airport>();
                config.CreateMap<Airport, AirportDto>();
            });
            return mappingConfig;
        }
    }
}
