namespace CoffeeMachine.Core.Services;

using CoffeeMachine.Core.Constants;
using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Extensions;
using CoffeeMachine.Core.Services.Interfaces;
using CoffeeMachine.Data.Db.Dal;
using CoffeeMachine.Data.Db.Models;

using Microsoft.EntityFrameworkCore;

/// <inheritdoc />
public class PurchaseService : IPurchaseService
{
    /// <inheritdoc cref="IChangeService" />
    private readonly IChangeService _changeService;

    /// <inheritdoc cref="IStatisticService" />
    private readonly IStatisticService _statisticService;

    /// <inheritdoc cref="UnitOfWork" />
    private readonly UnitOfWork _unitOfWork;

    /// <inheritdoc cref="IPurchaseService" />
    /// <param name="unitOfWork"> Eдиница работы для взаимодействия с базой данных. </param>
    /// <param name="statisticService"> Сервис для работы со статистиками. </param>
    /// <param name="changeService"> Сервис для работы со сдачей. </param>
    public PurchaseService(UnitOfWork unitOfWork, IStatisticService statisticService, IChangeService changeService)
    {
        _unitOfWork = unitOfWork;
        _statisticService = statisticService;
        _changeService = changeService;
    }

    /// <summary>
    /// Оформляет покупку кофе с обновлением статистики и рассчетом сдачи.
    /// </summary>
    /// <exception cref="ArgumentNullException"> </exception>
    /// <exception cref="NotEnoughMoneyException"> </exception>
    /// <exception cref="NotFoundException"> </exception>
    public async Task<PurchaseCoffeeDto> BuyCoffeeAsync(Guid id, int money)
    {
        var foundCoffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(id);

        if (foundCoffee is null)
            throw new NotFoundException(ExceptionMessages.NotFoundCoffee);

        if (money < foundCoffee.Price)
            throw new NotEnoughMoneyException(ExceptionMessages.NotEnoughMoney);

        var changeBills = money - foundCoffee.Price;

        var changeBillsList = _changeService.CalculateChangeBills(changeBills);

        var createdPurchase = new Purchase { Date = DateTime.Now, CoffeeId = foundCoffee.Id };

        await _unitOfWork.PurchaseRepository.InsertAsync(createdPurchase);

        await _unitOfWork.SaveAsync();

        await _statisticService.IncreaseStatisticByCoffeeIdAsync(foundCoffee.Id);

        var purchaseCoffee = foundCoffee.ToPurchaseCoffeeDto(changeBillsList);

        return purchaseCoffee;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAllPurchasesByCoffeeIdAsync(Guid coffeeId)
    {
        var purchases = await _unitOfWork.PurchaseRepository
            .Where(purchase => purchase.CoffeeId == coffeeId).ToListAsync();

        if (purchases.Any() is false)
            return true;

        foreach (var purchase in purchases)
        {
            _unitOfWork.PurchaseRepository.Delete(purchase);
        }

        await _unitOfWork.SaveAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeletePurchaseByIdAsync(Guid purchaseId)
    {
        var purchaseForDelete = await _unitOfWork.PurchaseRepository.GetByIdAsync(purchaseId);

        if (purchaseForDelete is null)
            throw new NotFoundException(ExceptionMessages.NotFoundPurchase);

        _unitOfWork.PurchaseRepository.Delete(purchaseForDelete);

        await _unitOfWork.SaveAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PurchaseDto?>> GetAllPurchasesAsync()
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetAllAsync();

        return purchases.ToDtoList();
    }

    /// <inheritdoc />
    /// <exception cref="NotFoundException"> </exception>
    public async Task<PurchaseDto?> GetPurchaseByIdAsync(Guid purchaseId)
    {
        var foundPurchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(purchaseId);

        if (foundPurchase is null)
            throw new NotFoundException(ExceptionMessages.NotFoundPurchase);

        return foundPurchase.ToDto();
    }
}