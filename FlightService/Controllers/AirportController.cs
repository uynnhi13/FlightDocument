using AutoMapper;
using FlightService.Data;
using FlightService.Models;
using FlightService.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("api/airport")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public AirportController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllAirport")]
        public async Task<ResponseDto> GetAllAirport()
        {
            try
            {
                IEnumerable<Airport> objList = await Task.Run(() => _db.Airports.ToList());
                _response.Result = _mapper.Map<IEnumerable<AirportDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("AddAirport")]
        public async Task<ResponseDto> Post([FromBody] AirportDto airportDto)
        {
            try
            {
                //Tạo một đối tượng Airport từ AirportDto
                Airport airport = _mapper.Map<Airport>(airportDto);

                //Lưu document vào database
                _db.Airports.Add(airport);
                await _db.SaveChangesAsync();

                _response.Result = airportDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;

                if(ex.InnerException != null)
                {
                    _response.Message += " Inner Exception: " + ex.InnerException.Message;
                }
            }
            return _response;
        }
    }
}
