using GETTER.Domain.Common;
using GETTER.Domain.Common.ValueObjects;

namespace GETTER.Domain.Tests.Common.ValueObjects;

public class PhoneTests
{
    public static TheoryData<string, string> InvalidPhone => new()
    {
        { "w", ValueObjectsErrors.PhoneInvalidCharacters.Code },
        { "", ValueObjectsErrors.PhoneLength.Code },
        { "    ", ValueObjectsErrors.PhoneLength.Code },
        { "123", ValueObjectsErrors.PhoneLength.Code },
        { "7863      312", ValueObjectsErrors.PhoneLength.Code },
        { "786387+786", ValueObjectsErrors.PhoneInvalidCharacters.Code },
        { "😀", ValueObjectsErrors.PhoneLength.Code },
        { "12345678", ValueObjectsErrors.PhoneLength.Code },
        { "12345678901234567", ValueObjectsErrors.PhoneLength.Code },
        { "++48123456789", ValueObjectsErrors.PhoneInvalidCharacters.Code },
        { "78623a312", ValueObjectsErrors.PhoneInvalidCharacters.Code },
        { "((((((((", ValueObjectsErrors.PhoneLength.Code },
    };

    [Theory]
    [MemberData(nameof(InvalidPhone))]
    public void Create_WithInvalidPhone_ShouldThrowDomainException(string input, string expected)
    {
        Phone act() => Phone.Create(input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Theory]
    [InlineData("  786233312  ", "786233312")]
    [InlineData("+1 (555) 555-1234", "+15555551234")]
    [InlineData("786233    312", "786233312")]
    [InlineData("+481234567890123", "+481234567890123")]
    public void Create_WithValidPhone_ShouldNormalize(string input, string expected)
    {
        var phone = Phone.Create(input);

        Assert.Equal(expected, phone.Value);
    }
}
