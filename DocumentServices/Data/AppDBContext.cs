using DocumentServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DocumentServices.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Models.Document> Documents { get; set; }
        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<PhanQuyenTaiLieu> phanQuyenTaiLieus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình composite primary key cho PhanQuyenTaiLieu
            modelBuilder.Entity<PhanQuyenTaiLieu>()
                .HasKey(p => new { p.TypeDocumentID, p.NameRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}
