namespace Store;

// Класс, описывающий взвешиваемый продукт и методы работы с ним
public sealed class WeighableProduct : Product
{
    // Является ли продукт взвешанным
    public bool isWeighted { get; private set; }

    // Вес (может иметь значение NULL, т.к. по умолчанию продукт не является взвешанным)
    public double? weight { get; set; }

    // Цена за 1кг продукта
    public new double price { get; private set; }

    // Id товара, неоходимое для установки веса на весах
    public int id { get; private set; }

    // Конструктор
    public WeighableProduct()
    {
        this.id = default;
        this.price = default;
        this.weight = null;
        this.isWeighted = false;
    }

    // Конструктор
    public WeighableProduct(int id, string name, double price, string? description = null)
        : base(name: name, description: description, price: default)
    {
        this.id = id;
        this.price = price;
        this.weight = null;
        this.isWeighted = false;
    }

    // Метод конвертации продукта в строку
    public override string ToString()
    {
        return description != null
          ? $"{name} / {description} - {price} руб/кг"
          : $"{name} - {price} руб/кг";
    }
}
