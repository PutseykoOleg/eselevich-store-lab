namespace Store;

// Класс, описывающий методы работы со стеком меню
public static class MenuManager
{
    // История вызовов меню
    private static Stack<Menu> _history = new Stack<Menu>();

    // Действие, совершаемое при выборе пункта "Главное меню"
    private static Action _goToMainMenuAction = () =>
    {
        // Удаление всех меню из стека, кроме главного
        while (_history.Count > 1)
        {
            _history.Pop();
        }

        // Очистка предыдущего меню
        Console.Clear();

        // Запуск главного меню
        _history.First().Run();
    };

    // Действие, совершаемое при выборе пункта "Выход"
    private static Action _exitAction = () => { };

    // Метод запуска меню
    public static void RunMenu(Menu menu, string additionalInfo = "")
    {
        // Очистить консоль
        Console.Clear();

        // Вывести дополнительную информацию перед меню
        if (!String.IsNullOrEmpty(additionalInfo))
        {
            Console.WriteLine(additionalInfo);
        }

        // Добавить меню в стек
        _history.Push(menu);
        // Вызвать это меню
        _history.First().Run();
    }

    // Метод создания главного меню, добавляет дополнительные пункты
    public static Menu CreateMainMenu(params MenuItem[] items)
    {
        return new Menu(
            new List<MenuItem>(items),
            new MenuItem("Выйти", _exitAction, MenuItem.Type.Secondary)
        );
    }

    // Метод создания дочернего меню, добавляет дополнительные пункты
    public static Menu CreateSubMenu(params MenuItem[] items)
    {
        return new Menu(
            new List<MenuItem>(items),
            new MenuItem("Главное меню", _goToMainMenuAction, MenuItem.Type.Secondary),
            new MenuItem("Выйти", _exitAction, MenuItem.Type.Secondary)
        );
    }

    // Метод создания дочернего меню, добавляет дополнительные пункты
    public static Menu CreateSubMenu(List<MenuItem> items, params MenuItem[] others)
    {
        List<MenuItem> othersAsList = new(others);
        othersAsList.AddRange(items);

        return new Menu(
            othersAsList,
            new MenuItem("Главное меню", _goToMainMenuAction, MenuItem.Type.Secondary),
            new MenuItem("Выйти", _exitAction, MenuItem.Type.Secondary)
        );
    }
}
