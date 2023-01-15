namespace CoffeeMachine.Core.Services.Interfaces;

/// <summary>
/// Сервис для работы со сдачей.
/// </summary>
public interface IChangeService
{
    /// <summary>
    /// Метод для расчета купюр сдачи.
    /// </summary>
    /// <param name="money"> Средства для покупки. </param>
    /// <returns> Перечисление купюр. </returns>
    public IEnumerable<int> CalculateChangeBills(int money);
}