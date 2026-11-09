using GETTER.Application.Common;
using GETTER.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace GETTER.Infrastructure.Database;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Warehouse> Warehouses
    {
        get { return Set<Warehouse>(); }
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
