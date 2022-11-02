using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UrunuKategoriVeMarkaylaGetir(int productId);
    }
}
