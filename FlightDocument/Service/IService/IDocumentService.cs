using FlightDocument.Models;

namespace FlightDocument.Service.IService
{
    public interface IDocumentService
    {
        Task<ResponseDto?> GetDocumentByIDAsync(int id);
        Task<ResponseDto?> GetAllDocumentAsync();
        Task<ResponseDto?> UpdateDocumentAsync(DocumentDTO documentDTO);
        Task<ResponseDto?> AddDocumentAsync(DocumentDTO documentDTO);
        Task<ResponseDto?> DeleteDocumentAsync(int id);
    }
}
