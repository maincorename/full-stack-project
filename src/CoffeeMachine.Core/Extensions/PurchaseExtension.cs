namespace CoffeeMachine.Core.Extensions;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Расширения для покупок.
/// </summary>
public static class PurchaseExtension
{
    /// <summary>
    /// Преобразование модели покупки в дто покупки.
    /// </summary>
    /// <param name="purchase"> Преобразуемый объект покупки. </param>
    /// <returns> Дто покупки. </returns>
    public static PurchaseDto ToDto(this Purchase purchase)
    {
        return new PurchaseDto
        {
            Id = purchase.Id,
            CoffeeName = purchase.Coffee.Name,
            CoffeeId = purchase.CoffeeId,
            Time = purchase.Date
        };
    }

    /// <summary>
    /// Преобразование коллекции моделей покупок в коллекцию дто покупок.
    /// </summary>
    /// <param name="purchases"> Преобразуемое перечисление покупок. </param>
    /// <returns> Перечисление дто покупок кофе. </returns>
    public static IEnumerable<PurchaseDto?> ToDtoList(this IEnumerable<Purchase> purchases)
    {
        return purchases.Select(purchase => purchase.ToDto());
    }
}