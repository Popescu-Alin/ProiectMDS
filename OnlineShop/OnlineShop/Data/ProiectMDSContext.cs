using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server;
using inceputproiectMds.Models.Entities;

namespace OnlineShop.Data
{
    public class ProiectMDSContext : DbContext
    {
        public ProiectMDSContext()
        { }

        public ProiectMDSContext(DbContextOptions<ProiectMDSContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Database = OnlineShopDB; Trusted_Connection = True; MultipleActiveResultSets = true");
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EasyBox> Easyboxes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserCard> UserCards { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // definire primary key compus
            modelBuilder.Entity<Cart>()
            .HasKey(cp => new
            {
                cp.ProductId,
                cp.UserId
            });
            modelBuilder.Entity<Cart>()
            .HasOne(ab => ab.Product)
            .WithMany(ab => ab.Carts)
            .HasForeignKey(ab => ab.ProductId);
            modelBuilder.Entity<Cart>()
            .HasOne(ab => ab.User)
            .WithMany(ab => ab.Carts)
            .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<Review>()
            .HasOne(ab => ab.Product)
            .WithMany(ab => ab.Reviews)
            .HasForeignKey(ab => ab.ProductId);
            modelBuilder.Entity<Review>()
            .HasOne(ab => ab.User)
            .WithMany(ab => ab.Reviews)
            .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<ProductOrder>()
             .HasKey(cp => new
             {
                 cp.ProductId,
                 cp.OrderId
             });
            modelBuilder.Entity<ProductOrder>()
            .HasOne(ab => ab.Product)
            .WithMany(ab => ab.Orders)
            .HasForeignKey(ab => ab.ProductId);
            modelBuilder.Entity<ProductOrder>()
            .HasOne(ab => ab.Order)
            .WithMany(ab => ab.ProductOrders)
            .HasForeignKey(ab => ab.OrderId);

            modelBuilder.Entity<UserAddress>()
             .HasKey(cp => new
             {
                 cp.UserId,
                 cp.AddressId
             });
            modelBuilder.Entity<UserAddress>()
            .HasOne(ab => ab.User)
            .WithMany(ab => ab.UserAddresses)
            .HasForeignKey(ab => ab.UserId);
            modelBuilder.Entity<UserAddress>()
            .HasOne(ab => ab.Address)
            .WithMany(ab => ab.UserAddresses)
            .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<UserCard>()
             .HasKey(cp => new
             {
                 cp.UserId,
                 cp.CardId
             });
            modelBuilder.Entity<UserCard>()
            .HasOne(ab => ab.User)
            .WithMany(ab => ab.UserCards)
            .HasForeignKey(ab => ab.UserId);
            modelBuilder.Entity<UserCard>()
            .HasOne(ab => ab.Card)
            .WithMany(ab => ab.UserCards)
            .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<UserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });
        }

    }
}
