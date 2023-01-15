namespace CoffeeMachine.Core;

using System.Text;

using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Класс для описания настроек генерации jwt токена.
/// </summary>
public static class AuthOptions
{
    /// <summary>
    /// Потребитель токена.
    /// </summary>
    public const string AUDIENCE = "MyAuthClient";

    /// <summary>
    /// Издатель токена.
    /// </summary>
    public const string ISSUER = "MyAuthServer";

    /// <summary>
    /// Ключ для шифрации.
    /// </summary>
    private const string KEY = "mysupersecret_secretkey!123";

    /// <summary>
    /// Получить защищенный ключ.
    /// </summary>
    /// <returns> Массив байт, созданный по ключу. </returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}