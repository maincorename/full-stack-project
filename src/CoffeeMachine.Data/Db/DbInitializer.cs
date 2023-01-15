namespace CoffeeMachine.Data.Db;

using CoffeeMachine.Data.Db.Models;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Инициализатор данных в бд.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Инициализация данных в бд.
    /// </summary>
    /// <param name="context"> Контекст данных. </param>
    public static async Task InitializeAsync(CoffeeMachineContext context)
    {
        if (await context.Coffees.AnyAsync())
            return;

        var coffees = new List<Coffee>
        {
            new() { Name = "капучино", Price = 600 },
            new() { Name = "латте", Price = 850 },
            new() { Name = "американо", Price = 900 }
        };

        await context.Coffees.AddRangeAsync(coffees);
        await context.SaveChangesAsync();
    }
}