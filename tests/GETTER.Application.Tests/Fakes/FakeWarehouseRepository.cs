using GETTER.Application.Warehouses;
using GETTER.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GETTER.Application.Tests.Fakes;

public sealed class FakeWarehouseRepository : IWarehouseRepository
{
    public List<Warehouse> Items { get; } = new();

    public void Add(Warehouse warehouse)
    {
        Items.Add(warehouse);
    }
    public Task<bool> SymbolExistsAsync(WarehouseSymbol symbol, CancellationToken cancellationToken)
    {
        return Items.Any(w => w.Symbol == symbol) ? Task.FromResult(true) : Task.FromResult(false);
    }
}
