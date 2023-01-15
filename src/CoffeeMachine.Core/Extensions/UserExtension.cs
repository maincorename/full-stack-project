namespace CoffeeMachine.Core.Extensions;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Расширение для пользователей.
/// </summary>
public static class UserExtension
{
    /// <summary>
    /// Преобразование дто пользователя в модель пользователя.
    /// </summary>
    /// <param name="userDto"> Дто пользователя. </param>
    /// <returns> Модель пользователя. </returns>
    public static User ToModel(this UserDto userDto)
    {
        var user = new User
        {
            Login = userDto.Login,
            Password = userDto.Password
        };

        return user;
    }
}