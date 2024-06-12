using MiniShop.Business.Abstract;
using MiniShop.Data.Abstract;
using MiniShop.Shared.ResponseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Business.Concrete
{
    public class ShoppingCartItemManager : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartItemManager(IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Response<NoContent>> ChangeQuantityAsync(int shoppingCartItemId, int quantity)
        {
            var shoppingCartItem= await _shoppingCartItemRepository.GetByIdAsync(x=>x.Id== shoppingCartItemId);
            await _shoppingCartItemRepository.ChangeQuantityAsync(shoppingCartItem, quantity);
            return Response<NoContent>.Success();
        }

        public async Task<int> CountAsync(int shoppingCartId)
        {
            return await _shoppingCartItemRepository.GetCount(x=>x.ShoppingCartId==shoppingCartId);
        }

     
        public async Task<Response<NoContent>> DeleteFromShoppingCartAsync(int shoppingCartItemId)
        {
            //aslında repository üzerinde bu işe özel bir metot yazmıştık ama burada generic repoda yazdığımız metotları kullanıyoruz. bu yol daha performanslı.
            var deletedCart =  await _shoppingCartItemRepository.GetByIdAsync(x=>x.Id== shoppingCartItemId);
            await _shoppingCartItemRepository.HardDeleteAsync(deletedCart);
            return Response<NoContent>.Success();
        }
    }
}
