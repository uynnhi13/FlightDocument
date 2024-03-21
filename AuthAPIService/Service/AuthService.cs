using AuthAPIService.Models;
using AuthAPIService.Models.DTO;
using AuthAPIService.Service.IService;
using DocumentServices.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

namespace AuthAPIService.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDBContext db, IJwtTokenGenerator jwtTokenGenerator ,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            return false;
        }


        public async Task<List<string>> GetRoles()
        {
            // Lấy danh sách các vai trò từ cơ sở dữ liệu
            var roles = await Task.Run(() => _roleManager.Roles.Select(r => r.Name).ToList());
            return roles;
        }


        //Phân role cho user
        public async Task<bool> AssignRole(string email, string roleName)
        {
            //Tìm user thông qua email
            var user = _db.ApplicationUsers.First(u => u.Email.ToLower() == email.ToLower());

            //Kiểm tra user có tồn tại không
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Tạo role mới nếu nó không tồn tại
                    return false;
                }

                //Gán role cho user
                await _userManager.AddToRoleAsync(user,roleName);
                return true; //trả về true nếu gán role thành công
            }
            return false; //trả về false nếu không tìm thấy role
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.First(u => u.Email.ToLower() == loginRequestDto.Email.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user==null || isValid==false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //If user was found, Generate JWT Token
            var roles= await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;


        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            if (!registerationRequestDto.Email.EndsWith("@alta.com"))
            {
                // Kiểm tra định dạng email: Kiểm tra xem email có kết thúc bằng "@alta.com"
                return "Gmail không đúng định dạng";
            }

            // Tạo một đối tượng người dùng ApplicationUser từ dữ liệu trong RegisterationRequestDto
            ApplicationUser user = new()
            {
                Name = registerationRequestDto.Name,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                PhoneNumber = registerationRequestDto.PhoneNumber,
                UserName = registerationRequestDto.UserName,
                RoleName = registerationRequestDto.Role.ToUpper()
            };

            try
            {
                // Tạo người dùng mới bằng cách gọi phương thức CreateAsync từ UserManager
                var result =await _userManager.CreateAsync(user, registerationRequestDto.Password);
                

                if (result.Succeeded)
                {
                    // Nếu tạo người dùng thành công, lấy thông tin người dùng từ cơ sở dữ liệu
                    var userToReturn = _db.ApplicationUsers.First(u => u.Email == registerationRequestDto.Email);
                    var AddRole = AssignRole(userToReturn.Email,registerationRequestDto.Role);


                    // Tạo một đối tượng UserDto từ thông tin người dùng
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        UserName = userToReturn.UserName,
                        RoleName = userToReturn.RoleName
                    };

                    // Trả về chuỗi rỗng để chỉ ra rằng quá trình đăng ký đã thành công
                    return "";
                }
                else
                {
                    // Trả về mô tả lỗi đầu tiên nếu quá trình đăng ký không thành công
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Đã Xảy ra lỗi";
        }
    }
}
