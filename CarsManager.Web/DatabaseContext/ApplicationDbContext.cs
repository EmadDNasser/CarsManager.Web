using CarsManager.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        public virtual DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
             new Car() { Id = 1, Make="Toyouta", Model = "Camry", Year = 2024});

            modelBuilder.Entity<Car>().HasData(
             new Car() { Id = 2, Make = "Honda", Model = "Civic", Year = 2022 });
        }
    }
}
