using Microsoft.EntityFrameworkCore;
using BangazonAPI.Models;

namespace BangazonAPI.Data
{
    public class BangazonContext : DbContext
    {
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }

        public DbSet<Computer> Computer { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeesComputers> EmployeesComputers { get; set; }
        public DbSet<EmployeeTrainings> EmployeeTrainings { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Order> Order { get; set; }
<<<<<<< Updated upstream
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Customers> Customers { get; set; }
=======
        public DbSet<OrderByProduc> OrderProduct { get; set; }
        public DbSet<PaymentTypes> PaymentTypes { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<TrainingProgram> TrainingProgram { get; set; }
>>>>>>> Stashed changes

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }
    }
}
