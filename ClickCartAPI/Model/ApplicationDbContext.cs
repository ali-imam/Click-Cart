using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, UserName = "Wasi", Password = "wasi#Passorwd90", Email = "mohddwasi@gmail.com", Phone = "8692051488", Role = "User" },
            new User { UserId = 2, UserName = "Ali", Password = "ali#Passorwd91", Email = "aliimam@gmail.com", Phone = "8692051488", Role = "Admin" }
            );

            modelBuilder.Entity<Product>().HasData(
                new { ProductId = 1, CategoryId = 1, ProductName = "watch", Description = "ifhiehfoiwehf", Price = "2200", StockQuantity = 20, ProductImg = "/images/products/first.webp" }
                );

            //modelBuilder.Entity<Order>().HasData(
            //    new { OrderId = 1, UserId = 1, ProductId = 1, Quantity = 2, Status = "Shipped", OrderCode = "ASDFFFF" }
            //    );
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, CategoryName = "Fashion" }
                );
            modelBuilder.Entity<Address>().HasData(
                new { AddressId = 1, UserId = 1, Street = "Colaba", City = "Mumbai", State = "Maharashtra", ZipCode = 400614, Country = "India" }
                );
            modelBuilder.Entity<Cart>().HasData(
                new { CartId = 1, UserId = 2, ProductId = 1, ProductName = "Watch", Price = "2200", Quantity = 2, ProductImg = "/images/products/first.webp" }
                );
        }
    }
}
