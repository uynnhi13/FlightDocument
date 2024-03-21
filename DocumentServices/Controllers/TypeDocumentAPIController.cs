using AutoMapper;
using DocumentServices.Data;
using DocumentServices.Models;
using DocumentServices.Models.DTO;
using DocumentServices.Service;
using DocumentServices.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;

namespace DocumentServices.Controllers
{
    [Route("api/typedocument")]
    [ApiController]
    [Authorize]
    public class TypeDocumentAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        private object _httpContextAccessor;
        private readonly IAuthService _authService;

        public TypeDocumentAPIController(AppDBContext db, IMapper mapper, IAuthService authService)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ResponseDto> GetAllTypeDocument()
        {
            try
            {
                IEnumerable<TypeDocument> typelst=await Task.Run(() => _db.TypeDocuments.ToList());

                _response.Result = _mapper.Map<IEnumerable<TypeDocumentDTO>>(typelst);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> AddTypeDocument(string GroupName, string Note, string[] NhomTruyCap)
        {
            try
            {
                var check=_db.TypeDocuments.First(u=>u.GroupName.ToLower()==GroupName.ToLower());
                _response.IsSuccess = false;
                _response.Message = "Loại tài liệu này đã tồn tại";
            }
            catch (Exception ex)
            {
                TypeDocument obj = new TypeDocument();
                obj.GroupName = GroupName;
                obj.Note = Note;
                var loggedInUser = HttpContext.User.Identity.Name;
                obj.Creator = loggedInUser;

                _db.TypeDocuments.Add(obj);
                await _db.SaveChangesAsync();

                var check = _db.TypeDocuments.First(u => u.GroupName.ToLower() == GroupName.ToLower());

                List<string> Roles = await _authService.GetRoles();
                if (Roles != null)
                {
                    foreach (var roles in Roles)
                    {
                        PhanQuyenTaiLieu phanquyen = new PhanQuyenTaiLieu();
                        phanquyen.TypeDocumentID = check.TypeId;
                        phanquyen.NameRole = roles;
                        phanquyen.Claims = "No Permission";
                        _db.phanQuyenTaiLieus.Add(phanquyen);
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = obj;
            }
            return _response;
        }

        [HttpGet("getroles")]
        public async Task<List<string>> GetAllRoles()
        {
            List<string> Roles = await _authService.GetRoles();
            return Roles;
        }

        [HttpPut("phanquyen")]
        public async Task<ResponseDto> PhanQuyenTaiLieu([FromBody]PhanQuyenDto phanQuyenDto)
        {
            List<string> Roles = await _authService.GetRoles();
            if (Roles != null)
            {
                foreach (var roles in Roles)
                {
                    if (roles.ToUpper() == phanQuyenDto.NameRole.ToUpper())
                    {
                        if (string.IsNullOrEmpty(KiemTraClaims(phanQuyenDto.Claims)))
                        {
                            _response.Message = "Claim không hợp lệ, mời nhập lại";
                        }
                        else
                        {
                            PhanQuyenTaiLieu obj = _mapper.Map<PhanQuyenTaiLieu>(phanQuyenDto);
                            _db.phanQuyenTaiLieus.Update(obj);
                            await _db.SaveChangesAsync();

                            _response.Result = _mapper.Map<PhanQuyenDto>(obj);
                            _response.Message = "Cấp quyền thành công";
                        }
                    }
                    else
                    {
                        _response.Message = "Lỗi";
                    }
                }
            }
            return _response;
        }

        [HttpGet("kiem-tra-claims")]
        public String KiemTraClaims(string Claim)
        {
            // Chuyển đổi claim thành chữ in thường để so sánh không phân biệt hoa thường
            string lowercaseClaim = Claim.ToLower();

            switch (lowercaseClaim)
            {
                case "read and modify":
                case "read only":
                case "no permission":
                    return Claim;
                default:
                    return "";
            }
        }
    }
}
