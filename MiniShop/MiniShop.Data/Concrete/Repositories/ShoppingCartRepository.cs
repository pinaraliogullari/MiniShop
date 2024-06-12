using Microsoft.EntityFrameworkCore;
using MiniShop.Data.Abstract;
using MiniShop.Data.Concrete.Contexts;
using MiniShop.Entity.Concrete;
using MiniShop.Shared.ResponseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Data.Concrete.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(MiniShopDbContext _context):base(_context)
        {

        }
        private MiniShopDbContext MiniShopDbContext
        {
            get { return _dbContext as MiniShopDbContext; }
        }

        
        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
        {
            //userIDye göre  shoppingCartı getirecek.
            var shoppingCart = await MiniShopDbContext
                 .ShoppingCarts
                 .Where(x => x.UserId == userId)
                 .Include(sc => sc.ShoppingCartItems)
                 .ThenInclude(sci => sci.Product)
                 .SingleOrDefaultAsync();
            return shoppingCart;

        }
        public async Task ClearShoppingCartAsync(int shoppingCartId)
        {
            //ADONET ile
            //var query = @"DELETE FROM ShoppingCartItems WHERE ShoppingCartId=@p0";
            //await MiniShopDbContext.Database.ExecuteSqlRawAsync(query, shoppingCartId);

            var deletedShoppingCartItems = await MiniShopDbContext
                .ShoppingCartItems
                .Where(x => x.ShoppingCartId == shoppingCartId)
                .ToListAsync();
            //toplu olarak silmek için removerange kullandık
            MiniShopDbContext.ShoppingCartItems.RemoveRange(deletedShoppingCartItems);
            await MiniShopDbContext.SaveChangesAsync();
        }
    }
}
