using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //sadece superadmine yetki verdik admin panele girebilmesi i�in. ba�ka roldeki bir user girmeye �al��t���nda programcste   options.AccessDeniedPath = "/Account/AccessDenied"; ayar� ile belirtti�imiz sayfaya y�nlendirilecek. 
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
            //asl�nda veritaban�ndan �ekerken 5 tanesini ya da ne kadar isteniyorsa o kadar�n� �ekmek daha mant�kl�
            orders = orders.Take(5).ToList();

            return View(orders);
        }
    }
}
