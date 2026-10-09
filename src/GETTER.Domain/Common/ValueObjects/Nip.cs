namespace GETTER.Domain.Common.ValueObjects;

public sealed record Nip
{
    public string Number { get; }
    public string Prefix { get; }

    private Nip(string number, string prefix)
    {
        Number = number;
        Prefix = prefix;
    }
    public static Nip Create(string number, string prefix)
    {
        ArgumentNullException.ThrowIfNull(prefix);

        if (string.IsNullOrWhiteSpace(number))
            throw new DomainException(ValueObjectsErrors.NipLength);

        number = number.Trim();

        if (number.Length is < 3 or > 30)
            throw new DomainException(ValueObjectsErrors.NipLength);

        if (number.Any(c => !char.IsLetterOrDigit(c) && c != '-' && c != ' '))
            throw new DomainException(ValueObjectsErrors.NipInvalidCharacters);

        return new Nip(number, prefix);
    }
}