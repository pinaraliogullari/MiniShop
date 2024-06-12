using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{

    public class AssignUserViewModel
    {
        public string  UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAssigned { get; set; }

    }
}
