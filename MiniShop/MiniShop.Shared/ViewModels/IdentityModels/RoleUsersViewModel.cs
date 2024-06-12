using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    public class RoleUsersViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AssignUserViewModel> Users { get; set; } 

    }
}
