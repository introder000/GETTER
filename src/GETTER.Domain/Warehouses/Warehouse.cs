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

    private Warehouse(Guid id, string name, WarehouseSymbol symbol, WarehouseType type, bool isActive, string? description, string? address)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
        Type = type;
        IsActive = isActive;
        Description = description;
        Address = address;
    }

    public static Warehouse Create(string name, WarehouseSymbol symbol, WarehouseType type, bool isActive, string? description = null, string? address = null)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        return new Warehouse(
            Guid.NewGuid(),
            ValidateName(name),
            symbol,
            type,
            isActive,
            ValidateDescription(description),
            ValidateAddress(address)
            );
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    private static string ValidateName(string? name)
    {
        name = StringNormalizer.Normalize(name) ?? throw new DomainException(WarehouseErrors.WarehouseNameLength);

        if (name.Length is < 3 or > 50)
            throw new DomainException(WarehouseErrors.WarehouseNameLength);

        if (!name.All(c => char.IsLetterOrDigit(c) || c is ' ' or '-' or '_'))
            throw new DomainException(WarehouseErrors.WarehouseNameInvalidCharacters);

        return name;
    }
    private static string? ValidateDescription(string? description)
    {
        description = StringNormalizer.Normalize(description);

        if (description is null)
            return null;

        if (description.Length > 200)
            throw new DomainException(WarehouseErrors.WarehouseDescriptionLength);

        return description;
    }

    private static string? ValidateAddress(string? address)
    {
        address = StringNormalizer.Normalize(address);

        if (address is null)
            return null;

        if (address.Length > 200)
            throw new DomainException(WarehouseErrors.WarehouseAddressLength);

        return address;
    }
}
