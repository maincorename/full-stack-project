namespace CoffeeMachine.Data.Db.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Модель покупки.
/// </summary>
public class Purchase
{
    /// <summary>
    /// Купленный кофе.
    /// </summary>
    public virtual Coffee Coffee { get; init; }

    /// <summary>
    /// Идентификатор кофе.
    /// </summary>
    [Required]
    public Guid CoffeeId { get; init; }

    /// <summary>
    /// Время покупки.
    /// </summary>
    [DataType(DataType.DateTime)]
    [Required]
    public DateTime Date { get; init; }

    /// <summary>
    /// Идентификатор покупки.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
}