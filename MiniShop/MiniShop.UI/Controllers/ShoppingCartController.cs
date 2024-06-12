using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;
using MiniShop.Entity.Concrete.Identity;
using MiniShop.Shared.ViewModels;

namespace MiniShop.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        //kullanıcının sepetini gösterecek.
        private readonly IShoppingCartService _shoppingCartManager;
        private readonly UserManager<User> _userManager;
        private readonly IShoppingCartItemService _shoppingCartItemManager;

        public ShoppingCartController(IShoppingCartService shoppingCartManager, UserManager<User> userManager, IShoppingCartItemService shoppingCartItemManager)
        {
            _shoppingCartManager = shoppingCartManager;
            _userManager = userManager;
            _shoppingCartItemManager = shoppingCartItemManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId =  _userManager.GetUserId(User);
            var shoppingCart= await _shoppingCartManager.GetShoppingCartByUserIdAsync(userId);
            return View(shoppingCart.Data);
        }

     
        public async Task<IActionResult> AddToCartAsync(int productId,int quantity=1)
        {   
            //Controller sınıfları, ControllerBase sınıfından türetilir ve ControllerBase, ASP.NET Core kimlik doğrulama ve yetkilendirme özelliklerini kullanır. Bu nedenle, Controller sınıflarının içindeki User özelliği, o anki isteği yapan kullanıcıyı temsil eder.

            var userId =  _userManager.GetUserId(User); //içerideki büyük harfli User o an login olan /online olan userdır. Identity paketi sayesinde bulunuyor.
            await _shoppingCartManager.AddToCartAsync(userId,productId, quantity);
            return RedirectToAction("Index");
        }
        public async Task <IActionResult>ChangeQuantity(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            if(ModelState.IsValid)
            {
                await _shoppingCartItemManager.ChangeQuantityAsync(shoppingCartItemViewModel.Id, shoppingCartItemViewModel.Quantity);
               
                return RedirectToAction("Index");
                
            }
            ViewBag.MessageofEmptyCart = "Sepetinizde ürün bulunmamaktadır.";
            return View(shoppingCartItemViewModel);
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            await _shoppingCartItemManager.DeleteFromShoppingCartAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart(int id)
        {
            await _shoppingCartManager.ClearShoppingCartAsync(id);
            return RedirectToAction("Index");
        }

     
    }
}
