using System;
using System.Collections.Generic;
using System.Linq.Expressions; // Expression lar veriyi sorgularken lambda expresiion kullanabilmemizi sağlar. Örneğin p=>p.Id == id gibi
using System.Text;
using System.Threading.Tasks;

namespace SMStore.Service.Repositories
{
    public interface IRepository<T> where T : class // IRepository interface i <T> yapısı ile generic hale getirildi. Buradaki T dışarıdan gönderilecek veri(class, değişken vb). where T : class ise buraya gönderilecek parametre class olmak zorundadır demek
    {
        // Burada Uygulamada kullanacağımız crud metotlarının imzalarını belirliyoruz.
        List<T> GetAll(); // verilerin hepsini getirecek metot
        List<T> GetAll(Expression<Func<T, bool>> expression); // uygulamada bu metodu lamda expression ile kullanabilmek için
        T Get(Expression<Func<T, bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
        // Asenkron Metotlar
        Task<T> FindAsync(int id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); // Asenkron void metot
        Task<int> SaveChangesAsync();
    }
}
