using GETTER.Application.Warehouses;
using GETTER.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace GETTER.Infrastructure.Database.Repositories;

internal sealed class WarehouseRepository : IWarehouseRepository
{
    private readonly AppDbContext _db;

    public WarehouseRepository(AppDbContext db)
    {
        _db = db;
    }
    public void Add(Warehouse warehouse)
    {
        _db.Warehouses.Add(warehouse);
    }

    public Task<bool> SymbolExistsAsync(WarehouseSymbol symbol, CancellationToken cancellationToken)
    {
        return _db.Warehouses.AnyAsync(w => w.Symbol == symbol, cancellationToken);
    }
}
