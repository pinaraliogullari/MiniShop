using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    public class UserProfileViewModel
    {
        public List<AdminOrderViewModel> Orders { get; set; }
        public UserViewModel User { get; set; }
    }
}
