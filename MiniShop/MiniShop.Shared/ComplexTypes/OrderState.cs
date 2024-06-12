using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ComplexTypes
{
    //enum bir tiptir. içinde veri tutar.
    //enum yapısı bir class değil bu nedenle herhangi bir viewe model olarak verilemez bu durumda da asp-for kullanılamaz.
    //bu yüzden extensionunu oluşturarak bu displaylerle beraber viewe gönderilmesini sağlıyoruz.
    public enum OrderState
    {
        [Display(Name = "Sipariş alındı")]
        Received = 0,

        [Display(Name = "Hazırlanıyor")]
        Preparing = 1,

        [Display(Name = "Gönderildi")]
        Sent = 2,

        [Display(Name = "Teslim edildi")]
        Delivered = 3


    }
}
