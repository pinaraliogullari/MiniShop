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


//Identity paketinin user ve roller i�in verdi�i paketleri istedi�imi yerde kullanabilmek i�in containera ekliyoruz.E�er bu s�n�far� olu�turmam�� olsayd�k IdentityUser, IdentityRole yazmal�yd�k. S�ras� da �nemli.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MiniShopDbContext>()
    .AddDefaultTokenProviders(); //mail onay� i�in

builder.Services.Configure<IdentityOptions>(options =>
{
    #region Parola Ayarlar�
    options.Password.RequiredLength = 6; //parola en az 6 karakter olmal�.
    options.Password.RequireDigit = true; //parola say�sal de�er i�ermeli
    options.Password.RequireNonAlphanumeric = true;//parola �zel karakter i�ermeli
    options.Password.RequireUppercase = true; //parola b�y�k harf i�ermeli
    options.Password.RequireLowercase = true;//parola k���k harf i�ermeli
                                             //options.Password.RequiredUniqueChars //tekrar etmemesi istenen karakterler
    #endregion

    #region Hesap Kilitleme Ayarlar�
    
    options.Lockout.MaxFailedAccessAttempts = 3;//�st�ste 3 hatal� giri� denemesi/hakk�. bu �zelli�i false verincce hesap kilitlenmez. true verince kilitlenir. 
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(15);//kilitlenmi� bir hesaba yeniden giri� yapabilmek i�in gereken bekleme s�resi 
    //options.Lockout.AllowedForNewUsers = true; //yeniden kay�t olmaya imkan ver.
    #endregion

    options.User.RequireUniqueEmail = true;// her email sadece bir kez  kay�t olabilir.
    options.SignIn.RequireConfirmedEmail = false; //email onay� ayar�.


});

//cookie ayarlar�
builder.Services.ConfigureApplicationCookie(options =>
{
    //kullan�c� gir� yapmam�� ama sepete ekleye bast�. veya admin panele girmeye �al��t� .kullan�c�y� o s�rada y�nlendirece�imiz sayfay� belirtme ayar�;
    options.LoginPath = "/Account/Login";
    //kullan�c� ��k�� yapt���nda y�nlendirece�imiz sayfa. default olarak anasayfad�r. tasarlad���m�z �zel bir sayfaya da y�nlendirebiliiriz. �rn: "/Account/Logout"
    options.LogoutPath = "/";
    //bir i�i yapmaya yetkisi olmayan kullan�c�lar� y�nlendirece�imiz sayfa ayar�
    options.AccessDeniedPath = "/Account/AccessDenied";
    //cookinin ya�am s�resi.45 saniye sonra logout olur e�er herhangi bir i�lem yap�lamazsa
    options.ExpireTimeSpan = TimeSpan.FromSeconds(45);//taray�c�y� kapatmad���m�z s�rece verilen s�re boyunca uygulama �al���r.ama e�er ispersistant� true yaparsak taray�c�y� kapatsak bile oturum a��k kal�r.
    options.SlidingExpiration = true; //e�er bu ayar false olursa i�lem yapsak da 45 saniye sonunda logout olur.
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


//blog/abc-> blog controller article action �al��s�n
//blog/def-> blog controller article action �al��s�n
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", action = "Article" });

app.Run();
