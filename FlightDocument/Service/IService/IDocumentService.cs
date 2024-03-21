using FlightDocument.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocument.Service.IService
{
    public interface IDocumentService
    {
        //Task<ResponseDto?> GetDocumentByIDAsync(int id);
        Task<ResponseDto?> GetAllDocumentAsync();
        //Task<ResponseDto?> UpdateDocumentAsync(DocumentDTO documentDTO);
        Task<ResponseDto?> AddDocumentAsync([FromForm] DocumentDTO documentDTO);
        Task<ResponseDto?> DownLoadFile(int id);
        Task<ResponseDto?> DeleteDocumentAsync(int id);
    }
}
