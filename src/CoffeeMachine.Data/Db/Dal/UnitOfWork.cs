namespace CoffeeMachine.Data.Db.Dal;

using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Единица работы, возвращает экземпляры репозиториев, работает с одним контекстом данных.
/// </summary>
public class UnitOfWork : IDisposable
{
    /// <inheritdoc cref="CoffeeMachineContext" />
    private readonly CoffeeMachineContext _context;

    /// <summary>
    /// Экземпляр репозитория кофе.
    /// </summary>
    private GenericRepository<Coffee> _coffeeRepository;

    /// <summary>
    /// Флаг проверки, освобожден ли контекст.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Экземпляр репозитория покупок.
    /// </summary>
    private GenericRepository<Purchase> _purchaseRepository;

    /// <summary>
    /// Экземпляр репозитория статистики.
    /// </summary>
    private GenericRepository<Statistic> _statisticRepository;

    /// <summary>
    /// Экземпляр репозитория пользователей.
    /// </summary>
    private GenericRepository<User> _userRepository;

    /// <inheritdoc cref="UnitOfWork" />
    /// <param name="context"> Общий контекст данных для репозиториев. </param>
    public UnitOfWork(CoffeeMachineContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Репозиторий кофе.
    /// </summary>
    public GenericRepository<Coffee> CoffeeRepository
    {
        get
        {
            if (_coffeeRepository is null)
                _coffeeRepository = new GenericRepository<Coffee>(_context);

            return _coffeeRepository;
        }
    }

    /// <summary>
    /// Репозиторий покупок.
    /// </summary>
    public GenericRepository<Purchase> PurchaseRepository
    {
        get
        {
            if (_purchaseRepository is null)
                _purchaseRepository = new GenericRepository<Purchase>(_context);

            return _purchaseRepository;
        }
    }

    /// <summary>
    /// Репозиторий статистики.
    /// </summary>
    public GenericRepository<Statistic> StatisticRepository
    {
        get
        {
            if (_statisticRepository is null)
                _statisticRepository = new GenericRepository<Statistic>(_context);

            return _statisticRepository;
        }
    }

    /// <summary>
    /// Репозиторий пользователей.
    /// </summary>
    public GenericRepository<User> UserRepository
    {
        get
        {
            if (_userRepository is null)
                _userRepository = new GenericRepository<User>(_context);

            return _userRepository;
        }
    }

    /// <summary>
    /// Освобождает контекст бд объекта uow.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Сохранение изменений контекста.
    /// </summary>
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Освобождает контекст бд объекта uow.
    /// </summary>
    /// <param name="disposing"> Флаг освобождения контекста. </param>
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _context.Dispose();
        }

        _disposed = true;
    }
}