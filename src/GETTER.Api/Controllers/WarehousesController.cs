using GETTER.Application.Warehouses.CreateWarehouse;
using GETTER.Domain.Warehouses;
using Microsoft.AspNetCore.Mvc;

namespace GETTER.Api.Controllers;

[ApiController]
[Route("api/warehouses")]
public sealed class WarehousesController : ControllerBase
{
    private readonly CreateWarehouseCommandHandler _createWarehouseHandler;

    public WarehousesController(CreateWarehouseCommandHandler createWarehouseHandler)
    {
        _createWarehouseHandler = createWarehouseHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var result = await _createWarehouseHandler.Handle(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(new { id = result.Value });

        int status = result.Error!.Code == WarehouseErrors.WarehouseSymbolAlreadyExists.Code
            ? StatusCodes.Status409Conflict
            : StatusCodes.Status400BadRequest;

        return Problem(statusCode: status, title: result.Error.Code, detail: result.Error.Message);
    }
}
