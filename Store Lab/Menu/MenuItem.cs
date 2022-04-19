namespace Store;

// Класс, описывающий пункт меню и методы работы с ним
public struct MenuItem : IComparable<MenuItem>
{
    // Тип пункта меню
    public enum Type
    {
        // Основной
        Primary = 1,

        // Вторичный
        Secondary = 0
    }

    // Название
    public string name { get; private set; }

    // Действие, выполняемое при выборе текущего пункта
    public Action chooseAction { get; private set; }

    // Тип пункта
    public Type type { get; private set; }

    // Конструктор
    public MenuItem(string name, Action chooseAction, Type type = Type.Primary)
    {
        this.name = name;
        this.chooseAction = chooseAction;
        this.type = type;
    }

    // Метод сравнения, небходимый для сортировки пунктов в меню
    public int CompareTo(MenuItem other)
    {
        // Сначала будут идти основные, а после - вторичные
        return other.type - this.type;
    }
}
