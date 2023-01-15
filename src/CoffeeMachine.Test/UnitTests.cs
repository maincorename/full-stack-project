namespace CoffeeMachine.Test;

using CoffeeMachine.Core.Services;
using CoffeeMachine.Data.Db;
using CoffeeMachine.Data.Db.Dal;
using CoffeeMachine.Data.Db.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

/// <summary>
/// Тесты для кофемашины.
/// </summary>
public class UnitTests
{
    /// <inheritdoc cref="UnitOfWork" />
    private readonly UnitOfWork _unitOfWork;

    /// <inheritdoc cref="ChangeService" />
    private ChangeService _changeService;

    /// <inheritdoc cref="StatisticService" />
    private StatisticService _statisticService;

    /// <inheritdoc cref="UnitTests" />
    public UnitTests()
    {
        var contextOptions = new DbContextOptionsBuilder<CoffeeMachineContext>()
            .UseInMemoryDatabase("CoffeeMachineTest")
            .ConfigureWarnings(database => database.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

        var context = new CoffeeMachineContext(contextOptions);

        _unitOfWork = new UnitOfWork(context);
    }

    /// <summary>
    /// Тест проверки просчёта без сдачи.
    /// </summary>
    [Test]
    public void BanknotesCalculating_ChangeIsEmpty()
    {
        var expectedChange = new List<int>();

        var actualChange = _changeService.CalculateChangeBills(0);

        Assert.That(actualChange, Is.EqualTo(expectedChange));
    }

    /// <summary>
    /// Тест проверки просчёта сдачи.
    /// </summary>
    [Test]
    public void BanknotesIsCalculating()
    {
        var expectedChange = new List<int> { 5000, 1000, 500, 200, 50 };

        var actualChange = _changeService.CalculateChangeBills(6750);

        Assert.That(actualChange, Is.EqualTo(expectedChange));
    }

    /// <summary>
    /// Тест проверки добавления кофе.
    /// </summary>
    [Test]
    public async Task CoffeeIsAdd()
    {
        var expectedCoffee = new Coffee { Name = "fakeCoffee", Price = 1000 };

        await _unitOfWork.CoffeeRepository.InsertAsync(expectedCoffee);
        await _unitOfWork.SaveAsync();
        var actualCoffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(expectedCoffee.Id);

        Assert.That(actualCoffee, Is.EqualTo(expectedCoffee));
    }

    /// <summary>
    /// Тест проверки создания новой статистики.
    /// </summary>
    [Test]
    public async Task EmptyStatistic_ForExistingCoffee_Created()
    {
        const string fakeName = "fakeCoffee";
        var fakeCoffee = await _unitOfWork.CoffeeRepository.Where(fakeCoffeeName => fakeCoffeeName.Name == fakeName)
            .FirstOrDefaultAsync();

        var expectedIncreasedStatistic = await _statisticService.GetStatisticByCoffeeIdAsync(fakeCoffee!.Id);

        Assert.That(expectedIncreasedStatistic!.SpendedMoney, Is.EqualTo(0));
    }

    /// <summary>
    /// Инициализация сервисов.
    /// </summary>
    [SetUp]
    public async Task Setup()
    {
        _changeService = new ChangeService();
        _statisticService = new StatisticService(_unitOfWork);

        var expectedCoffee = new Coffee { Name = "fakeCoffee", Price = 1000 };
        await _unitOfWork.CoffeeRepository.InsertAsync(expectedCoffee);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Тест проверки создания новой статистики с инкрементированным балансом.
    /// </summary>
    [Test]
    public async Task Statistic_ForExistingCoffee_CreatedAndIncreased()
    {
        const string fakeName = "fakeCoffee";
        var fakeCoffee = await _unitOfWork.CoffeeRepository.Where(fakeCoffeeName => fakeCoffeeName.Name == fakeName)
            .FirstOrDefaultAsync();

        await _statisticService.IncreaseStatisticByCoffeeIdAsync(fakeCoffee!.Id);
        var expectedIncreasedStatistic = await _statisticService.GetStatisticByCoffeeIdAsync(fakeCoffee.Id);

        Assert.That(fakeCoffee.Price, Is.EqualTo(expectedIncreasedStatistic!.SpendedMoney));
    }
}