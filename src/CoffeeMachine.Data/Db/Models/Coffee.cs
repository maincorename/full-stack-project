namespace CoffeeMachine.Data.Db.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Модель кофе.
/// </summary>
public class Coffee
{
    /// <summary>
    /// Id кофе.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    /// Наименование кофе.
    /// </summary>
    [StringLength(30, MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Стоимость кофе.
    /// </summary>
    [Range(0, 50000)]
    [DataType(DataType.Currency)]
    [Required]
    public int Price { get; set; }
}