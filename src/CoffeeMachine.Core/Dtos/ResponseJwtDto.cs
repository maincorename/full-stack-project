namespace CoffeeMachine.Core.Dtos;

/// <summary>
/// Дто ответа аутентификации.
/// </summary>
public record ResponseJwtDto
{
    /// <summary>
    /// JWT-токен доступа.
    /// </summary>
    public string AccessToken { get; init; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public string Username { get; init; }
}