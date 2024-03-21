using DocumentServices.Models;
using DocumentServices.Models.DTO;
using DocumentServices.Service.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DocumentServices.Service
{
    public class FlightService : IFlightService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FlightService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<FlightDto>> GetAllFlight()
        {
            var client=_httpClientFactory.CreateClient("Flight");
            var response = await client.GetAsync($"api/flight/GetAllAirport");
            var apiContent=await response.Content.ReadAsStringAsync();
            var resp=JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<FlightDto>>(Convert.ToString(resp.Result));
            }
            return new List<FlightDto>();
        }

        public async Task<bool> FlightExists(int flightId)
        {
            var client = _httpClientFactory.CreateClient("Flight");
            var response = await client.GetAsync($"api/flight/flightexists/{flightId}");
            var apiContent = await response.Content.ReadAsStringAsync();

            // Chuyển đổi giá trị của apiContent thành kiểu bool và trả về
            return bool.Parse(apiContent);
        }
    }
}
