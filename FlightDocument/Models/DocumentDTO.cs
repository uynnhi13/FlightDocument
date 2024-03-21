using FlightDocument.Models;
using System.ComponentModel.DataAnnotations;

namespace FlightDocument.Models
{
    public class DocumentDTO
    {
        public int? documentID { get; set; }
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        public string? creator { get; set; }
        public string? typeDocument { get; set; }
        public IFormFile? filePDF { get; set; }
        public string? Version { get; set; }
        public int? FlightId { get; set; }
        public FlightDto? Flight { get; set; }
    }
}
