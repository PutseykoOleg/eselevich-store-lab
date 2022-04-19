namespace Store;

// Класс, описывающий меню и методы работы с ним
public class Menu
{
    // Пункты меню
    public List<MenuItem> items = new();

    // Конструктор
    public Menu(params MenuItem[] items)
    {
        this.items = new(items);

        // Сортировка пунктов меню: сначала первичные, а после - вторичные
        this.items.Sort();
    }

    // Конструктор
    public Menu(List<MenuItem> items, params MenuItem[] others)
    {
        List<MenuItem> othersAsList = new(others);

        this.items = items;
        this.items.AddRange(othersAsList);

        // Сортировка пунктов меню: сначала первичные, а после - вторичные
        this.items.Sort();
    }

    // Метод, запускающий меню
    public void Run(string choiseLine = "Выберите действие")
    {
        // Вывести все пункты
        PrintItems();

        // Считать выбранный индекс
        int index = ReadIndex(choiseLine);

        // Запустить действие выбранного пункта
        items[index].chooseAction();
    }

    // Метод, выводящий в консоль пункты меню
    private void PrintItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            MenuItem item = items[i];

            // Если текущий пункт - первый из вторичных, то разделить первичные и вторичные пустой строкой
            if (
                i > 0
                && items[i - 1].type == MenuItem.Type.Primary
                && item.type == MenuItem.Type.Secondary
            )
            {
                Console.WriteLine();
            }

            Console.WriteLine($"{i + 1}) {item.name}");
        }
    }

    // Метод, считывающий выбранный индекс
    private int ReadIndex(string choiseLine)
    {
        return ConsoleReader.ReadInt32(
                inputLine: choiseLine,
                errorLine: "Пунта меню с таким индексом не найдено, повторите выбор еще раз...",
                min: 1,
                max: items.Count
            ) - 1;
    }
}
