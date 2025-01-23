using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Entities;


namespace SalesDatePrediction.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers", "Sales");
            entity.HasKey(c => c.Custid);
            entity.Property(c => c.Companyname)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(c => c.Orders)
                .WithOne(o => o.Cust)
                .HasForeignKey(o => o.Custid);
        });

        modelBuilder.Entity<Order>(entity =>
       {
           entity.ToTable("Orders", "Sales");
           entity.HasKey(o => o.Orderid);
           entity.Property(o => o.Orderdate).HasColumnType("datetime");
           entity.Property(o => o.Freight).HasColumnType("money");

           entity.HasOne(o => o.Cust)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.Custid)
               .OnDelete(DeleteBehavior.Restrict);
       });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetails", "Sales"); 
            entity.HasKey(od => new { od.Orderid, od.Productid });
            entity.Property(od => od.Unitprice).HasColumnType("money");
            entity.Property(od => od.Discount).HasColumnType("numeric(4, 3)");

            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.Orderid);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products", "Production"); 
            entity.HasKey(p => p.Productid);
            entity.Property(p => p.Productname)
                .IsRequired()
                .HasMaxLength(40);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees", "Hr");
            entity.HasKey(e => e.Empid);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
        });
        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.ToTable("Shippers", "Sales");
            entity.HasKey(s => s.Shipperid);
            entity.Property(s => s.Companyname)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
