namespace FlightDocument.Models
{
    public class FlightDto
    {
        public int FlightId { get; set; }
        public string FlightNo { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
