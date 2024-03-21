using System.ComponentModel.DataAnnotations;

namespace FlightService.Models
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }
        public string Name { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
