using AuthAPIService.Models.DTO;
using AuthAPIService.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPIService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            //Gọi phương thức Login từ IAuthService để thực hiện việc đăng nhập
            var loginResponse = await _authService.Login(model);

            // Kiểm tra xem có thông tin người dùng trả về không
            if (loginResponse.User==null)
            {
                // Nếu không tìm thấy người dùng hoặc thông tin đăng nhập không chính xác, 
                   // IsSuccess là false và gán thông báo lỗi vào Message
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Email or Password is incorrect";
                return BadRequest(_responseDto);
            }

            // Nếu đăng nhập thành công, gán kết quả đăng nhập vào Result và trả về một phản hồi thành công
            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            // Gọi phương thức AssignRole từ AuthService để thực hiện gán role cho user.
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());

            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
