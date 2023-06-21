using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sigma.Models;
using System.Reflection.Emit;

namespace Sigma.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Product> Products {get; set;}
        public DbSet<ApplicationUser> ApplicationUsers {get; set;}
        public DbSet<ShoppingCart> ShoppingCarts {get; set;}
        public DbSet<OrderHeader> OrderHeaders {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);


			// FLUENT API AS AN ALTERNATIVE TO DATA ANNOTATIONS
			// FLUENT API TAKES PRECEDENCE OVER DATA ANNOTATIONS
			/*
			
			modelBuilder.Entity<Product>().HasKey(x => x.Id);
			modelBuilder.Entity<Product>().Property(x => x.Title).IsRequired();
			modelBuilder.Entity<Product>().Property(x => x.Description);
			modelBuilder.Entity<Product>().Property(x => x.Author).IsRequired();
			modelBuilder.Entity<Product>().Property(e => e.Price).IsRequired()
			.HasColumnName("Price").HasAnnotation("DisplayName", "Price")
			.HasAnnotation("Range", new[] { 1, 10000 });
			modelBuilder.Entity<Product>().Property(x => x.ImageUrl).IsRequired();

			modelBuilder.Entity<ApplicationUser>().Property(x => x.Name).IsRequired();
			modelBuilder.Entity<ApplicationUser>().Property(x => x.StreetAddress);
			modelBuilder.Entity<ApplicationUser>().Property(x => x.City);
			modelBuilder.Entity<ApplicationUser>().Property(x => x.State);
			modelBuilder.Entity<ApplicationUser>().Property(x => x.PostalCode);


			modelBuilder.Entity<ShoppingCart>().HasKey(x => x.Id);
			modelBuilder.Entity<ShoppingCart>().Property(x => x.ProductId).IsRequired();
			modelBuilder.Entity<ShoppingCart>().Property(x => x.Count).IsRequired()
                .HasAnnotation("Range", new RangeAttribute(1, 1000).ToString());
			modelBuilder.Entity<ShoppingCart>().Property(x => x.ApplicationUserId).IsRequired();
			modelBuilder.Entity<ShoppingCart>().Ignore(x => x.Price);        

			modelBuilder.Entity<ShoppingCart>().HasOne(e => e.Product)
				.WithOne()
				.HasForeignKey(e => e.ProductId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired()
				.HasConstraintName("FK_ShoppingCart_Product");
	       modelBuilder.Entity<ShoppingCart>().HasOne(e => e.ApplicationUser)
				.WithOne()
				.HasForeignKey(e => e.ApplicationUserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired()
				.HasConstraintName("FK_ShoppingCart_ApplicationUser");
			
			modelBuilder.Entity<OrderHeader>().HasKey(x => x.Id);
			modelBuilder.Entity<OrderHeader>().Property(x => x.ApplicationUserId).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.OrderDate).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.ShippingDate);
			modelBuilder.Entity<OrderHeader>().Property(x => x.OrderTotal);
			modelBuilder.Entity<OrderHeader>().Property(x => x.OrderStatus);
			modelBuilder.Entity<OrderHeader>().Property(x => x.PaymentStatus);
			modelBuilder.Entity<OrderHeader>().Property(x => x.TrackingNumber);
			modelBuilder.Entity<OrderHeader>().Property(x => x.PaymentDate);
			modelBuilder.Entity<OrderHeader>().Property(x => x.PaymentDueDate);
			modelBuilder.Entity<OrderHeader>().Property(x => x.SessionId);
			modelBuilder.Entity<OrderHeader>().Property(x => x.PaymentIntentId);
			modelBuilder.Entity<OrderHeader>().Property(x => x.PhoneNumber).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.StreetAddress).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.City).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.State).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.PostalCode).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.Name).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.Carrier).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.State).IsRequired();
			modelBuilder.Entity<OrderHeader>().Property(x => x.PostalCode).IsRequired();

	        modelBuilder.Entity<OrderHeader>().HasOne(e => e.ApplicationUser)
            .WithOne()
            .HasForeignKey<OrderHeader>(e => e.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName("FK_OrderHeader_ApplicationUser");


			base.OnModelCreating(modelBuilder); 
			 */



		}
	}
}