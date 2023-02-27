using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ScientificMagazine.Models;

namespace ScientificMagazine.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Archive> Archives { get; set; }
        
        public DbSet<Status> Statuses { get; set; }
    }

}
