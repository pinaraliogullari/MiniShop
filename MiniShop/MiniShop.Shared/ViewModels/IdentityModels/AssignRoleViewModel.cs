using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    //rol atama viewi için tasarladığımız view model
    public class AssignRoleViewModel
    {
        public string  RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsAssigned { get; set; }

    }
}
