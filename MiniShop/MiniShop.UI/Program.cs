using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.Business.Abstract;
using MiniShop.Business.Concrete;
using MiniShop.Data.Abstract;
using MiniShop.Data.Concrete.Contexts;
using MiniShop.Data.Concrete.Repositories;
using MiniShop.Entity.Concrete.Identity;
using MiniShop.Shared.Helpers.Abstract;
using MiniShop.Shared.Helpers.Concrete;
using NToastNotify;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
   .AddNToastNotifyNoty(new NotyOptions()
    {
     ProgressBar=true,
     Timeout=3000,
      
    });

//context
builder.Services.AddDbContext<MiniShopDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);


//Identity paketinin user ve roller için verdiði paketleri istediðimi yerde kullanabilmek için containera ekliyoruz.Eðer bu sýnýfarý oluþturmamýþ olsaydýk IdentityUser, IdentityRole yazmalýydýk. Sýrasý da önemli.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MiniShopDbContext>()
    .AddDefaultTokenProviders(); //mail onayý için

builder.Services.Configure<IdentityOptions>(options =>
{
    #region Parola Ayarlarý
    options.Password.RequiredLength = 6; //parola en az 6 karakter olmalý.
    options.Password.RequireDigit = true; //parola sayýsal deðer içermeli
    options.Password.RequireNonAlphanumeric = true;//parola özel karakter içermeli
    options.Password.RequireUppercase = true; //parola büyük harf içermeli
    options.Password.RequireLowercase = true;//parola küçük harf içermeli
                                             //options.Password.RequiredUniqueChars //tekrar etmemesi istenen karakterler
    #endregion

    #region Hesap Kilitleme Ayarlarý
    
    options.Lockout.MaxFailedAccessAttempts = 3;//üstüste 3 hatalý giriþ denemesi/hakký. bu özelliði false verincce hesap kilitlenmez. true verince kilitlenir. 
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(15);//kilitlenmiþ bir hesaba yeniden giriþ yapabilmek için gereken bekleme süresi 
    //options.Lockout.AllowedForNewUsers = true; //yeniden kayýt olmaya imkan ver.
    #endregion

    options.User.RequireUniqueEmail = true;// her email sadece bir kez  kayýt olabilir.
    options.SignIn.RequireConfirmedEmail = false; //email onayý ayarý.


});

//cookie ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    //kullanýcý girþ yapmamýþ ama sepete ekleye bastý. veya admin panele girmeye çalýþtý .kullanýcýyý o sýrada yönlendireceðimiz sayfayý belirtme ayarý;
    options.LoginPath = "/Account/Login";
    //kullanýcý çýkýþ yaptýðýnda yönlendireceðimiz sayfa. default olarak anasayfadýr. tasarladýðýmýz özel bir sayfaya da yönlendirebiliiriz. örn: "/Account/Logout"
    options.LogoutPath = "/";
    //bir iþi yapmaya yetkisi olmayan kullanýcýlarý yönlendireceðimiz sayfa ayarý
    options.AccessDeniedPath = "/Account/AccessDenied";
    //cookinin yaþam süresi.45 saniye sonra logout olur eðer herhangi bir iþlem yapýlamazsa
    options.ExpireTimeSpan = TimeSpan.FromSeconds(45);//tarayýcýyý kapatmadýðýmýz sürece verilen süre boyunca uygulama çalýþýr.ama eðer ispersistantý true yaparsak tarayýcýyý kapatsak bile oturum açýk kalýr.
    options.SlidingExpiration = true; //eðer bu ayar false olursa iþlem yapsak da 45 saniye sonunda logout olur.
    //cookie yaratma
    options.Cookie = new CookieBuilder
    {
        Name = "MiniShop.Security.Cookie",
        HttpOnly = true,
        SameSite=SameSiteMode.Strict
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartManager>();
builder.Services.AddScoped<IShoppingCartItemService, ShoppingCartItemManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();



builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IImageHelper, ImageHelper>();




var app = builder.Build();
 app.UseNToastNotify();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//blog/abc-> blog controller article action çalýþsýn
//blog/def-> blog controller article action çalýþsýn
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", action = "Article" });

app.Run();
