namespace Store;

// Класс, описывающий корзину и методы работы с ней
public sealed class Cart
{
    // Список пар [продукт - количество]
    public Dictionary<Product, int> countDictionary { get; private set; } = new();
    public Dictionary<WeighableProduct, double> weightDictionary { get; private set; } = new();

    // Конструктор
    public Cart() { }

    // Метод, выполняющий добавление продукта в заданном количестве в корзину
    public void PutProduct(Product product, int count = 1)
    {
        if (product is WeighableProduct)
        {
            WeighableProduct weighableProduct = (WeighableProduct)product;

            // Если продукта в корзине нет, то добавить
            if (!weightDictionary.ContainsKey(weighableProduct))
            {
                weightDictionary.Add(weighableProduct, 0);
            }
            // Увеличить вес
            weightDictionary[weighableProduct] += weighableProduct.weight ?? 0;
        }

        // Если продукта в корзине нет, то добавить
        if (!countDictionary.ContainsKey(product))
        {
            countDictionary.Add(product, 0);
        }
        // Увеличить количество на заданное
        countDictionary[product] += count;
    }

    // Метод, удаляющий продукт в заданном количестве из корзины
    public void RemoveProduct(Product product, int count = 1)
    {
        // Если продукт есть в корзине
        if (countDictionary.ContainsKey(product))
        {
            // Если удаляемое количество больше либо равно тому, что находится в корзине, то удалить продукт полностью
            if (countDictionary[product] <= count)
            {
                countDictionary.Remove(product);
            }
            // Иначе уменшить на заданное количество
            else
            {
                countDictionary[product] -= count;
            }
        }
    }

    // Метод, удаляющий все продукты из корзины
    public void Clear()
    {
        countDictionary.Clear();
    }

    // Метод, возвращающий количество продуктов в корзине
    public int GetCountOfProducts()
    {
        return countDictionary.Select(product => product.Value).Sum();
    }

    // Метод, возвращающий итоговую стоимость продуктов в корзине
    public double GetSum()
    {
        double sum = 0;

        foreach (KeyValuePair<Product, int> pair in countDictionary)
        {
            Product product = pair.Key;
            int count = pair.Value;

            // Если продукт взвешиваемый
            if (product is WeighableProduct)
            {
                WeighableProduct weighableProduct = (WeighableProduct)product;

                // Добавить к итоговой сумме стоимость текущего продукта ([цена за кг] * [вес])
                sum += weighableProduct.price * Math.Round(weightDictionary[weighableProduct], 2);
            }
            else
            {
                sum += product.price * count;
            }
        }

        return sum;
    }
}
