namespace GETTER.Domain.Common.ValueObjects
{
    public static class ValueObjectsErrors
    {
        // PHONE
        public static readonly Error PhoneLength =
        new("Phone.Length", "Phone must be between 9 and 16 characters long.");

        public static readonly Error PhoneInvalidCharacters =
            new("Phone.InvalidCharacters", "Phone contains invalid characters.");

        // EMAIL
        public static readonly Error EmailLength =
            new("Email.Length", "Email must be between 8 and 255 characters long.");

        public static readonly Error EmailInvalidCharacters =
            new("Email.InvalidCharacters", "Email contains invalid characters.");

        // ADDRESS
        public static readonly Error StreetLength =
            new("Street.Length", "Street must be between 5 and 255 characters long.");

        public static readonly Error CityLength =
            new("City.Length", "City must be between 3 and 100 characters long.");

        public static readonly Error PostalCodeLength =
            new("PostalCode.Length", "PostalCode must be between 3 and 30 characters long.");

        public static readonly Error PostalCodeInvalidCharacters =
            new("PostalCode.InvalidCharacters", "PostalCode contains invalid characters.");

        public static readonly Error CountryLength =
            new("Country.Length", "Country must be between 3 and 100 characters long.");

        public static readonly Error CountryCodeLength =
            new("CountryCode.Length", "CountryCode must be exactly 2 letters");

        public static readonly Error CountryCodeInvalid =
            new("CountryCode.Invalid", "CountryCode contains invalid characters.");

        // NIP
        public static readonly Error NipLength =
            new("Nip.Length", "Nip must be between 3 and 30 characters long.");

        public static readonly Error NipInvalidCharacters =
            new("Nip.InvalidCharacters", "Nip contains invalid characters.");
    }
}
