namespace CoffeeMachine.Core.Dtos;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Дто пользователя.
/// </summary>
public record UserDto
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    public string Login { get; init; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    public string Password { get; init; }
}