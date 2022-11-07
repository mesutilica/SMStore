using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> UrunleriKategoriveMarkaylaGetirAsync();
        Task<Product> UrunuKategoriVeMarkaylaGetir(int productId);
    }
}
