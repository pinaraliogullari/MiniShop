using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public List<AssignRoleViewModel> Roles { get; set; } //bu sayede hem rolleri hem de üzerinde çalıştığım kullanıcının rolleri hangileri onları göndrebiliyorum.
        //productlar için edit sayfası tasarladığımızda categorleri ayrıca gönderip hangisinin tikli olup olmayacağının kontrolünü viewde tasarlamıştık. ama bu yaklaşımda viewe göndermeden bu işi halletmiş olduk.

    }
}
