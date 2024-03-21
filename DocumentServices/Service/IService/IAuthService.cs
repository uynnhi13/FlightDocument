using DocumentServices.Models.DTO;

namespace DocumentServices.Service.IService
{
    public interface IAuthService
    {
        Task<List<string>> GetRoles();
    }
}
