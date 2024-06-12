using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity.Concrete.Identity
{
    //Identity paketinin içinde olmayan bazı özelliklere(Ad soyad adres gibi) ihtiyacımız olduğundan User sınıfını oluşturduk ve Identiyden miras alarak genişlettik. Eğer böyle bir ihtiyacımız olmasaydı tanımlamamıza gerek yoktu.
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
