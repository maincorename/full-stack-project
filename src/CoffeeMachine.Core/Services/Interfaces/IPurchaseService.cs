namespace CoffeeMachine.Core.Services.Interfaces;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;

/// <summary>
/// Сервис для работы с покупками.
/// </summary>
public interface IPurchaseService
{
    /// <summary>
    /// Купить кофе.
    /// </summary>
    /// <param name="id"> Идентификатор покупаемого кофе. </param>
    /// <param name="money"> Средства для покупки. </param>
    /// <returns> Дто покупки кофе. </returns>
    public Task<PurchaseCoffeeDto> BuyCoffeeAsync(Guid id, int money);

    /// <summary>
    /// Удалить все покупки по идентификатору кофе.
    /// </summary>
    /// <param name="coffeeId"> Идентификатор кофе. </param>
    /// <returns> true - объекты удалены. </returns>
    public Task<bool> DeleteAllPurchasesByCoffeeIdAsync(Guid coffeeId);

    /// <summary>
    /// Удалить покупку по идентификатору.
    /// </summary>
    /// <param name="purchaseId"> Идентификатор покупки. </param>
    /// <returns> true - объект удалён. </returns>
    public Task<bool> DeletePurchaseByIdAsync(Guid purchaseId);

    /// <summary>
    /// Получить все покупки.
    /// </summary>
    /// <returns> Перечисление всех покупок кофе. </returns>
    public Task<IEnumerable<PurchaseDto?>> GetAllPurchasesAsync();

    /// <summary>
    /// Получить покупку по идентификатору.
    /// </summary>
    /// <param name="purchaseId"> Идентификатор покупки. </param>
    /// <returns> Дто покупки. </returns>
    public Task<PurchaseDto?> GetPurchaseByIdAsync(Guid purchaseId);
}