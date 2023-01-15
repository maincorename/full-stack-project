namespace CoffeeMachine.Core.Dtos;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Дто кофе.
/// </summary>
public record CoffeeDto
{
    /// <summary>
    /// Идентификатор кофе.
    /// </summary>
    [Required]
    public Guid Id { get; init; }

    /// <summary>
    /// Название кофе.
    /// </summary>
    [Required]
    public string Name { get; init; }

    /// <summary>
    /// Стоимость кофе.
    /// </summary>
    [Required]
    public int Price { get; init; }
}