using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Entity.Concrete.Identity;
using MiniShop.Shared.ViewModels.IdentityModels;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoles(string id)
        {
            //Idsi gönderilen, rol ataması yapılacak userı buluyoruz.
            var user = await _userManager.FindByIdAsync(id);

            //bulduğumuz userın var olan rollerini alıyoruz. var olan rollerin seçili gelmesi için yapacağımız işlem için bu gerekli.
            var userRoles= await _userManager.GetRolesAsync(user);


            //ilgili userınn rollerini de içerecek şekilde rol listesini yaratıyoruz. buradaki r tüm roller, x userroles.userroles ün içinde tüm rollerden olanlar için true döner.
            var roles = await _roleManager.Roles.Select(r => new AssignRoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
                IsAssigned=userRoles.Any(x=>x==r.Name)
            }).ToListAsync();

            //Viewin ihtiyacı olan user id  ve rol listesini içeren modeli yaratıyoruz.
            var userRolesViewModel = new UserRolesViewModel
            {
                Id= user.Id,
                Name=$"{user.FirstName} {user.LastName}",
                UserName=user.UserName,
                Roles = roles,
            };
            return View(userRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoles(UserRolesViewModel userRolesViewModel)
        {

            //modelstate kısmını alışkanlık haline getirmekte fayda var.
            if(ModelState.IsValid)
            {
                var user= await _userManager.FindByIdAsync(userRolesViewModel.Id);
                foreach(var role in userRolesViewModel.Roles)
                {
                    if (role.IsAssigned)
                    {
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                    }
                }
                return RedirectToAction("Index");

            }
            return View(userRolesViewModel);
           
        }

    }
}
