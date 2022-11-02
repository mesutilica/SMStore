using Microsoft.EntityFrameworkCore;
using SMStore.Data;
using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Brand> MarkayiUrunlerliyleGetir(int brandId)
        {
            return await _databaseContext.Brands.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == brandId);
        }
    }
}
