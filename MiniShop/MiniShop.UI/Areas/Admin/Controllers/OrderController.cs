﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniShop.Business.Abstract;
using MiniShop.Entity.Concrete;
using MiniShop.Shared.ComplexTypes;
using MiniShop.Shared.Extensions;
using MiniShop.UI.Areas.Admin.Models;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Authorize(Roles ="SuperAdmin,Admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderManager;
        private readonly IProductService _productManager;

        public OrderController(IOrderService orderManager, IProductService productManager)
        {
            _orderManager = orderManager;
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderManager.GetOrdersAsync();
            var result= await _productManager.GetAllNonDeletedAsync();
            var products = result.Data;
            //viewde selecti asp-for-items ile kullanabilmek için ;
            //view içindeki diğer yoldan daha zor görünüyor ama aslında ihtiyacımız olan her yerde kullanabiliriz.ayrıca name ,id değerleri ile de uğraşmamıza gerek kalmaz.
            List<SelectListItem> productListItems = products
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();

            var model = new OrderFilterViewModel
            {
                Orders = orders,
                ProductListItems = productListItems
            };
            return View(model);
        }

        public async Task<IActionResult> FilterByProduct(int id)
        {
            if (id == 0)
            {
                var orders = await _orderManager.GetOrdersAsync();
                //ajax uygulayacağımız kısmı partial yapıp ona buradan veriyi gönderiyoruz.
                return PartialView("_OrderListPartial", orders);
            }
            //tüm ürünler seçilince çalışacak ;
            else
            {
                var orders = await _orderManager.GetOrdersAsync(id);
                return PartialView("_OrderListPartial", orders);
            }
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {

            var orderState = await _orderManager.ChangeStatus(id,OrderState.Preparing);
            return Json (orderState);
        }
    }
}
