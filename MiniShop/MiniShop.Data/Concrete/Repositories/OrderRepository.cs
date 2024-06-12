using Microsoft.EntityFrameworkCore;
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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        //program cs te oluşturulan MinShopDbContext constructor ile alınıyor ve base e yani Generic repoya gönderiliyor . orada _dbContext değişkeni(tipi DbContext. DbContext MiniShopDbContexti kapsıyor) ile yakalanıp tekrar buraya  geliyor.
        public OrderRepository(MiniShopDbContext _context) : base(_context)
        {
        }
        MiniShopDbContext MiniShopDbContext
        { 
            get { return _dbContext as MiniShopDbContext; }
        }

        public async Task<List<Order>> GetAllOrdersByProductIdAsync(int productId)
        {
            //Where kullanıldığında, sadece Order öğelerini filtreleriz, ancak bu öğelerin alt koleksiyonlarına (yani OrderDetails) erişip onları da filtrelemek için Any metodunu kullanmamız gerekir. Yani, Where sadece Order öğelerini filtreler ve Any, bu filtrelenmiş Order öğelerinin alt koleksiyonları olan OrderDetails içinde belirli bir koşulu sağlayan öğe olup olmadığını kontrol eder. Bu şekilde, sadece belirli bir productId'ye sahip olan Order öğelerini alırız.
            var result = await MiniShopDbContext
               .Orders
               .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .Where(o => o.OrderDetails.Any(x => x.ProductId == productId))
               .OrderByDescending(x=>x.Id)
               .ToListAsync();
            return result;

        }
    }
}
