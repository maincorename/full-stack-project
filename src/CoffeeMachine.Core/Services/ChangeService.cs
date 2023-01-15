namespace CoffeeMachine.Core.Services;

using CoffeeMachine.Core.Services.Interfaces;

/// <inheritdoc />
public class ChangeService : IChangeService
{
    /// <summary>
    /// Список номиналов купюр.
    /// </summary>
    private readonly List<int> _denominations = new() { 50, 100, 200, 500, 1000, 2000, 5000 };

    /// <inheritdoc />
    /// <param name="inputMoney"> Средства для рассчета сдачи. </param>
    public IEnumerable<int> CalculateChangeBills(int inputMoney)
    {
        var sortedDenominations = _denominations.OrderByDescending(denomination => denomination)
            .Where(denomination => denomination <= inputMoney);

        var resultCalculating = new List<int>();

        foreach (var cupure in sortedDenominations)
        {
            while (inputMoney >= cupure)
            {
                resultCalculating.Add(cupure);
                inputMoney -= cupure;
            }
        }

        return resultCalculating;
    }
}