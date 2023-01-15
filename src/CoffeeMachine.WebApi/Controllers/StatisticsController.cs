namespace CoffeeMachine.WebApi.Controllers;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Serilog;

/// <summary>
/// Контроллер для статистик.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatisticsController : ControllerBase
{
    /// <inheritdoc cref="IStatisticService" />
    private readonly IStatisticService _statisticService;

    /// <inheritdoc cref="StatisticsController" />
    /// <param name="statisticService"> Сервис для работы со статистиками. </param>
    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    /// <summary>
    /// Удалить статистику по идентификатору кофе.
    /// </summary>
    /// <param name="id"> Идентификатор кофе. </param>
    /// <response code="200"> Статистика удалена. </response>
    /// <response code="400"> Отправлены некорректные данные. </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteByCoffeeIdAsync([FromRoute] Guid id)
    {
        try
        {
            await _statisticService.DeleteStatisticByCoffeeIdAsync(id);

            Log.Information($"Статистика по идентификатору кофе {id} удалена.");

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Получить статистику по всем кофе.
    /// </summary>
    /// <returns> Список статистики по всем видам кофе. </returns>
    /// <response code="200"> Возвращает список всей статистики. </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<StatisticDto>>> GetAllAsync()
    {
        var statistics = await _statisticService.GetAllStatisticAsync();

        return Ok(statistics);
    }

    /// <summary>
    /// Получить статистику по кофе ID.
    /// </summary>
    /// <param name="id"> Идентификатор кофе для поиска статистики. </param>
    /// <returns> Статистика по одному виду кофе. </returns>
    /// <response code="200"> Возвращает найденную статистику по идентификатору кофе. </response>
    /// <response code="404"> Если статистика не найдена. </response>
    [HttpGet("coffee/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StatisticDto>> GetByCoffeeIdAsync([FromRoute] Guid id)
    {
        try
        {
            var foundStatistic = await _statisticService.GetStatisticByCoffeeIdAsync(id);

            return Ok(foundStatistic);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    /// <summary>
    /// Увеличение статистики по кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе для обновления статистики. </param>
    /// <response code="204"> Статистика успешно обновлена. </response>
    /// <response code="400"> Переданы некорректные данные. </response>
    /// <response code="404"> Кофе с заданным идентификатором не существует. </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> IncreaseStatisticForCoffeeAsync([FromQuery] Guid coffeeId)
    {
        try
        {
            await _statisticService.IncreaseStatisticByCoffeeIdAsync(coffeeId);

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