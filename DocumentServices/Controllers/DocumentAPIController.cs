using AutoMapper;
using DocumentServices.Data;
using DocumentServices.Models;
using DocumentServices.Models.DTO;
using DocumentServices.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentServices.Controllers
{
    [Route("api/document")]
    [ApiController]
    //[Authorize]
    public class DocumentAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        private IFlightService _flightService;

        public DocumentAPIController(AppDBContext db,IMapper mapper,
            IFlightService flightService)
        {
            _db = db; 
            _mapper = mapper;
            _response = new ResponseDto();
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Document> objList = await Task.Run(() => _db.Documents.ToList());

                // Ánh xạ danh sách các đối tượng Document thành một danh sách DTO và gán kết quả cho ResponseDto
                _response.Result = _mapper.Map<IEnumerable<DocumentDetailDTO>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize()]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Document document = await _db.Documents.FindAsync(id);
                _response.Result = _mapper.Map<DocumentDTO>(document);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("getbyflightid/{flightId:int}")]
        public async Task<ResponseDto> GetAllDocumentByFlightID(int flightId)
        {
            try
            {
                //Lọc danh sách các tài liệu có flightId tương ứng
                List<Document> documents = await _db.Documents.Where(d => d.FlightId == flightId).ToListAsync();
                if (documents.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No documents found for the specified flightId.";
                }
                else
                {
                    _response.Result=documents.Select(d=>_mapper.Map<DocumentDetailDTO>(d)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //Thêm Document

        [HttpPost]
        public async Task<ResponseDto> Post([FromForm] DocumentDTO documentDTO)
        {
            try
            {
                var check = await _flightService.FlightExists((int)documentDTO.FlightId);
                if (check)
                {
                    // Tạo một đối tượng Document từ DocumentDTO
                    Document obj = _mapper.Map<Document>(documentDTO);
                    // tạo phiên bản cho tài liệu
                    List<Document> existingDocument = await _db.Documents.Where(d => d.FlightId == obj.FlightId).ToListAsync();
                    if (existingDocument == null)
                    {
                        obj.Version = "1.0";
                    }
                    else
                    {
                        int majorVersion = 1;
                        int minorVersion = existingDocument.Count;
                        obj.Version = $"{majorVersion}.{minorVersion}";
                    }
                    // Lưu trữ file PDF vào thư mục và lấy đường dẫn
                    string filePath = await SaveFile(documentDTO.filePDF);
                    obj.filePath = filePath; // Lưu đường dẫn của file PDF
                                             // Lấy thông tin người dùng đang đăng nhập
                    var loggedInUser = HttpContext.User.Identity.Name;
                    obj.creator = loggedInUser;
                    // Lưu document vào database
                    _db.Documents.Add(obj);
                    await _db.SaveChangesAsync();

                    DocumentDetailDTO detailDTO = _mapper.Map<DocumentDetailDTO>(obj);
                    _response.Result = detailDTO;
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Message = "không tồn tại chuyến bay";
                }
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                if (ex.InnerException != null)
                {
                    _response.Message += " Inner Exception: " + ex.InnerException.Message;
                }
            }
            return _response;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null && file.Length > 0)
            {
                // Tạo tên file duy nhất
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                // Đường dẫn tuyệt đối đến thư mục lưu trữ file PDF
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", uniqueFileName);

                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(int documentId)
        {
            try
            {
                // Lấy thông tin document từ database
                Document document = await _db.Documents.FindAsync(documentId);

                if (document == null)
                {
                    return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy document
                }

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", document.filePath);

                // Đọc dữ liệu của tệp từ đường dẫn và tên tệp đã lưu
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filepath);

                // Trả về tệp PDF với MIME type phù hợp và tên tệp
                return File(fileBytes, "application/pdf", document.filePath);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về một trang lỗi hoặc mã lỗi phù hợp
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


        //update document
        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] DocumentDTO documentDTO)
        {
            try
            {
                Document obj = _mapper.Map<Document>(documentDTO);
                _db.Documents.Update(obj);
                await _db.SaveChangesAsync();

                _response.Result = _mapper.Map<DocumentDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //delete document
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Document obj = await _db.Documents.FindAsync(id);
                _db.Documents.Remove(obj);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("getalllights")]
        public async Task<ResponseDto> GetAllLight()
        {
            _response.Result=await _flightService.GetAllFlight();
            return _response;
        }
    }
}
