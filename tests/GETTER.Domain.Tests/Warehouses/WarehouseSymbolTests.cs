using GETTER.Domain.Common;
using GETTER.Domain.Warehouses;

namespace GETTER.Domain.Tests.Warehouses
{
    public class WarehouseSymbolTests
    {
        public static TheoryData<string, string> InvalidSymbol => new()
        {
            { "w", WarehouseErrors.WarehouseSymbolLength.Code },
            { "ABCDEFGHIJK", WarehouseErrors.WarehouseSymbolLength.Code },
            { "WRÓ-01", WarehouseErrors.WarehouseSymbolInvalidCharacters.Code },
            { "WRO 01", WarehouseErrors.WarehouseSymbolInvalidCharacters.Code },
            { "", WarehouseErrors.WarehouseSymbolLength.Code },
            { " ", WarehouseErrors.WarehouseSymbolLength.Code },
            { "  ", WarehouseErrors.WarehouseSymbolLength.Code }
        };

        [Theory]
        [MemberData(nameof(InvalidSymbol))]
        public void Create_WithInvalidSymbol_ShouldThrowDomainException(string input, string expected)
        {
            WarehouseSymbol act() => WarehouseSymbol.Create(input);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(expected, ex.Error.Code);
        }

        [Theory]
        [InlineData("ab", "AB")]
        [InlineData("AB", "AB")]
        [InlineData("ABCDEFGHIJ", "ABCDEFGHIJ")]
        [InlineData("wro-01", "WRO-01")]
        [InlineData("  wro-01  ", "WRO-01")]
        public void Create_WithValidSymbol_ShouldNormalizeSymbol(string input, string expected)
        {
            var symbol = WarehouseSymbol.Create(input);

            Assert.Equal(expected, symbol.Value);
        }
    }
}
