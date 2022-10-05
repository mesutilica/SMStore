using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMStore.Entities;

namespace SMStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
            builder.Property(x => x.ProductCode).HasMaxLength(30);
            // Burada class lar arasındaki ilişkileri de belirtebiliyoruz
            builder.HasOne(b => b.Brand).WithMany(p => p.Products).HasForeignKey(b => b.BrandId); // Burada Brand class ı ile product class ı arasında bire çok bir ilişki olacağını belirttik. 1 olan kısım Brand olduğu için hasone özelliğine brand i belirttik. Çok olan kısımproducts olacağı için bunu da WithMany içerisinde belirttik. HasForeignKey de ise veritabanında oluşacak kolonlardan BrandId nin foreign key olacağını belirttik
            builder.HasOne(b => b.Category).WithMany(p => p.Products).HasForeignKey(b => b.CategoryId);
        }
    }
}
