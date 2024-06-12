using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;
using MiniShop.Entity.Concrete.Identity;
using MiniShop.Shared.ViewModels.IdentityModels;

namespace MiniShop.UI.Controllers
{
    public class AccountController : Controller
    {
        //kullanıcı listesini çekme, kullanıcı oluşturma, mail adresi şu olan kullanıcıyı getir vs gibi işlemleri yapar.
        private readonly UserManager<User> _userManager;
        //kullanıcı login işlemlerini yapar.
        private readonly SignInManager<User> _signInManager;
        private readonly IOrderService _orderManager;

        //categorymanagerda ve diğerlerinde kendimiz containera soyut sınıfları ekleyip bu soyut sınıfı yazdığımda bu somut sınıfı ver demiştik. Ancak burada servis havuzuna ekleme işlemini programcse yazdığımız AddIdentity satırında sağladık.
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IOrderService orderManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _orderManager = orderManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                };
                //usermanagerdan gelen createAsync; verdiğimiz userı kaydeder ve passwordü hashleyerek kaydeder.
                var result= await _userManager.CreateAsync(user,registerViewModel.Password); 
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //eğer login olmadan herhangi bir sayfaya girersek program.cste yaptığımız ayardan(   options.LoginPath = "/Account/Login";) dolayı login ekranına yönlendiriliriz. ve login olduktan sonra da kaldığımız sayfadan devam etmeyi bekleriz.aşağıdaki kodlar ile bunu sağlamış oluyoruz. login olmadan örn: admin sayfasına girdiğimizde tarayıcı çubuğunda returnUrl de eklenmiş olur. bu returnurlyi bu metotta parametre olarak alıyoruz. eğer herhangi bir sayfada kalmamışsak yani direkt giriş yapa basarak login oluyorsak returnurl oluşmayacaktır. dolayısıyla burada null kontrolü de yapmamız gerekiyor.
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Şifre hatalı");
                return View(loginViewModel);
            }
            var returnUrl = TempData["ReturnUrl"]?.ToString();//temdatanın içi logout durumunda boşaltılğı için buraya null olabilir dedik soru işareti koyarak.
            if(!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home"); //giriş yapınca ana sayfaya yönledniriliyoruz.
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();//ilgili cookiyi temizler. ve logout olmamızı sağlar.
            TempData["ReturnUrl"] = null;//temdatanın içi dolu kaldığı için logout yapıpı yeniden girdiğimizde bile yönlendirilmiş sayfada kalıyorduk bu sebeple içini boşalttık.
            return RedirectToAction("Index", "Home");
        }

        public  IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var orders=await _orderManager.GetOrdersAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);

            UserProfileViewModel userProfileViewModel = new UserProfileViewModel
            {
                User = new UserViewModel
                {
                    Id = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    City = user.City,
                    UserName = user.UserName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                },
                Orders = orders,
            };

           
            return View(userProfileViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel userProfileViewModel)
        {

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                user.FirstName = userProfileViewModel.User.FirstName;
                user.LastName = userProfileViewModel.User.LastName;
                user.Email = userProfileViewModel.User.Email;
                user.City = userProfileViewModel.User.City;
                user.Address = userProfileViewModel.User.Address;
                user.PhoneNumber = userProfileViewModel.User.PhoneNumber;
                user.DateOfBirth = userProfileViewModel.User.DateOfBirth;
                user.Gender = userProfileViewModel.User.Gender;
                user.UserName = userProfileViewModel.User.UserName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Redirect("~/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }

            }
            userProfileViewModel.Orders = await _orderManager.GetOrdersAsync(userId);
            return View(userProfileViewModel);
        }
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);
                //eski şifre doğru mu?
                var isVerified = await _userManager.CheckPasswordAsync(user, changePasswordViewModel.OldPassword);
                //eski şifre doğruysa;
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, changePasswordViewModel.NewPassword, false, false);
                        return RedirectToAction("Profile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
                //eski şifre yanlışsa;
                ModelState.AddModelError("", "Geçerli şifreniz hatalıdır");


            }
            return View(changePasswordViewModel);
        }

    }
}
