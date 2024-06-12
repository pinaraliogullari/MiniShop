using Microsoft.AspNetCore.Mvc;

namespace MiniShop.UI.ViewComponents
{
    public class MessageNotificationViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View();
        }
    }
}
