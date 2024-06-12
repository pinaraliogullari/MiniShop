using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;
using MiniShop.Business.Concrete;

namespace MiniShop.UI.ViewComponents
{
    public class ProductSectionViewComponent:ViewComponent
    {

        private readonly IProductService _productManager;

        public ProductSectionViewComponent(IProductService productManager)
        {
            _productManager = productManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productManager.GetAllNonDeletedAsync();
            return View(products.Data);
        }
    }
}
