namespace CoffeeMachine.Data.Db.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Модель пользователя.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    [MinLength(5), MaxLength(15)]

    public string Login { get; init; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    [MinLength(5), MaxLength(84)]
    public string Password { get; set; }
}