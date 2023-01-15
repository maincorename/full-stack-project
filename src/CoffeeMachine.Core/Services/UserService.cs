namespace CoffeeMachine.Core.Services;

using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;

using CoffeeMachine.Core.Constants;
using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Extensions;
using CoffeeMachine.Data.Db.Dal;
using CoffeeMachine.Data.Db.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Сервис для регистрации и авторизации пользователей.
/// </summary>
public class UserService
{
    /// <inheritdoc cref="UnitOfWork" />
    private readonly UnitOfWork _unitOfWork;

    /// <inheritdoc cref="UserService" />
    /// <param name="unitOfWork"> Единица работы для взаимодействия с базой данных. </param>
    public UserService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Аутентифицировать пользователя.
    /// </summary>
    /// <returns> Dto с jwt-токеном и логином пользователя. </returns>
    /// <param name="loginDataDto"> Данные для аутентификации. </param>
    /// <exception cref="VerificationException"> </exception>
    /// <exception cref="NotFoundException"> </exception>
    public async Task<ResponseJwtDto> AuthenticationUser(UserDto loginDataDto)
    {
        var loginData = loginDataDto.ToModel();

        var user = await _unitOfWork.UserRepository
            .Where(user => user.Login == loginData.Login)
            .SingleOrDefaultAsync();

        if (user is null)
            throw new VerificationException(ExceptionMessages.FailedVerification);

        var userPassword = user.Password;

        var hasher = new PasswordHasher<User>();

        var isVerifiedPassword = hasher.VerifyHashedPassword(user, userPassword, loginData.Password);

        if (isVerifiedPassword is PasswordVerificationResult.Failed)
            throw new VerificationException(ExceptionMessages.FailedVerification);

        var claims = new List<Claim> { new(ClaimTypes.Name, user.Login) };

        var jwtToken = GenerateAccessToken(claims);

        var response = new ResponseJwtDto
        {
            AccessToken = jwtToken,
            Username = user.Login
        };

        return response;
    }

    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    /// <param name="registerData"> Данные для регистрации. </param>
    /// <exception cref="ArgumentNullException"> </exception>
    /// <exception cref="DuplicateNameException"> </exception>
    public async Task RegisterUser(UserDto registerData)
    {
        if (registerData == null)
            throw new ArgumentNullException(nameof(registerData));

        var registerModel = registerData.ToModel();

        var foundUser = await _unitOfWork.UserRepository
            .Where(user => user.Login == registerModel.Login)
            .SingleOrDefaultAsync();

        if (foundUser is not null)
            throw new DuplicateNameException(ExceptionMessages.DuplicateUser);

        var hasher = new PasswordHasher<User>();

        var hashedPassword = hasher.HashPassword(registerModel, registerData.Password);

        registerModel.Password = hashedPassword;

        await _unitOfWork.UserRepository.InsertAsync(registerModel);

        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Создаёт новый токен доступа.
    /// </summary>
    /// <param name="claims"> Перечисление клеймов (утверждений). </param>
    /// <returns> Jwt токен. </returns>
    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = AuthOptions.GetSymmetricSecurityKey();

        var jwt = new JwtSecurityToken(
            AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: new SigningCredentials(secretKey,
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

        return tokenString;
    }
}