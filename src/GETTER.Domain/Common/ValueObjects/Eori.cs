namespace GETTER.Domain.Common.ValueObjects;

public sealed record Eori
{
    public string Value { get; }

    private Eori(string value)
    {
        Value = value;
    }

    public static Eori Create(string value)
    {
        return new(value);
    }
}
