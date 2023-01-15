namespace CoffeeMachine.WebApi.Controllers;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Serilog;

/// <summary>
/// Контроллер для кофе.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CoffeesController : Controller
{
    /// <inheritdoc cref="ICoffeeService" />
    private readonly ICoffeeService _coffeeService;

    /// <inheritdoc cref="CoffeesController" />
    /// <param name="coffeeService"> Сервис для работы кофе. </param>
    public CoffeesController(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    /// <summary>
    /// Добавить кофе.
    /// </summary>
    /// <param name="coffeeDto"> Дто кофе. </param>
    /// <returns> Добавленный экземпляр кофе. </returns>
    /// <response code="201"> Кофе успешно добавлен. </response>
    /// <response code="400"> Переданы некорректные данные. </response>
    /// <response code="400"> Кофе с таким именем уже существует. </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CoffeeDto>> AddCoffee([FromBody] CoffeeDto coffeeDto)
    {
        try
        {
            var addedCoffee = await _coffeeService.AddCoffeeAsync(coffeeDto);

            Log.Information($"Добавлено новое кофе {addedCoffee.Name}.");

            return CreatedAtAction("GetCoffeeById", new { id = addedCoffee.Id }, addedCoffee);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Удалить кофе.
    /// </summary>
    /// <param name="id"> Идентификатор кофе для удаления. </param>
    /// <response code="204"> Кофе успешно удалено. </response>
    /// <response code="404"> Неверно указан идентификатор. Объект для удаления не найден. </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCoffeeById([FromRoute] Guid id)
    {
        try
        {
            await _coffeeService.DeleteCoffeeByIdAsync(id);

            Log.Information($"Кофе с идентификатором {id} успешно удалено.");

            return NoContent();
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Получить кофе по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор кофе. </param>
    /// <returns> Найденный экземпляр кофе. </returns>
    /// <response code="200"> Возвращает найденный кофе. </response>
    /// <response code="404"> Если кофе не найдено. </response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CoffeeDto>> GetCoffeeById([FromRoute] Guid id)
    {
        try
        {
            var coffeeDto = await _coffeeService.GetCoffeeByIdAsync(id);

            return Ok(coffeeDto);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Получение всех видов кофе.
    /// </summary>
    /// <returns> Список кофе. </returns>
    /// <response code="200"> Возвращает список всех видов кофе. </response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CoffeeDto>>> ShowCoffees()
    {
        var coffeesDto = await _coffeeService.GetAllCoffeesAsync();

        return Ok(coffeesDto);
    }

    /// <summary>
    /// Обновить кофе.
    /// </summary>
    /// <param name="updatedCoffeeDto"> Дто кофе. </param>
    /// <returns> Дто созданного кофе. </returns>
    /// <response code="204"> Кофе успешно обновлено. </response>
    /// <response code="400"> Переданы некорректные данные. </response>
    /// <response code="404"> Кофе для обновления не найдено. </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateCoffee([FromBody] CoffeeDto updatedCoffeeDto)
    {
        try
        {
            await _coffeeService.UpdateCoffeeAsync(updatedCoffeeDto);

            Log.Information($"Кофе с идентификатором {updatedCoffeeDto.Id} успешно обновлено.");

            return NoContent();
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
}