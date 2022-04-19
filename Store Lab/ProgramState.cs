namespace Store;

// Класс, представляющий состояние программы (ее глобальные переменные)
public static class ProgramState
{
    // Корзина
    public static Cart cart { get; set; } = new();

    // Пара [продукт - количество], необходимая для хранения текущего продукта, обрабатываемого корзиной
    public static KeyValuePair<Product, int> productPairInCart { get; set; } = new();
}
