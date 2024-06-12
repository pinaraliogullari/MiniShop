using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniShop.Data.Concrete.Configs;
using MiniShop.Data.Extensions;
using MiniShop.Entity.Concrete;
using MiniShop.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Data.Concrete.Contexts
{
    //normalde bu metodun parametresinde DbContext yazıyprdu. Ama Identity paketi kullanınca bu şekilde değiştiriyoruz. IdentityDbContext zaten DbContexti de kapsar. son da verdiğimizi string , Identity paketi  tarafından  verileccek Id nin default tipidir.
    //eğer user ve role sınıflarını oluşturmasaydık IdentityDvContext içine IdentityUser ve IdentityRole yazacaktık. Yani bir sınıf oluşturduysak direkt sınıfın ismini, oluşturmadıysak Idetity li halini generic olarak ekliyoruz.
    //bu eklemden sonra veritabanında aspnetroles, aspnetuserclaims gibi tablolar oluşur. bir de aspnetuserroles tablosu oluşur. bu tablo userlar ve roller arasındaki çoka çok ilişkiyi sağlayan tablo.
    public class MiniShopDbContext:IdentityDbContext<User, Role,string>
    {
        public MiniShopDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //user ile role entitylerini eklememize gerek yok.



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);
            base.OnModelCreating(modelBuilder);
            //bazen ayrı ayı config dosyaları oluşturmak yerine ModelBuilder sınıfını extend ederek(yani modelbuilder. dediğimizde artık kendi oluşturduğumuz metodu çalıştırabiliyoruz(seedData metodu.) ) tüm seed dataları tek bir dosyada oluşturmak isteyebiliriz.bkz:data-extensions-modelbuilderextension
            //ve daha sonra oluşturduğumuz yeni metodu burada çağırmamız yeterli. 
            modelBuilder.SeedData();
        }
    }
}
