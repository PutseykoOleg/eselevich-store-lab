namespace Store;

// Класс, описывающий невзвешиваемый продукт и методы работы с ним
public class Product
{
    // Название
    public string name { get; protected set; }

    // Описание
    public string? description { get; protected set; }

    // Цена за единицу товара
    public double price { get; protected set; }

    // Конструктор
    public Product()
    {
        this.name = "";
        this.description = null;
        this.price = default;
    }

    // Конструктор
    public Product(string name, double price, string? description = null)
    {
        this.name = name;
        this.description = description;
        this.price = price;
    }

    // Метод конвертации продукта в строку
    public override string ToString()
    {
        return description != null
          ? $@"{name} / {description} - {price} руб/шт"
          : $"{name} - {price} руб/шт";
    }
}
