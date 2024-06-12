using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Shared.ViewModels.IdentityModels
{
    public class RegisterViewModel
    {
        //Sistemin kayıt olacağı sayfadaki gireceği bilgileri karşılamak için oluşturduğumuz ViewModel.User classındaki property isimlerine uygun giriyoruz.
        //Şifre en az bir sayı içersin vs gibi ayarları ıdentity paketi sağlıyor program cs te ayarını yapacağız. 
        [DisplayName("Ad")]
        [Required(ErrorMessage ="Lütfen ad alanını boş bırakmayınız")]
        public  string FirstName { get; set; }


        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Lütfen soyad alanını boş bırakmayınız")]
        public  string LastName { get; set; }


        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adı alanını boş bırakmayınız")]
        public  string UserName { get; set; }


        [DisplayName("Email")]
        [Required(ErrorMessage = "Lütfen email alanını boş bırakmayınız")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Lütfen geçerli bir email adresi giriniz")]
        public  string Email  { get; set; }


        [DisplayName("Parola")]
        [Required(ErrorMessage = "Lütfen parola alanını boş bırakmayınız")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }


        [DisplayName("Parola Tekrar")]
        [Required(ErrorMessage = "Lütfen parola tekrar alanını boş bırakmayınız")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor, kontrol ediniz.")]
        public  string RePassword { get; set; }



    }
}
