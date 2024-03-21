namespace DocumentServices.Models.DTO
{
    public class TypeDocumentDTO
    {
        public int TypeId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public string Creator { get; set; } //Là Gmail.
    }
}
