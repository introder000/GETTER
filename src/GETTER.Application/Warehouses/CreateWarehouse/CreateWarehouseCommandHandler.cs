using GETTER.Application.Common;
using GETTER.Domain.Common;
using GETTER.Domain.Warehouses;

namespace GETTER.Application.Warehouses.CreateWarehouse;

public sealed class CreateWarehouseCommandHandler
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            WarehouseSymbol symbol = WarehouseSymbol.Create(command.Symbol);

            bool exists = await _warehouseRepository.SymbolExistsAsync(symbol, cancellationToken);

            if (exists) {
                return Result<Guid>.Failure(WarehouseErrors.WarehouseSymbolAlreadyExists);
            }

            var warehouse = Warehouse.Create(command.Name, symbol, command.Type, command.IsActive, command.Description, command.Address);

            _warehouseRepository.Add(warehouse);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(warehouse.Id);
        }
        catch (DomainException ex)
        {
            Result<Guid> res = Result<Guid>.Failure(ex.Error);
            return res;
        }
    }
}