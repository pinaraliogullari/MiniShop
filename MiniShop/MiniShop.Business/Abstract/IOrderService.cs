using MiniShop.Entity.Concrete;
using MiniShop.Shared.ComplexTypes;
using MiniShop.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Business.Abstract
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task<List<AdminOrderViewModel>> GetOrdersAsync();
        //bu metotların isimlerini GetOrdersByUserId tarzında vermek daha güzel olur ama burada overload şeklinde yazmışız.
        Task<List<AdminOrderViewModel>> GetOrdersAsync(string userId);
        Task<List<AdminOrderViewModel>> GetOrdersAsync(int productId);
        Task<AdminOrderViewModel> GetOrderAsync(int orderId);

        //bu metot verdiğim id li orderı bulup orderStateini verdiğim orderState yapıyor.
        Task <string> ChangeStatus (int id,OrderState orderState);

      
    }
}
