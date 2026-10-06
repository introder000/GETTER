using GETTER.Domain.Common;
using GETTER.Domain.Warehouses;

namespace GETTER.Domain.Tests.Warehouses
{
    public class WarehouseSymbolTests
    {
        public static IEnumerable<object[]> InvalidSymbol =>
        [
            ["w", WarehouseErrors.WarehouseSymbolLength],
            ["ABCDEFGHIJK", WarehouseErrors.WarehouseSymbolLength],   // 11 znaków
            ["WRÓ-01", WarehouseErrors.WarehouseSymbolInvalidCharacters],       // polska litera
            ["WRO 01", WarehouseErrors.WarehouseSymbolInvalidCharacters],        // spacja
            ["", WarehouseErrors.WarehouseSymbolLength],
            [" ", WarehouseErrors.WarehouseSymbolLength],
            ["  ", WarehouseErrors.WarehouseSymbolLength]
        ];
        [Theory]
        [MemberData(nameof(InvalidSymbol))]
        public void Create_WithInvalidSymbol_ShouldThrowDomainException(string symbol, Error expected)
        {
            WarehouseSymbol act() => WarehouseSymbol.Create(symbol);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(expected.Code, ex.Error.Code);
        }

        [Theory]
        [InlineData("ab", "AB")]
        [InlineData("AB", "AB")]
        [InlineData("ABCDEFGHIJ", "ABCDEFGHIJ")]
        [InlineData("wro-01", "WRO-01")]
        [InlineData("  wro-01  ", "WRO-01")]
        public void Create_WithValidSymbol_ShouldNormalizeSymbol(string input, string expected)
        {
            var warehouse = Warehouse.Create("Testowy", input, WarehouseType.Physical, isActive: true);

            Assert.Equal(expected, warehouse.Symbol.Value);
        }
    }
}
