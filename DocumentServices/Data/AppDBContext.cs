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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Document>().HasData(new Models.Document
            {
                documentID = 1,
                nameDocument = "HelloWorld",
                typeDocument = "load Sumary",
                creator = "uynnhi"
            });

            modelBuilder.Entity<Models.Document>().HasData(new Models.Document
            {
                documentID = 2,
                nameDocument = "HelloSadari",
                typeDocument = "load Sumary",
                creator = "kimenk"
            });
        }
    }
}
