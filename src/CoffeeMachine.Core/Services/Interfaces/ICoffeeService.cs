namespace CoffeeMachine.Core.Services.Interfaces;

using CoffeeMachine.Core.Dtos;

/// <summary>
/// Сервис для работы с кофе.
/// </summary>
public interface ICoffeeService
{
    /// <summary>
    /// Добавить кофе.
    /// </summary>
    /// <param name="coffeeDto"> Дто кофе. </param>
    /// <returns> Дто добавленного кофе. </returns>
    public Task<CoffeeDto> AddCoffeeAsync(CoffeeDto coffeeDto);

    /// <summary>
    /// Удалить кофе по идентификатору.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <returns> true - объект удалён. </returns>
    public Task<bool> DeleteCoffeeByIdAsync(Guid coffeeId);

    /// <summary>
    /// Получить все виды кофе.
    /// </summary>
    /// <returns> Перечисление всех видов кофе. </returns>
    public Task<IEnumerable<CoffeeDto?>> GetAllCoffeesAsync();

    /// <summary>
    /// Получить кофе по идентификатору.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <returns> Объект кофе. </returns>
    public Task<CoffeeDto?> GetCoffeeByIdAsync(Guid coffeeId);

    /// <summary>
    /// Обновить кофе.
    /// </summary>
    /// <param name="coffeeDto"> Дто кофе. </param>
    /// <returns> Обновленный дто кофе. </returns>
    public Task<CoffeeDto?> UpdateCoffeeAsync(CoffeeDto coffeeDto);
}