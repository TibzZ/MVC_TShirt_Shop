using TShirt.Models;
using Microsoft.EntityFrameworkCore;

namespace TShirt.DataAccess;

    public class ApplicationDbContext : DbContext
    {
        //configure RDB context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Have to create a dbset called Category inside DB:
        //Create category table with the name of Categories, and will have 4 columns we wrote inside category model
        // Code first
        public DbSet<Category> Categories { get; set; }

    }
