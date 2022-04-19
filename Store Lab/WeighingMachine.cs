namespace Store;

// Класс, описывающий весы
public static class WeighingMachine
{
    // Текущий продукт на весах
    public static WeighableProduct? weightingProduct { get; private set; } = null;

    // Метод, кладущий товар на весы
    public static void PutProduct(WeighableProduct product)
    {
        weightingProduct = product;
    }

    // Метод, генерирующий случайный вес
    public static double GetWeight()
    {
        Random random = new Random();

        double weight = random.Next(0, 2) + random.NextDouble() + 0.1;

        if (weightingProduct != null)
        {
            weightingProduct.weight = weight;
        }

        return weight;
    }
}
