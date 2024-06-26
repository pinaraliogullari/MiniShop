﻿using Microsoft.EntityFrameworkCore;
using MiniShop.Data.Abstract;
using MiniShop.Data.Concrete.Contexts;
using MiniShop.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Data.Concrete.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MiniShopDbContext _context) : base(_context)
        {

        }

        private MiniShopDbContext MiniShopDbContext
        {
            get { return _dbContext as MiniShopDbContext; }
        }

        public async Task<List<Category>> GetTopCategories(int n)
        {
            //ana sayfada istediğimiz sayıda kategori görüntülemek için gerekli olan fonksiyon . eğer son n categoryyi listelemek istersek order by da kullanmamız gerekir.
            var categories = await MiniShopDbContext
                .Categories
                .Where(c=>c.IsActive&& !c.IsDeleted)
                .Take(n)
                .ToListAsync();
            return categories;
        }
    }
}
