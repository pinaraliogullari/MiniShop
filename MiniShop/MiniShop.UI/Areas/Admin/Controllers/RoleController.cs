using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Entity.Concrete.Identity;
using MiniShop.Shared.ViewModels.IdentityModels;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles =await  _roleManager.Roles.ToListAsync();
            return View(roles);
        }


      public async Task<IActionResult> AssignUsers(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleUsers = await _userManager.GetUsersInRoleAsync(role.Name);

            var roleUserNames = roleUsers.Select(x => x.UserName).ToList(); 

            var users = await _userManager.Users.Select(u => new AssignUserViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                FirstName= u.FirstName,
                LastName= u.LastName,            
                IsAssigned = roleUserNames.Contains(u.UserName)
            }).ToListAsync();


            var roleUserViewModel = new RoleUsersViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Users = users,
            };
            return View(roleUserViewModel);
        }
        //[HttpPost]
        //public async Task<IActionResult> AssignUsers(RoleUsersViewModel roleUsersViewModel)
        //{

          

        //}


    }
}
