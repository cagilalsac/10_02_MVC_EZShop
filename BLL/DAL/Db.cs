using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductStore> ProductStores { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Store> Stores { get; set; }

    public DbSet<User> Users { get; set; }
}