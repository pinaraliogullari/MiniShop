using MiniShop.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Data.Abstract
{
    public interface IShoppingCartRepository:IGenericRepository<ShoppingCart>
    {

        //user ıdsi ile cartı bulmak için
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);

        Task ClearShoppingCartAsync(int shoppingCartId); //kullanmadık. generic repodaki metotu kullandık.



    }
}
