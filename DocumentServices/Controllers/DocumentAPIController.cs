﻿using AutoMapper;
using DocumentServices.Data;
using DocumentServices.Models;
using DocumentServices.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public DocumentAPIController(AppDBContext db,IMapper mapper)
        {
            _db = db; 
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Document> objList = _db.Documents.ToList();
                _response.Result = _mapper.Map<IEnumerable<DocumentDTO>>(objList);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Document document=_db.Documents.First(u=>u.documentID==id);
                _response.Result = _mapper.Map<DocumentDTO>(document);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] DocumentDTO documentDTO) 
        {
            try
            {
                Document obj = _mapper.Map<Document>(documentDTO);
                _db.Documents.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<DocumentDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
