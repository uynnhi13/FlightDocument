using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentServices.Models.DTO
{
    public class DocumentDTO
    {
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        public IFormFile? filePDF { get; set; }
        public int? FlightId { get; set; }
        public int TypeDocumentID { get; set; }
    }
}
