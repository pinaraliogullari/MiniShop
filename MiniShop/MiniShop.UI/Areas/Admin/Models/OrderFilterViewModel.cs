using Microsoft.AspNetCore.Mvc.Rendering;
using MiniShop.Shared.ViewModels;

namespace MiniShop.UI.Areas.Admin.Models
{
    public class OrderFilterViewModel
    {
        //admin siparişler sayfası için tasarladığımız viewmodel
        public List<AdminOrderViewModel>  Orders{ get; set; }
        //select kullanımında ilk yol için public List<ProductViewModel>  Products{ get; set; }
        public List<SelectListItem> ProductListItems { get; set; }
    }
}
