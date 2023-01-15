namespace CoffeeMachine.Core.Dtos;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Дто статистики.
/// </summary>
public record StatisticDto
{
    /// <summary>
    /// Идентификатор кофе.
    /// </summary>
    [Required]
    public Guid CoffeeId { get; init; }

    /// <summary>
    /// Наименование кофе.
    /// </summary>
    [Required]
    public string CoffeeName { get; init; }

    /// <summary>
    /// Количество денег затраченных на кофе.
    /// </summary>
    [Required]
    public int SpendedMoney { get; init; }
}