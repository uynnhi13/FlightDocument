using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentServices.Models
{
    public class PhanQuyenTaiLieu
    {
        [Key]
        public int TypeDocumentID { get; set; }
        [Key]
        public string NameRole { get; set; }
        public string Claims {  get; set; }
    }
}
