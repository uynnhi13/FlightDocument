namespace DocumentServices.Models.DTO
{
    public class DocumentDTO
    {
        public int documentID { get; set; }
        public string? nameDocument { get; set; }
        public DateTime updateTime { get; set; }
        public string? creator { get; set; }
        public string? typeDocument { get; set; }
    }
}
