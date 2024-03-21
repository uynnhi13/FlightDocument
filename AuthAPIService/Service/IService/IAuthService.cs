using AuthAPIService.Models.DTO;

namespace AuthAPIService.Service.IService
{
    public interface IAuthService
    {
        Task<List<string>> GetRoles();
        Task<bool> CreateRole(string roleName);
        Task<string> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        
    }
}
