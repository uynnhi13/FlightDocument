using FlightDocument.Models;

namespace FlightDocument.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true);
    }
}
