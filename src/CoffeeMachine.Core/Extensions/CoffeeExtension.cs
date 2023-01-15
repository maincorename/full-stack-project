namespace CoffeeMachine.Core.Extensions;

using CoffeeMachine.Core.Dtos;
using CoffeeMachine.Data.Db.Models;

/// <summary>
/// Расширения для Кофе.
/// </summary>
public static class CoffeeExtension
{
    /// <summary>
    /// Преобразование модели кофе в дто кофе.
    /// </summary>
    /// <param name="coffee"> Преобразуемый объект кофе. </param>
    /// <returns> Кофе дто. </returns>
    public static CoffeeDto ToDto(this Coffee coffee)
    {
        return new CoffeeDto
        {
            Id = coffee.Id,
            Price = coffee.Price,
            Name = coffee.Name
        };
    }

    /// <summary>
    /// Преобразование коллекции моделей кофе в коллекцию дто этих кофе.
    /// </summary>
    /// <param name="coffees"> Преобразуемое перечисление кофе. </param>
    /// <returns> Перечисление кофе дто. </returns>
    public static IEnumerable<CoffeeDto?> ToDtoList(this IEnumerable<Coffee> coffees)
    {
        return coffees.Select(coffee => coffee.ToDto());
    }

    /// <summary>
    /// Преобразование дто кофе в модель кофе.
    /// </summary>
    /// <param name="coffeeDto"> Преобразуемый объект дто. </param>
    /// <returns> Кофе. </returns>
    public static Coffee ToModel(this CoffeeDto coffeeDto)
    {
        return new Coffee
        {
            Id = coffeeDto.Id,
            Price = coffeeDto.Price,
            Name = coffeeDto.Name
        };
    }

    /// <summary>
    /// Преобразование модели кофе и сдачи в дто покупки кофе.
    /// </summary>
    /// <param name="coffee"> Модель кофе. </param>
    /// <param name="changeBanknonets"> Список сдачи. </param>
    /// <returns> Дто с купленным кофе и перечисление купюр сдачи. </returns>
    public static PurchaseCoffeeDto ToPurchaseCoffeeDto(this Coffee coffee, IEnumerable<int> changeBanknonets)
    {
        return new PurchaseCoffeeDto
        {
            ChangeBanknotes = changeBanknonets,
            PurchasedCoffee = coffee
        };
    }
}