namespace CoffeeMachine.Core.Constants;

/// <summary>
/// Класс для констант-сообщений исключений.
/// </summary>
public static class ExceptionMessages
{
    /// <summary>
    /// Кофе с таким именем уже существует.
    /// </summary>
    public static string DuplicateCoffee => "Кофе с таким именем уже существует.";

    /// <summary>
    /// Пользователь с таким именем уже зарегистрирован.
    /// </summary>
    public static string DuplicateUser => "Пользователь с таким именем уже зарегистрирован.";

    /// <summary>
    /// Неверно указан логин или пароль.
    /// </summary>
    public static string FailedVerification => "Неверно указан логин или пароль.";

    /// <summary>
    /// Недостаточно средств.
    /// </summary>
    public static string NotEnoughMoney => "Недостаточно средств.";

    /// <summary>
    /// Кофе не найдено.
    /// </summary>
    public static string NotFoundCoffee => "Кофе не найдено.";

    /// <summary>
    /// Покупка не найдена.
    /// </summary>
    public static string NotFoundPurchase => "Покупка не найдена.";

    /// <summary>
    /// Пользователя с указанным логином не существует.
    /// </summary>
    public static string NotFoundUser => "Пользователя с указанным логином не существует.";
}