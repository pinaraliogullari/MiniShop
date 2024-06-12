using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //sadece superadmine yetki verdik admin panele girebilmesi için. baþka roldeki bir user girmeye çalýþtýðýnda programcste   options.AccessDeniedPath = "/Account/AccessDenied"; ayarý ile belirttiðimiz sayfaya yönlendirilecek. 
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderService _orderManager;

        public HomeController(IOrderService orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Index()
        {
           var orders= await _orderManager.GetOrdersAsync();
            //aslýnda veritabanýndan çekerken 5 tanesini ya da ne kadar isteniyorsa o kadarýný çekmek daha mantýklý
            orders = orders.Take(5).ToList();

            return View(orders);
        }
    }
}
