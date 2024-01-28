using FlightDocument.Models;
using FlightDocument.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FlightDocument.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDocument()
        {
            List<DocumentDTO>? list = new();
            ResponseDto? response = await _documentService.GetAllDocumentAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DocumentDTO>>(Convert.ToString(response.Result));
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDocument(DocumentDTO documentDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _documentService.AddDocumentAsync(documentDTO);
                return Ok("thêm thành công");
            }
            else { return BadRequest(); }
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteDocument(int id)
        {
            ResponseDto? response = await _documentService.DeleteDocumentAsync(id);

            if (response != null && response.IsSuccess)
            {
                DocumentDTO? model=JsonConvert.DeserializeObject<DocumentDTO>(Convert.ToString(response.Result));
                return Ok("Xóa Tài liệu có mã id " + id + " thành công");
            }
            else
            {
                return NotFound($"Không tồn tại mã id {id} cần xóa");
            }
        }

    }
}
