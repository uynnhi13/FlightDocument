using DocumentServices.Models.DTO;
using DocumentServices.Service.IService;
using Newtonsoft.Json;

namespace DocumentServices.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<string>> GetRoles()
        {
            var client=_httpClientFactory.CreateClient("Auth");
            var response = await client.GetAsync($"api/auth/roles");
            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<string>>(apiContent);
                return roles;
            }
            else
            {
                // Xử lý khi response không thành công (ví dụ: throw exception, trả về null, hoặc giá trị mặc định khác)
                // Ở đây tôi sẽ throw exception để xử lý ở nơi gọi phương thức này
                throw new HttpRequestException($"Error getting roles: {response.ReasonPhrase}");
            }
        }
    }
}
