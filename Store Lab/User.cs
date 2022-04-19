namespace Store;

// Класс, описывающий пользователя
public static class User
{
    // Номер бонусной карты
    public static string bonusCardNumber { get; private set; } =
        $"{Faker.GenerateInt32(1000, 9999)} {Faker.GenerateInt32(1000, 9999)} {Faker.GenerateInt32(1000, 9999)} {Faker.GenerateInt32(1000, 9999)}";

    // Список покупок
    public static List<Product> shoppingList { get; private set; } =
        Faker.GenerateListFromOtherOne(Catalog.products);

    // Количество денег на счету
    public static int moneyAmount { get; private set; } = Faker.GenerateInt32(1000, 5000);

    // Количество бонусов
    public static int bonusesAmount { get; private set; } = Faker.GenerateInt32(100, 500);

    // Метод, совершающий оплату со счета
    public static void SubstractMoneyAmount(int amount)
    {
        moneyAmount -= amount >= moneyAmount ? moneyAmount : amount;
    }

    // Метод, совершающий оплату бонусами
    public static void SubstractBonusesAmount(int amount)
    {
        bonusesAmount -= amount >= bonusesAmount ? bonusesAmount : amount;
    }
}
