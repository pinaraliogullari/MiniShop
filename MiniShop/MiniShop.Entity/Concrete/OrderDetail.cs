using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity.Concrete
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }//fiyat değişebilir, alışveriş yapıldığı zamanki halini tutmak istediğimiz için bunu ekledik.
        public int Quantity { get; set; }
    }
}
