using GETTER.Domain.Common;

namespace GETTER.Domain.Warehouses;

public sealed class Warehouse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public WarehouseSymbol Symbol { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Address { get; private set; }
    public bool IsActive { get; private set; }
    public WarehouseType Type { get; private set; }

    private Warehouse() { }

    public static Warehouse Create(string name, string symbol, WarehouseType type, bool isActive, string? description = null, string? address = null)
    {
        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Symbol = WarehouseSymbol.Create(symbol),
            Type = type,
            IsActive = isActive
        };
        warehouse.UpdateDetails(description, address);
        warehouse.Rename(name);
        return warehouse;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void UpdateDetails(string? description, string? address)
    {
        description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        address = string.IsNullOrWhiteSpace(address) ? null : address.Trim();

        if (description?.Length > 200)
            throw new DomainException(WarehouseErrors.WarehouseDescriptionLength);

        if (address?.Length > 200)
            throw new DomainException(WarehouseErrors.WarehouseAddressLength);

        Description = description;
        Address = address;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(WarehouseErrors.WarehouseNameLength);

        name = name.Trim();

        if (name.Length is < 3 or > 50)
            throw new DomainException(WarehouseErrors.WarehouseNameLength);

        if (name.Contains("  "))
            throw new DomainException(WarehouseErrors.WarehouseNameDoubleSpaces);

        if (!name.All(c => char.IsLetterOrDigit(c) || c is ' ' or '-' or '_'))
            throw new DomainException(WarehouseErrors.WarehouseNameInvalidCharacters);

        Name = name;
    }
}
