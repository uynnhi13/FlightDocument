using FlightDocument.Models;
using FlightDocument.Service.IService;
using FlightDocument.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocument.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IBaseService _baseService;

        public DocumentService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddDocumentAsync([FromForm] DocumentDTO documentDTO)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data= documentDTO,
                Url = SD.DocumentAPIBase + "/api/document"
            });
        }
        public async Task<ResponseDto?> DeleteDocumentAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.DocumentAPIBase + "/api/document/"+id
            });
        }

        public async Task<ResponseDto?> DownLoadFile(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType=SD.ApiType.GET,
                Url=SD.DocumentAPIBase+"/api/document/"+id
            });
        }

        public async Task<ResponseDto?> GetAllDocumentAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DocumentAPIBase + "/api/document"
            });
        }

        //public async Task<ResponseDto?> GetDocumentByIDAsync(int id)
        //{
        //    return await _baseService.SendAsync(new RequestDto()
        //    {
        //        ApiType= SD.ApiType.GET,
        //        Url=SD.DocumentAPIBase+"/api/document/"+id
        //    });
        //}

        //public async Task<ResponseDto?> UpdateDocumentAsync(DocumentDTO documentDTO)
        //{
        //    return await _baseService.SendAsync(new RequestDto()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = documentDTO,
        //        Url = SD.DocumentAPIBase + "/api/document"
        //    });
        //}
    }
}
