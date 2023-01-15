namespace CoffeeMachine.Core.Services;

using System.Data;

using CoffeeMachine.Core.Constants;
using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Core.Exceptions;
using CoffeeMachine.Core.Extensions;
using CoffeeMachine.Core.Services.Interfaces;
using CoffeeMachine.Data.Db.Dal;

using Microsoft.EntityFrameworkCore;

/// <inheritdoc />
public class CoffeeService : ICoffeeService
{
    /// <inheritdoc cref="UnitOfWork" />
    private readonly UnitOfWork _unitOfWork;

    /// <inheritdoc cref="ICoffeeService" />
    /// <param name="unitOfWork"> Единица работы для взаимодействия с базой данных. </param>
    public CoffeeService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    /// <exception cref="DuplicateNameException"> </exception>
    /// <exception cref="ArgumentNullException"> </exception>
    public async Task<CoffeeDto> AddCoffeeAsync(CoffeeDto coffeeDto)
    {
        if (coffeeDto is null)
            throw new ArgumentNullException(nameof(coffeeDto));

        var coffeeModel = coffeeDto.ToModel();

        var foundCoffee = await _unitOfWork.CoffeeRepository
            .Where(coffee => coffee.Name == coffeeModel.Name)
            .SingleOrDefaultAsync();

        if (foundCoffee is not null)
            throw new DuplicateNameException(ExceptionMessages.DuplicateCoffee);

        var addedCoffee = await _unitOfWork.CoffeeRepository.InsertAsync(coffeeModel);

        await _unitOfWork.SaveAsync();

        return addedCoffee.ToDto();
    }

    /// <inheritdoc />
    /// <exception cref="NotFoundException"> </exception>
    public async Task<bool> DeleteCoffeeByIdAsync(Guid coffeeId)
    {
        var foundCoffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(coffeeId);

        if (foundCoffee is null)
            throw new NotFoundException(ExceptionMessages.NotFoundCoffee);

        var isDeleted = _unitOfWork.CoffeeRepository.Delete(foundCoffee);

        await _unitOfWork.SaveAsync();

        return isDeleted;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CoffeeDto?>> GetAllCoffeesAsync()
    {
        var coffees = await _unitOfWork.CoffeeRepository.GetAllAsync();

        return coffees.ToDtoList();
    }

    /// <inheritdoc />
    /// <exception cref="NotFoundException"> </exception>
    public async Task<CoffeeDto?> GetCoffeeByIdAsync(Guid coffeeId)
    {
        var foundCoffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(coffeeId);

        return foundCoffee is null
            ? throw new NotFoundException(ExceptionMessages.NotFoundCoffee)
            : foundCoffee.ToDto();
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException"> </exception>
    /// <exception cref="NotFoundException"> </exception>
    public async Task<CoffeeDto?> UpdateCoffeeAsync(CoffeeDto coffeeDto)
    {
        if (coffeeDto is null)
            throw new ArgumentNullException(nameof(coffeeDto));

        var coffee = coffeeDto.ToModel();

        var foundCoffee = await _unitOfWork.CoffeeRepository.GetByIdAsync(coffee.Id);

        if (foundCoffee is null)
            throw new NotFoundException(ExceptionMessages.NotFoundCoffee);

        foundCoffee.Price = coffeeDto.Price;
        foundCoffee.Name = coffeeDto.Name;

        _unitOfWork.CoffeeRepository.Update(foundCoffee);

        await _unitOfWork.SaveAsync();

        return foundCoffee.ToDto();
    }
}