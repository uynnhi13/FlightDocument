using FlightDocument.Models;
using FlightDocument.Service.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static FlightDocument.Utility.SD;

namespace FlightDocument.Service
{
    public class BaseService : IBaseService
    {
        //Đối tượng factory để tạo HttpClient
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                // Tạo một HttpClient từ IHttpClientFactory, sử dụng tên "FlightDocAPI"
                HttpClient client = _httpClientFactory.CreateClient("FlightDocAPI");
                HttpRequestMessage message = new();

                // Thêm header Accept vào request, chỉ định kiểu dữ liệu nhận được là JSON
                message.Headers.Add("Accept", "application/json");

                //token
                message.RequestUri = new Uri(requestDto.Url);
                // Nếu có dữ liệu được gửi đi
                if (requestDto.Data != null)
                {
                    // Convert dữ liệu thành chuỗi JSON và gán vào message.Content
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        System.Text.Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;
                // Xác định loại phương thức HTTP dựa trên ApiType trong requestDto
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Gửi request đến API bằng phương thức tương ứng và nhận phản hồi
                apiResponse = await client.SendAsync(message);

                // Xử lý phản hồi từ API
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Acess Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }

        }
    }
}
