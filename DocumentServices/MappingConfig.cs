using AutoMapper;
using DocumentServices.Models;
using DocumentServices.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentServices
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DocumentDTO, Document>();
                config.CreateMap<Document, DocumentDTO>();
                config.CreateMap<Document, DocumentDetailDTO>();
                config.CreateMap<TypeDocument, TypeDocumentDTO>();
                config.CreateMap<PhanQuyenTaiLieu, PhanQuyenDto>();
                config.CreateMap<PhanQuyenDto, PhanQuyenTaiLieu >();
            });
            return mappingConfig;
        }
    }
}
