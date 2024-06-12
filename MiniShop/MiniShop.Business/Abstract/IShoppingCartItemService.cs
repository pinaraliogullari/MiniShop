using MiniShop.Shared.ResponseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Business.Abstract
{
    public interface IShoppingCartItemService
    {
        Task<Response<NoContent>> ChangeQuantityAsync(int shoppingCartItemId, int quantity);
        Task<int> CountAsync(int shoppingCartId); // sepetteki ürünleri saymak için 
        Task<Response<NoContent>> DeleteFromShoppingCartAsync(int shshoppingCartItemId);
       
    }
}
