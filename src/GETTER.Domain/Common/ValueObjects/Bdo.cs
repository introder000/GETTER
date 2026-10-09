namespace GETTER.Domain.Common.ValueObjects;

public sealed record Bdo
{
    public string Value { get; }

    private Bdo(string value)
    {
        Value = value;
    }

    public static Bdo Create(string value)
    {
        return new(value);
    }
}
