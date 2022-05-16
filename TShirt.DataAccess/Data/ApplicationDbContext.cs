using TShirt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TShirt.DataAccess;

    public class ApplicationDbContext : IdentityDbContext
{
        //configure RDB context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // Push our Models to the DB - Categories, DesignTypes will be the name of the DB that will be created

        // Have to create a dbset called Category inside DB:
        //Create category table with the name of Categories, and will have 4 columns we wrote inside category model
        // Code first
        public DbSet<Category> Categories { get; set; }
        public DbSet<DesignType> DesignTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }

}
