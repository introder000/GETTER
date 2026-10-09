using GETTER.Domain.Common;

namespace GETTER.Domain.Warehouses
{
    public static class WarehouseErrors
    {
        //Warehouse name
        public static readonly Error WarehouseNameLength =
        new("Warehouse.Name.Length", "Warehouse name must be between 3 and 50 characters long.");

        public static readonly Error WarehouseNameDoubleSpaces =
        new("Warehouse.Name.DoubleSpaces", "Warehouse name cannot contain double spaces.");

        public static readonly Error WarehouseNameInvalidCharacters =
        new("Warehouse.Name.InvalidCharacters", "Warehouse name can only contain letters, digits, spaces, hyphens, and underscores.");


        //Warehouse symbol
        public static readonly Error WarehouseSymbolLength =
        new("Warehouse.Symbol.Length", "Warehouse symbol must be between 2 and 10 characters long.");

        public static readonly Error WarehouseSymbolInvalidCharacters =
        new("Warehouse.Symbol.InvalidCharacters", "Warehouse symbol can only contain letters, digits, and hyphens.");


        //Warehouse description
        public static readonly Error WarehouseDescriptionLength =
        new("Warehouse.Description.Length", "Warehouse description cannot exceed 200 characters.");


        //Warehouse address
        public static readonly Error WarehouseAddressLength =
        new("Warehouse.Address.Length", "Warehouse address cannot exceed 200 characters.");

    }
}
