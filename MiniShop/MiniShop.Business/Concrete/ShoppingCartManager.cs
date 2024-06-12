using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniShop.Business.Abstract;
using MiniShop.Data.Abstract;
using MiniShop.Entity.Concrete;
using MiniShop.Shared.ResponseViewModels;
using MiniShop.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

 

        public async Task<Response<NoContent>> AddToCartAsync(string userId, int productId, int quantity)
        {
            ShoppingCart shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            if (shoppingCart != null)
            {
                //Eğer ürün daha önceden sepette varsa, sıra numarası bulunur ve index içine konulur.
                //Eğer ürün daha önceden sepette yoksa, sıra numarası -1 olarak döner.
                var index = shoppingCart.ShoppingCartItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)
                {
                    //burada cartıtem içindeki productın içindeki pekçok özelliği doldurmadığımız için hata almıştık.onları nullable olarak işaretlersek sorunun çözüleceğini söyledi hoca.
                    shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        ShoppingCartId = shoppingCart.Id
                    });
                }
                else
                {
                    shoppingCart.ShoppingCartItems[index].Quantity += quantity;
                }
                await _shoppingCartRepository.UpdateAsync(shoppingCart);
                return Response<NoContent>.Success();
            }
            return Response<NoContent>.Fail("Bir hata oluştu");
        }

        public async Task<Response<NoContent>> ClearShoppingCartAsync(int shoppingCartId)
        {
            //aslında repository üzerinde bu işe özel bir metot yazmıştık ama burada generic repoda yazdığımız metotları kullanıyoruz. bu yol daha performanslı.
             var cart = await _shoppingCartRepository.GetByIdAsync(x=>x.Id==shoppingCartId,
              source=>source
              .Include(x=>x.ShoppingCartItems));
            cart.ShoppingCartItems= new List<ShoppingCartItem>();
            await _shoppingCartRepository.UpdateAsync(cart);
            return Response<NoContent>.Success();
        }

        public async Task<Response<ShoppingCartViewModel>> GetShoppingCartByUserIdAsync(string userId)
        {
        var shoppingCart= await _shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            if(shoppingCart == null)
            {
                return Response<ShoppingCartViewModel>.Fail("İlgili kullanıcnın sepetinde sorun var");
            }
            var  result= _mapper.Map<ShoppingCartViewModel>(shoppingCart);
            return Response<ShoppingCartViewModel>.Success(result);
        }

        public Task<Response<NoContent>> InitializeShoppingCartAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
