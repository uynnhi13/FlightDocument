using FlightDocument.Service.IService;
using FlightDocument.Utility;
using Newtonsoft.Json.Linq;

namespace FlightDocument.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // Phương thức để xóa cookie token
        public void ClearToken()
        {
            // Nó kiểm tra HttpContextAccessor nếu tồn tại, nếu có, thì nó sẽ xóa
            // cookie token bằng cách gọi phương thức Delete của đối tượng Response.
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        // Phương thức để lấy token từ cookie
        // Nó trả về chuỗi token nếu có, nếu không, trả về null.
        public string? GetToken()
        {
            // Khởi tạo biến token và gán giá trị mặc định là null.
            string? token = null;

            // Kiểm tra nếu có HttpContextAccessor và có thể lấy được token từ cookie,
            // thì gán giá trị của token là giá trị của token được lấy từ cookie.
            bool? hasToken=_contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
           
            return hasToken is true? token : null;
        }

        // Phương thức để đặt cookie token
        public void SetToken(string token)
        {
             _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
          
        }
    }
}
