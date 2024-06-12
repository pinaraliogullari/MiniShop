using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;
using MiniShop.Entity.Concrete.Identity;

namespace MiniShop.UI.ViewComponents
{
    public class ShoppingCartNotificationViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IShoppingCartItemService _shoppingCartItemManager;
        private readonly IShoppingCartService _shoppingCartManager;

        public ShoppingCartNotificationViewComponent(UserManager<User> userManager, IShoppingCartItemService shoppingCartItemManager, IShoppingCartService shoppingCartManager)
        {
            _userManager = userManager;
            _shoppingCartItemManager = shoppingCartItemManager;
            _shoppingCartManager = shoppingCartManager;
        }

        //ViewComponent'ler, Controller sınıfları gibi doğrudan User özelliğine erişemezler çünkü ViewComponent'ler, Controller sınıfları gibi doğrudan bir HTTP isteğine yanıt vermezler.Bunun yerine, ViewComponent'ler, bir view içinde çağrılarak bir kısmi görünüm oluştururlar. Bu nedenle, HTTP isteğinin bir parçası olarak bir kullanıcı kimliğine (User) erişmek mümkün değildir.
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId != null)
            {
               
                    var shoppingCart = await _shoppingCartManager.GetShoppingCartByUserIdAsync(userId);
                    var count = await _shoppingCartItemManager.CountAsync(shoppingCart.Data.Id);
                    return View("Default",count);
            }
            return View("EmptyCart");


            //2. yol hocanın çözdüğü yol: headersectionpartialdan userı getirdi(parametre olarak verereek.) ve iki cshtml oluşturmadı .
            //if (userName != null)
            //{
            //    var user = await _userManager.FindByNameAsync(userName);
            //    var shoppingCart = await _shoppingCartManager.GetShoppingCartByUserIdAsync(user.Id);
            //    var count = await _shoppingCartItemManager.CountAsync(shoppingCart.Data.Id);
            //    return View(count);
            //}


            //return View(0);


        }
    }
}
