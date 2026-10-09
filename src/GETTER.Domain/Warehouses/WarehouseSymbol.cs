using GETTER.Domain.Common;

namespace GETTER.Domain.Warehouses;

public sealed record WarehouseSymbol
{
    public string Value { get; }

    private WarehouseSymbol(string value)
    {
        Value = value;
    }
    public static WarehouseSymbol Create(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new DomainException(WarehouseErrors.WarehouseSymbolLength);

        symbol = symbol.Trim();

        if (symbol.Length is < 2 or > 10)
            throw new DomainException(WarehouseErrors.WarehouseSymbolLength);

        if (!symbol.All(c => char.IsAsciiLetterOrDigit(c) || c == '-'))
            throw new DomainException(WarehouseErrors.WarehouseSymbolInvalidCharacters);

        return new WarehouseSymbol(symbol.ToUpperInvariant());
    }
}
