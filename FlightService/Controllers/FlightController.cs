using AutoMapper;
using FlightService.Models;
using FlightService.Data;
using FlightService.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public FlightController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllAirport")]
        public async Task<ResponseDto> GetAllFlight()
        {
            try
            {
                IEnumerable<Flight> flightList = await Task.Run(() => _db.Flights.ToList());
                _response.Result = _mapper.Map<IEnumerable<FlightDto>>(flightList);
            }
            catch (Exception ex)
            {
                _response.Result = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("AddFlight")]
        public async Task<ResponseDto> Post([FromBody] FlightDto flightDto)
        {
            try
            {
                //Tạo một đối tượng Flight từ FlightDto
                Flight flight = _mapper.Map<Flight>(flightDto);

                //Lưu Flight vào database
                _db.Flights.Add(flight);
                await _db.SaveChangesAsync();

                _response.Result = flightDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                if (ex.InnerException != null)
                {
                    _response.Message += " Inner Exception: " + ex.InnerException.Message;
                }
            }
            return _response;
        }

        [HttpGet("GetByFlightNo/{FlightNo}")]
        public async Task<ResponseDto> GetByFlightNo(string FlightNo)
        {
            try
            {
                Flight flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightNo.ToUpper() == FlightNo.ToUpper());
                if (flight != null)
                {
                    _response.Result = _mapper.Map<FlightDto>(flight);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Flight not found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("flightexists/{flightId}")]
        public async Task<bool> FlightExists(int flightId)
        {
            return _db.Flights.Any(f => f.FlightId == flightId);
        }

    }
}
 