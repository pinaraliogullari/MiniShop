using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Boş bırakılamaz")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz")]
        [DisplayName("Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
