namespace CoffeeMachine.WebApi.Controllers;

using System.Security;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Services;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для пользователей.
/// </summary>
[Route("api/[controller]")]
public class UserController : Controller
{
    /// <inheritdoc cref="UserService" />
    private readonly UserService _userService;

    /// <inheritdoc cref="UserController" />
    /// <param name="userService"> Сервис для работы с пользователями. </param>
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Аутентификация пользователя.
    /// </summary>
    /// <param name="loginData"> Данные для аутентификации. </param>
    /// <returns> Jwt токен с логином пользователя. </returns>
    /// <response code="401"> Неверный логин или пароль. </response>
    /// <response code="200"> Аутентификация выполнена успешно. </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Authentication([FromBody] UserDto loginData)
    {
        try
        {
            var response = await _userService.AuthenticationUser(loginData);

            return Ok(response);
        }
        catch (VerificationException exception)
        {
            return Unauthorized(exception.Message);
        }
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="registerData"> Данные для регистрации. </param>
    /// <returns> Json строку с зарегистрированным пользователем. </returns>
    /// <response code="400"> Введены некорректные данные. </response>
    /// <response code="400"> Пользователь с таким login уже существует. </response>
    /// <response code="200"> Регистрация выполнена успешно. </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Registration([FromBody] UserDto registerData)
    {
        try
        {
            await _userService.RegisterUser(registerData);

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}