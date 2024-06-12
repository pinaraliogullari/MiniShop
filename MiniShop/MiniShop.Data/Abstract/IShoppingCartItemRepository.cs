using MiniShop.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Data.Abstract
{
    public interface IShoppingCartItemRepository:IGenericRepository<ShoppingCartItem>
    {
        Task ChangeQuantityAsync(ShoppingCartItem shoppingCartItem, int quantity);
 
        Task DeleteFromShoppingCartAsync(int shoppingCartId, int productId);  //carttan ürün silmek için ama generic repodaki metotu kullandık.

 


    }
}
