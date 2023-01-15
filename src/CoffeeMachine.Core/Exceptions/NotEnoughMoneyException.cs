namespace CoffeeMachine.Core.Exceptions;

/// <summary>
/// Исключение о недостатке средств.
/// </summary>
public class NotEnoughMoneyException : Exception
{
    /// <inheritdoc cref="NotEnoughMoneyException" />
    /// <param name="message"> Сообщение исключения. </param>
    public NotEnoughMoneyException(string message)
        : base(message)
    {
    }
}