namespace CoffeeMachine.Data.Db;

using CoffeeMachine.Data.Db.Models;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контекст данных для работы с кофемашиной.
/// </summary>
public class CoffeeMachineContext : DbContext
{
    /// <inheritdoc cref="CoffeeMachineContext" />
    public CoffeeMachineContext(DbContextOptions<CoffeeMachineContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Коллекция кофе.
    /// </summary>
    public DbSet<Coffee> Coffees { get; set; }

    /// <summary>
    /// Покупки кофе.
    /// </summary>
    public DbSet<Purchase> Purchases { get; set; }

    /// <summary>
    /// Статистика учета покупки конкретного кофе.
    /// </summary>
    public DbSet<Statistic> Statistics { get; set; }

    /// <summary>
    /// Коллекция пользователей.
    /// </summary>
    public DbSet<User> Users { get; set; }
}