using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMStore.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB; Database=SMStore; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI : Veritabanı tablo ve kolonlarını oluşturmak için data annotations a alternatif olarak kullanılabilen bir teknoloji
            modelBuilder.Entity<AppUser>().Property(a => a.Name)
                .HasColumnType("varchar(50)").HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
