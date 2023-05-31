// using Sigma.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sigma.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        // public DbSet<Product> Products {get; set;}
        // public DbSet<ApplicationUser> ApplicationUsers {get; set;}
        // public DbSet<ShoppingCart> ShoppingCarts {get; set;}
        // public DbSet<OrderHeader> OrderHeaders {get; set;}


    }
}