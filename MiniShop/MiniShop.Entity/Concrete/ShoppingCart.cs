using MiniShop.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity.Concrete
{
    public class ShoppingCart:IMainEntity
    {
        //bu entitye sadece ıd ve created date gerekiyordu dolayısıyla base entityden miras almak gereksiz olacaktı bu yüzden yeni bir sadece bu değerleri barındıran yeni bir (IMainEntity) oluşturduk.
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
