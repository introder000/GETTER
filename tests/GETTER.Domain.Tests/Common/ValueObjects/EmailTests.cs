using GETTER.Domain.Common;
using GETTER.Domain.Common.ValueObjects;

namespace GETTER.Domain.Tests.Common.ValueObjects;

public class EmailTests
{
    public static TheoryData<string, string> InvalidEmail => new()
    {
        { "", ValueObjectsErrors.EmailLength.Code },
        { "     ", ValueObjectsErrors.EmailLength.Code },
        { "a@b", ValueObjectsErrors.EmailLength.Code },
        { new string('a', 250) + "@firma.pl", ValueObjectsErrors.EmailLength.Code },
        { "janfirma.pl", ValueObjectsErrors.EmailInvalidCharacters.Code },
        { "@firma.pl", ValueObjectsErrors.EmailInvalidCharacters.Code },
        { "ab@cd", ValueObjectsErrors.EmailInvalidCharacters.Code },
        { "jan kowalski@firma.pl", ValueObjectsErrors.EmailInvalidCharacters.Code },
        { "Jan <jan@firma.pl>", ValueObjectsErrors.EmailInvalidCharacters.Code },
        { "jan\U0001F600@firma.pl", ValueObjectsErrors.EmailInvalidCharacters.Code },
    };

    [Theory]
    [MemberData(nameof(InvalidEmail))]
    public void Create_WithInvalidEmail_ShouldThrowExpectedError(string input, string expected)
    {
        Email act() => Email.Create(input);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(expected, ex.Error.Code);
    }

    [Fact]
    public void Create_WithNull_ShouldThrowEmailLength()
    {
        Email act() => Email.Create(null);

        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal(ValueObjectsErrors.EmailLength.Code, ex.Error.Code);
    }

    [Theory]
    [InlineData("jan@firma.pl", "jan@firma.pl")]
    [InlineData("  JAN@Firma.PL  ", "jan@firma.pl")]
    [InlineData("jan.kowalski+faktury@firma.com.pl", "jan.kowalski+faktury@firma.com.pl")]
    [InlineData("a@b.co", "a@b.co")]
    public void Create_WithValidEmail_ShouldNormalize(string input, string expected)
    {
        var email = Email.Create(input);

        Assert.Equal(expected, email.Value);
    }
}
