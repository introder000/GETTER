namespace GETTER.Domain.Common.ValueObjects;

public sealed record Regon
{
    public string Value { get; }

    private Regon(string value)
    {
        Value = value;
    }

    public static Regon Create(string value)
    {
        return new(value);
    }
}

