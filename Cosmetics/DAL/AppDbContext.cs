using Cosmetics.Models; // Assuming you have your own application-specific models
using Cosmetics.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Header> Headers { get; set; }
        
        public DbSet<Basket> Baskets { get; set; }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductWholeLook> ProductWholeLook { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BasketProduct>  BasketProducts { get; set; }
        public DbSet<About> About { get; set; }

        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<AboutPhoto> AboutPhotos { get; set; }

        public DbSet<Icon> Icons { get; set; }
        public DbSet<FeaturedProduct> FeaturedProducts { get; set; }
        public DbSet<MakeupFooter> MakeupFooter { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<TestimonalBackground> TestimonalBackground { get; set; }

        public DbSet<TestimonalImageVM> TestimonalImages { get; set; }

        public DbSet<Testimonals> Testimonals { get; set; }

        public DbSet<TestimonialMessage> TestimonialMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.AppUser)
                .WithOne(u => u.Basket)
                .HasForeignKey<AppUser>(u => u.BasketId); 
        }
    }
}
