using Microsoft.EntityFrameworkCore;
using SMStore.Data;
using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Product> UrunuKategoriVeMarkaylaGetir(int productId)
        {
            return await _databaseContext.Products.Include(c => c.Brand).Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == productId);
        }
    }
}
