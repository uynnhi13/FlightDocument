using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentServices.Models.DTO
{
    public class DocumentDetailDTO
    {
        public int documentID { get; set; }
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        public string? creator { get; set; }
        public int TypeDocumentID { get; set; }
        public string? filePath { get; set; }
        public int? FlightId { get; set; }
        public string? Version { get; set; }
    }
}
