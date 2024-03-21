using AuthAPIService.Models;
using AuthAPIService.Models.DTO;
using AuthAPIService.Service.IService;
using AutoMapper;
using DocumentServices.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthAPIService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        private IMapper _mapper;

        public AuthAPIController(IAuthService authService, AppDBContext db, IMapper mapper)
        {
            _authService = authService;
            _responseDto = new();
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("roles")]
        [SwaggerOperation(Summary = "Get all roles", Description = "Retrieve a list of all available roles.")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _authService.GetRoles();
            // Phương thức này lấy danh sách các vai trò từ cơ sở dữ liệu
            return Ok(roles);
        }

        [HttpPost("CreateRoles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty");
            }

            bool roleCreated = await _authService.CreateRole(roleName);
            if (roleCreated)
            {
                return Ok($"Role '{roleName}' created successfully");
            }
            else
            {
                return BadRequest($"Failed to create role '{roleName}'");
            }
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
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
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

        [HttpGet("getalluser")]
        public async Task<ResponseDto> GetAllUser()
        {
            try
            {
                IEnumerable<ApplicationUser> userlst = await Task.Run(() => _db.ApplicationUsers.ToList());

                _responseDto.Result = _mapper.Map<IEnumerable<UserDto>>(userlst);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
