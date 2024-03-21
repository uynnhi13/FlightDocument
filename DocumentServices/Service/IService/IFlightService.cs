using DocumentServices.Models;

namespace DocumentServices.Service.IService
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlight();
        Task<bool> FlightExists(int flightId);
    }
}
