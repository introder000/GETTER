using GETTER.Domain.Warehouses;

namespace GETTER.Application.Warehouses;

public interface IWarehouseRepository
{
    Task<bool> SymbolExistsAsync(WarehouseSymbol symbol, CancellationToken cancellationToken);
    void Add(Warehouse warehouse);
}
