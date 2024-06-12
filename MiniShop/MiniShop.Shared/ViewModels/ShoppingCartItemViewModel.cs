using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        //sepetteki her bir satır için oluşturduğumuz viewmodel

        public int Id { get; set; } //sepeti silerken lazım  olacak.
        public int ProductId { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCartViewModel ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}
