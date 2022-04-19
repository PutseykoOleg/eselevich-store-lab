namespace Store;

// Класс, описывающий каталог товаров
public static class Catalog
{
    // Список продуктов магазина
    public static List<Product> products { get; private set; } =
        new()
        {
            new Product(name: "Соевое молоко", price: 169.99),
            new Product(name: "Хлеб белый", price: 58.5),
            new Product(name: "Желе", price: 129.99),
            new Product(name: "Салат (фасованый)", description: "100г", price: 69.99),
            new Product(name: "Сок", price: 125.6),
            new Product(name: "Маринованые огурцы", price: 169.99),
            new Product(name: "Шоколадка", price: 109.9),
            new Product(name: "Черный чай", description: "25 пакетиков", price: 119.9),
            new Product(name: "Леденец", price: 169.99),
            new Product(name: "Торт \"Божья коровка\"", description: "2кг", price: 429.9),
            new WeighableProduct(id: 1, name: "Картофель", price: 149.0),
            new WeighableProduct(id: 2, name: "Морковь", price: 34.9),
            new WeighableProduct(id: 3, name: "Лук", price: 29.9),
            new WeighableProduct(id: 4, name: "Яблоки", price: 169.9),
            new WeighableProduct(id: 5, name: "Тыква", price: 202.0),
            new WeighableProduct(id: 6, name: "Конфеты шоколадные", price: 149.5),
            new WeighableProduct(id: 7, name: "Баклажан", price: 81.0),
            new WeighableProduct(id: 8, name: "Сахар", price: 70.9),
            new WeighableProduct(id: 9, name: "Помидоры", price: 149.0),
            new WeighableProduct(id: 10, name: "Орехи грецкие", price: 500.0),
        };
}
