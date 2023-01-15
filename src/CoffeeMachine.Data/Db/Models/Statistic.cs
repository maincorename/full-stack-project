namespace CoffeeMachine.Data.Db.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Модель статистики.
/// </summary>
public class Statistic
{
    /// <summary>
    /// Текущий баланс по кофе.
    /// </summary>
    [DataType(DataType.Currency)]
    [Required]
    public int BalanceCoffee { get; set; }

    /// <summary>
    /// Кофе, по которому ведётся статистика.
    /// </summary>
    [Required]
    public virtual Coffee Coffee { get; init; }

    /// <summary>
    /// Идентификатор кофе.
    /// </summary>
    [Required]
    public Guid CoffeeId { get; init; }

    /// <summary>
    /// Идентификатор статистики.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
}