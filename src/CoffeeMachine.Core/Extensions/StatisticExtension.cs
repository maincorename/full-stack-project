namespace CoffeeMachine.Core.Extensions;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Расширения для статистик.
/// </summary>
public static class StatisticExtension
{
    /// <summary>
    /// Преобразование модели статистики в дто статистики.
    /// </summary>
    /// <param name="statistic"> Преобразуемый объект статистики. </param>
    /// <returns> Дто статистики. </returns>
    public static StatisticDto ToDto(this Statistic statistic)
    {
        return new StatisticDto
        {
            CoffeeId = statistic.CoffeeId,
            CoffeeName = statistic.Coffee.Name,
            SpendedMoney = statistic.BalanceCoffee
        };
    }

    /// <summary>
    /// Преобразование коллекции моделей статистик в список дто.
    /// </summary>
    /// <param name="statistics"> Преобразуемое перечисление статистик. </param>
    /// <returns> Перечисление дто статистик. </returns>
    public static IEnumerable<StatisticDto?> ToDtoList(this IEnumerable<Statistic> statistics)
    {
        return statistics.Select(statistic => statistic.ToDto());
    }
}