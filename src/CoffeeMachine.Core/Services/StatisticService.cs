namespace CoffeeMachine.Core.Services;

using CoffeeMachine.Core.Constants;
using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Extensions;
using CoffeeMachine.Core.Services.Interfaces;
using CoffeeMachine.Data.Db.Dal;
using CoffeeMachine.Data.Db.Models;

using Microsoft.EntityFrameworkCore;

using Serilog;

/// <inheritdoc />
public class StatisticService : IStatisticService
{
    /// <inheritdoc cref="UnitOfWork" />
    private readonly UnitOfWork _unitOfWork;

    /// <inheritdoc cref="IStatisticService" />
    /// <param name="unitOfWork"> Единица работы для взаимодействия с базой данных. </param>
    public StatisticService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteStatisticByCoffeeIdAsync(Guid coffeeId)
    {
        var foundStatistic = await _unitOfWork.StatisticRepository
            .Where(statistic => statistic.CoffeeId == coffeeId)
            .SingleOrDefaultAsync();

        if (foundStatistic is null)
            return true;

        _unitOfWork.StatisticRepository.Delete(foundStatistic);

        await _unitOfWork.SaveAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<StatisticDto?>> GetAllStatisticAsync()
    {
        var statistics = await _unitOfWork.StatisticRepository.GetAllAsync();

        return statistics.ToDtoList();
    }

    /// <inheritdoc />
    /// <exception cref="NotFoundException"> </exception>
    public async Task<StatisticDto?> GetStatisticByCoffeeIdAsync(Guid coffeeId)
    {
        var foundStatistic = await _unitOfWork.StatisticRepository
            .Where(statistic => statistic.Coffee.Id == coffeeId)
            .SingleOrDefaultAsync();

        if (foundStatistic is not null)
            return foundStatistic.ToDto();

        var coffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(coffeeId);

        if (coffee is null)
            throw new NotFoundException(ExceptionMessages.NotFoundCoffee);

        var emptyStatistic = new Statistic { BalanceCoffee = 0, CoffeeId = coffeeId };

        await _unitOfWork.StatisticRepository.InsertAsync(emptyStatistic);

        await _unitOfWork.SaveAsync();

        Log.Information("Статистика не найдена, была создана новая.");

        return emptyStatistic.ToDto();
    }

    /// <inheritdoc />
    /// <exception cref="NotFoundException"> </exception>
    /// <exception cref="ArgumentNullException"> </exception>
    public async Task IncreaseStatisticByCoffeeIdAsync(Guid coffeeId)
    {
        var foundStatistic = await _unitOfWork.StatisticRepository
            .Where(statistic => statistic.CoffeeId == coffeeId)
            .SingleOrDefaultAsync();

        if (foundStatistic is not null)
        {
            foundStatistic.BalanceCoffee += foundStatistic.Coffee.Price;

            _unitOfWork.StatisticRepository.Update(foundStatistic);

            await _unitOfWork.SaveAsync();

            Log.Information($"Баланс статистики по кофе {coffeeId} успешно увеличен.");

            return;
        }

        var coffee = await _unitOfWork.CoffeeRepository
            .Where(coffee => coffeeId == coffee.Id)
            .SingleOrDefaultAsync();

        if (coffee is null)
            throw new NotFoundException(ExceptionMessages.NotFoundCoffee);

        var statistic = new Statistic { BalanceCoffee = coffee.Price, CoffeeId = coffeeId };

        await _unitOfWork.StatisticRepository.InsertAsync(statistic);

        await _unitOfWork.SaveAsync();

        Log.Information($"Статистика для кофе {coffeeId} не найдена, была создана новая.");
    }
}