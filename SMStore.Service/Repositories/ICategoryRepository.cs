﻿using SMStore.Entities;

namespace SMStore.Service.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> KategoriyiUrunlerliyleGetir(int categoryId);
    }
}
