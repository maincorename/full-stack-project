namespace CoffeeMachine.Core.Dtos;

using System.ComponentModel.DataAnnotations;

using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Дто покупки кофе.
/// </summary>
public record PurchaseCoffeeDto
{
    /// <summary>
    /// Перечисление купюр для сдачи.
    /// </summary>
    [Required]
    public IEnumerable<int> ChangeBanknotes { get; init; }

    /// <summary>
    /// Купленный кофе.
    /// </summary>
    [Required]
    public Coffee PurchasedCoffee { get; init; }
}