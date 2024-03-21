namespace FlightService.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string FlightNo { get; set; }
        public int DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; }
        public int ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


    }


}
