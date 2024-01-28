using System.ComponentModel.DataAnnotations;

namespace FlightDocument.Models
{
    public class DocumentDTO
    {
        public int documentID { get; set; }
        [Required(ErrorMessage = "Tên tài liệu là bắt buộc.")]
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        public string? creator { get; set; }
        public string? typeDocument { get; set; }
    }
}
