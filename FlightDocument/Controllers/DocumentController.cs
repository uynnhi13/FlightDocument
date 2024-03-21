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
        public async Task<IActionResult> CreateDocument([FromForm] DocumentDTO documentDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _documentService.AddDocumentAsync(documentDTO);
                return Ok("thêm thành công");
            }
            else { return BadRequest(); }
        }

        [HttpGet]
        [Route("DownloadfileDocument")]
        public async Task<IActionResult> DownLoadFile(int id)
        {
            ResponseDto? response=await _documentService.DownLoadFile(id);
            if (!response.IsSuccess)
            {
                // Nếu không thành công, trả về mã lỗi 500 với thông báo lỗi
                return StatusCode(500, response.Message);
            }

            // Kiểm tra xem kết quả có là một tệp không
            if (response.Result is byte[] fileBytes && fileBytes.Length > 0)
            {
                // Trả về tệp PDF với MIME type phù hợp và tên tệp
                return File(fileBytes, "application/pdf", "Tên tệp.pdf");
            }
            else
            {
                // Nếu không tìm thấy tệp, trả về mã lỗi 404 với thông báo
                return NotFound("Không tìm thấy tệp");
            }
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
