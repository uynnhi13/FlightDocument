using System.ComponentModel.DataAnnotations;

namespace DocumentServices.Models
{
    public class Document
    {
        [Key]
        public int documentID { get; set; }
        [Required]
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        [Required]
        public string? creator { get; set; }
        [Required]
        public string? typeDocument { get; set; }
    }
}
