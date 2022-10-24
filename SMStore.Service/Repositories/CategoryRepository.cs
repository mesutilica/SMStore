using Microsoft.EntityFrameworkCore;
using SMStore.Data;
using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Category> KategoriyiUrunlerliyleGetir(int categoryId)
        {
            return await _databaseContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c=>c.Id == categoryId);
        }
    }
}
