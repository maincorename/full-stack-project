namespace CoffeeMachine.Core.Services.Interfaces;

using System.Data;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;

/// <summary>
/// Сервис для работы со статистиками.
/// </summary>
public interface IStatisticService
{
    /// <summary>
    /// Получить все статистики.
    /// </summary>
    /// <returns> Перечисление всех статистик. </returns>
    public Task<IEnumerable<StatisticDto?>> GetAllStatisticAsync();

    /// <summary>
    /// Получить статистику по кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <returns> Дто статистики по кофе. </returns>
    public Task<StatisticDto?> GetStatisticByCoffeeIdAsync(Guid coffeeId);

    /// <summary>
    /// Обновить статистику по идентификатору кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    public Task IncreaseStatisticByCoffeeIdAsync(Guid coffeeId);

    /// <summary>
    /// Удалить статистику по идентификатору кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <returns> true - объект удалён. </returns>
    public Task<bool> DeleteStatisticByCoffeeIdAsync(Guid coffeeId);
}