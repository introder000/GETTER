using System.Net.Mail;

namespace GETTER.Domain.Common.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string? value)
    {
        value = value?.Trim().ToLowerInvariant();

        if (string.IsNullOrEmpty(value) || value.Length is < 5 or > 255)
            throw new DomainException(ValueObjectsErrors.EmailLength);

        if (!value.All(char.IsAscii)
            || !MailAddress.TryCreate(value, out var parsed)
            || parsed.Address != value
            || !parsed.Host.Contains('.'))
            throw new DomainException(ValueObjectsErrors.EmailInvalidCharacters);

        return new Email(value);
    }
}
