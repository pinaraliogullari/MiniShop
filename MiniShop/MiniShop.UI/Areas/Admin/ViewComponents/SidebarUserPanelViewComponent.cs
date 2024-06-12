using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using MiniShop.Entity.Concrete.Identity;

namespace MiniShop.UI.Areas.Admin.ViewComponents
{
 
   
    public class SidebarUserPanelViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public SidebarUserPanelViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //burada usernamei mainsidebarviewcomponentsin cshtml dosyas�ndan parametre olarak g�nderdim.ShoppingCartNotificationViewComponentin cs dosyas�nda online olan Usera direkt httpcontext �zerinden eri�mi�tim.
        public async Task<IViewComponentResult> InvokeAsync(string username)
        {
            if(username!=null)
            {
                var name = await _userManager.FindByNameAsync(username);
              var fullName=  name.FirstName +" "+ name.LastName;
                ViewBag.FullName= fullName;
                return View();
                //veya;
                //return View("default",fullName);
            }
           
          return View();
     
        }
    }
}