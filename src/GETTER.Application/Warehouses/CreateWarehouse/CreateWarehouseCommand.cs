using GETTER.Domain.Warehouses;

namespace GETTER.Application.Warehouses.CreateWarehouse;

public sealed record CreateWarehouseCommand
{
    public string? Name { get; init; }
    public string? Symbol { get; init; }
    public WarehouseType Type { get; init; }
    public bool IsActive { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
}


// skrocona wersja 
//public sealed record CreateWarehouseCommand2(
//    string? Name,
//    string? Symbol,
//    WarehouseType Type,
//    bool IsActive,
//    string? Description = null,
//    string? Address = null) : ICommand<Guid>;