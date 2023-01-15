namespace CoffeeMachine.WebApi.Controllers;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Serilog;

/// <summary>
/// Контроллер для покупок.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchasesController : ControllerBase
{
    /// <inheritdoc cref="IPurchaseService" />
    private readonly IPurchaseService _purchaseService;

    /// <inheritdoc cref="PurchasesController" />
    /// <param name="purchaseService"> Сервис для работы с покупками. </param>
    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    /// <summary>
    /// Купить кофе.
    /// </summary>
    /// <param name="id"> Идентификатор кофе для покупки. </param>
    /// <param name="money"> Средства для покупки. </param>
    /// <response code="200"> Покупка успешно совершена. </response>
    /// <response code="402"> Если недостаточно средств. </response>
    /// <response code="400"> Если переданы некорректные данные. </response>
    /// <response code="404"> Если кофе не найдено. </response>
    [HttpPut("coffee/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PurchaseCoffeeDto>> BuyCoffeeAsync([FromRoute] Guid id, [FromQuery] int money)
    {
        try
        {
            var purchaseResult = await _purchaseService.BuyCoffeeAsync(id, money);

            Log.Information($"Покупка кофе {id} успешно произведена.");

            return Ok(purchaseResult);
        }
        catch (NotEnoughMoneyException exception)
        {
            return StatusCode(402, exception.Message);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (ArgumentNullException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Удалить все покупки по идентификатору кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <response code="204"> Покупки удалены. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpDelete("all/{coffeeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAllPurchasesByCoffeeId([FromRoute] Guid coffeeId)
    {
        try
        {
            await _purchaseService.DeleteAllPurchasesByCoffeeIdAsync(coffeeId);

            Log.Information($"Все покупки по идентификатору кофе {coffeeId} удалены.");

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Удалить покупку по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор покупки. </param>
    /// <response code="204"> Покупка удалена. </response>
    /// <response code="404"> Покупка не найдена. </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePurchaseById([FromRoute] Guid id)
    {
        try
        {
            await _purchaseService.DeletePurchaseByIdAsync(id);

            Log.Information($"Покупка {id} удалена.");

            return NoContent();
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Получить список всех покупок.
    /// </summary>
    /// <returns> Список покупок. </returns>
    /// <response code="200"> Список покупок найден. </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<PurchaseDto>>> GetAllAsync()
    {
        var purchases = await _purchaseService.GetAllPurchasesAsync();

        return Ok(purchases);
    }

    /// <summary>
    /// Получить покупку по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор покупки. </param>
    /// <returns> Найденный экземпляр покупки. </returns>
    /// <response code="200"> Возвращает найденную покупку. </response>
    /// <response code="404"> Если покупка не найдена. </response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById([FromRoute] Guid id)
    {
        try
        {
            var foundPurchase = await _purchaseService.GetPurchaseByIdAsync(id);

            return Ok(foundPurchase);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}