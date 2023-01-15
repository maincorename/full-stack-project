namespace CoffeeMachine.Core.Dtos;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Дто покупки.
/// </summary>
public record PurchaseDto
{
    /// <summary>
    /// Купленный кофе.
    /// </summary>
    [Required]
    public Guid CoffeeId { get; init; }

    /// <summary>
    /// Наименование кофе.
    /// </summary>
    public string CoffeeName { get; init; }

    /// <summary>
    /// Идентификатор покупки.
    /// </summary>
    [Required]
    public Guid Id { get; init; }

    /// <summary>
    /// Дата и время покупки.
    /// </summary>
    [Required]
    public DateTime Time { get; init; }
}