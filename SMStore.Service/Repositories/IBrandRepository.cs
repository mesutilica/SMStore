using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> MarkayiUrunlerliyleGetir(int brandId);
    }
}
