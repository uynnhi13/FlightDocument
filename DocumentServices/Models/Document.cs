using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentServices.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int documentID { get; set; }
        [Required]
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        [Required]
        public string? creator { get; set; }
        [Required]
        public int? TypeDocumentId { get; set; }
        [ForeignKey("TypeDocumentId")]
        public TypeDocument typeDocumentType { get; set; }
        public string? filePath { get; set; }
        public int? FlightId { get; set; }
        public string? Version { get; set; }
        [NotMapped]
        public FlightDto Flight { get; set; }
    }
}
