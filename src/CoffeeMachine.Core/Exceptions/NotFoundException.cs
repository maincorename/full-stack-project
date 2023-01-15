namespace CoffeeMachine.Core.Exceptions;

/// <summary>
/// Исключение о не нахождении объекта.
/// </summary>
public class NotFoundException : Exception
{
    /// <inheritdoc cref="NotFoundException" />
    /// <param name="message"> Сообщение исключения. </param>
    public NotFoundException(string message)
        : base(message)
    {
    }
}