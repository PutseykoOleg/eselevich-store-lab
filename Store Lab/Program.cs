namespace Store;

internal partial class Program
{
    // Меню оплаты
    private static Menu GetPaymentMenu()
    {
        List<MenuItem> menuItems = new();

        double cartSum = ProgramState.cart.GetSum();

        // Если пользователю хватает средств для оплаты
        if (cartSum <= User.moneyAmount + User.bonusesAmount)
        {
            // Добавить пункт оплаты со счета
            menuItems.Add(
                new MenuItem(
                    "Оплатить со счета",
                    () =>
                    {
                        // Очистить консоль для вывода новой информации
                        Console.Clear();

                        // Вывод информации о счете
                        Console.WriteLine(
                            $"К оплате: {cartSum}\n\nНа счету: {User.moneyAmount} руб\nБонусов: {User.bonusesAmount}"
                        );

                        // Если покупка может быть полностью оплачена со счета
                        if (User.moneyAmount >= cartSum)
                        {
                            // Считывание разрешения на списание баллов
                            bool canSubstrateAllMoneyAmount = ConsoleReader.ReadBool(
                                inputLine: "Оплатить покупку полностью со счета? y/n",
                                errorLine: "Необходимо ввести y/Y (да) или n/N (нет), попробуйте еще раз..."
                            );

                            if (canSubstrateAllMoneyAmount)
                            {
                                // Списание средств со счета
                                User.SubstractMoneyAmount(
                                    (int)Math.Round(cartSum, MidpointRounding.ToPositiveInfinity)
                                );

                                // Очистка корзины
                                ProgramState.cart.Clear();

                                MenuManager.RunMenu(
                                    additionalInfo: "Товары успешно оплачены!\n",
                                    menu: GetMainMenu()
                                );

                                return;
                            }
                        }

                        // Считывание количества удаляемого товара
                        int moneyAmountToSubstrate = ConsoleReader.ReadInt32(
                            inputLine: "Списать со счета",
                            errorLine: "Не удалось списать указанную сумму, попробуйте еще раз...",
                            min: 0,
                            max: User.moneyAmount
                        );

                        // Если суммы не хватило
                        if (moneyAmountToSubstrate < cartSum)
                        {
                            Console.WriteLine(
                                $"\nДо полной оплаты не хватает {cartSum - moneyAmountToSubstrate} руб"
                            );

                            // Если бонусов хватает для дополнительного списания
                            if (User.bonusesAmount >= cartSum - moneyAmountToSubstrate)
                            {
                                // Считывание разрешения на списание баллов
                                bool canSubstrateBonuses = ConsoleReader.ReadBool(
                                    inputLine: "Списать бонусы? y/n",
                                    errorLine: "Необходимо ввести y/Y (да) или n/N (нет), попробуйте еще раз..."
                                );

                                if (!canSubstrateBonuses)
                                {
                                    MenuManager.RunMenu(
                                        additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                        menu: GetCartMenu()
                                    );

                                    return;
                                }

                                // Считывание количества бонусов
                                int bonusesAmountToSubstrate = ConsoleReader.ReadInt32(
                                    inputLine: "Списать бонусов",
                                    errorLine: "Не удалось списать указанное количество бонусов, попробуйте еще раз...",
                                    min: 0,
                                    max: User.bonusesAmount
                                );

                                if (moneyAmountToSubstrate + bonusesAmountToSubstrate >= cartSum)
                                {
                                    // Списание средств со счета
                                    User.SubstractMoneyAmount(moneyAmountToSubstrate);

                                    // Списание баллов
                                    User.SubstractBonusesAmount(bonusesAmountToSubstrate);

                                    // Очистка корзины
                                    ProgramState.cart.Clear();

                                    MenuManager.RunMenu(
                                        additionalInfo: "Товары успешно оплачены!\n",
                                        menu: GetMainMenu()
                                    );
                                }
                                else
                                {
                                    MenuManager.RunMenu(
                                        additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                        menu: GetCartMenu()
                                    );
                                }
                            }
                            else
                            {
                                MenuManager.RunMenu(
                                    additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                    menu: GetCartMenu()
                                );
                            }
                        }
                        else
                        {
                            // Списание средств со счета
                            User.SubstractMoneyAmount(moneyAmountToSubstrate);

                            // Очистка корзины
                            ProgramState.cart.Clear();

                            MenuManager.RunMenu(
                                additionalInfo: "Товары успешно оплачены!\n",
                                menu: GetMainMenu()
                            );
                        }
                    }
                )
            );

            // Добавить пункт оплаты бонусами
            menuItems.Add(
                new MenuItem(
                    "Оплатить бонусами",
                    () =>
                    {
                        // Очистить консоль для вывода новой информации
                        Console.Clear();

                        double cartSum = ProgramState.cart.GetSum();

                        // Вывод информации о счете
                        Console.WriteLine(
                            $"К оплате: {cartSum}\n\nНа счету: {User.moneyAmount} руб\nБонусов: {User.bonusesAmount}\n"
                        );

                        // Если покупка может быть полностью оплачена бонусами
                        if (User.bonusesAmount >= cartSum)
                        {
                            // Считывание разрешения на списание бонусов
                            bool canSubstrateAllBonusesAmount = ConsoleReader.ReadBool(
                                inputLine: "Оплатить покупку полностью бонусами? y/n",
                                errorLine: "Необходимо ввести y/Y (да) или n/N (нет), попробуйте еще раз..."
                            );

                            if (canSubstrateAllBonusesAmount)
                            {
                                // Списание бонусов
                                User.SubstractBonusesAmount(
                                    (int)Math.Round(cartSum, MidpointRounding.ToPositiveInfinity)
                                );

                                // Очистка корзины
                                ProgramState.cart.Clear();

                                MenuManager.RunMenu(
                                    additionalInfo: "Товары успешно оплачены!\n",
                                    menu: GetMainMenu()
                                );

                                return;
                            }
                        }

                        // Считывание количества бонусов
                        int bonusesAmountToSubstrate = ConsoleReader.ReadInt32(
                            inputLine: "Списать бонусов",
                            errorLine: "Не удалось списать указанное количество бонусов, попробуйте еще раз...",
                            min: 0,
                            max: User.bonusesAmount
                        );

                        // Если бонусов не хватило
                        if (bonusesAmountToSubstrate < cartSum)
                        {
                            Console.WriteLine(
                                $"\nДо полной оплаты не хватает {cartSum - bonusesAmountToSubstrate} руб"
                            );

                            // Если бонусов хватает для дополнительного списания
                            if (User.moneyAmount >= cartSum - bonusesAmountToSubstrate)
                            {
                                // Считывание разрешения на списание со счета
                                bool canSubstrateMoneyAmount = ConsoleReader.ReadBool(
                                    inputLine: "Списать со счета? y/n",
                                    errorLine: "Необходимо ввести y/Y (да) или n/N (нет), попробуйте еще раз..."
                                );

                                if (!canSubstrateMoneyAmount)
                                {
                                    MenuManager.RunMenu(
                                        additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                        menu: GetCartMenu()
                                    );

                                    return;
                                }

                                // Считывание количества удаляемого товара
                                int moneyAmountToSubstrate = ConsoleReader.ReadInt32(
                                    inputLine: "Списать со счета",
                                    errorLine: "Не удалось списать указанную сумму, попробуйте еще раз...",
                                    min: 0,
                                    max: User.moneyAmount
                                );

                                if (moneyAmountToSubstrate + bonusesAmountToSubstrate >= cartSum)
                                {
                                    // Списание средств со счета
                                    User.SubstractMoneyAmount(moneyAmountToSubstrate);

                                    // Списание баллов
                                    User.SubstractBonusesAmount(bonusesAmountToSubstrate);

                                    // Очистка корзины
                                    ProgramState.cart.Clear();

                                    MenuManager.RunMenu(
                                        additionalInfo: "Товары успешно оплачены!\n",
                                        menu: GetMainMenu()
                                    );
                                }
                                else
                                {
                                    MenuManager.RunMenu(
                                        additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                        menu: GetCartMenu()
                                    );
                                }
                            }
                            else
                            {
                                MenuManager.RunMenu(
                                    additionalInfo: "Недостаточно средств для списания, попробуйте убрать что-нибудь из корзины...\n",
                                    menu: GetCartMenu()
                                );
                            }
                        }
                        else
                        {
                            // Списание средств со счета
                            User.SubstractBonusesAmount(bonusesAmountToSubstrate);

                            // Очистка корзины
                            ProgramState.cart.Clear();

                            MenuManager.RunMenu(
                                additionalInfo: "Товары успешно оплачены!\n",
                                menu: GetMainMenu()
                            );
                        }
                    }
                )
            );
        }

        return MenuManager.CreateSubMenu(menuItems);
    }

    // Меню продукта в корзине
    private static Menu GetCartProductMenu()
    {
        return MenuManager.CreateSubMenu(
            new MenuItem(
                "Убрать из корзины",
                () =>
                {
                    // Очистить консоль для вывода новой информации
                    Console.Clear();

                    // Вывод информации о товаре
                    Console.WriteLine(
                        $"Текущего товара в корзине: {ProgramState.productPairInCart.Value} шт\n"
                    );

                    // Считывание количества удаляемого товара
                    int count = ConsoleReader.ReadInt32(
                        inputLine: "Убрать в количестве",
                        errorLine: "Не удалось удалить указанное количество продукта, попробуйте еще раз...",
                        min: 0,
                        max: ProgramState.productPairInCart.Value
                    );

                    // Удаление товара их корзины
                    ProgramState.cart.RemoveProduct(ProgramState.productPairInCart.Key, count);

                    // Вернуться в меню корзины
                    MenuManager.RunMenu(GetCartMenu());
                }
            )
        );
    }

    // Меню корзины
    private static Menu GetCartMenu()
    {
        List<MenuItem> primaryMenuItems = new List<MenuItem>(
            // Проход по каждому продукту из корзины и конвертация его в пункт меню
            ProgramState.cart.countDictionary.Select(
                pair =>
                {
                    Product product = pair.Key;
                    int count = pair.Value;

                    return new MenuItem(
                        // Название пункта меню - конвертированный в строку товар
                        $"{product.ToString()} ({count} шт)",
                        () =>
                        {
                            // Установка текущего продукта в корзине
                            ProgramState.productPairInCart = pair;

                            // Вывод меню текущего продукта в корзине
                            MenuManager.RunMenu(GetCartProductMenu());
                        }
                    );
                }
            )
        );

        List<MenuItem> additionalMenuItems = new();

        // Если в корзине есть продукты, то добавить новые пункты
        if (ProgramState.cart.countDictionary.Count > 0)
        {
            additionalMenuItems.Add(
                new MenuItem(
                    "Оплатить",
                    () =>
                    {
                        string additionalInfo =
                            $"К оплате: {ProgramState.cart.GetSum()}\n\nНа счету: {User.moneyAmount} руб\nБонусов: {User.bonusesAmount}\n";

                        string canBePayedStr =
                            ProgramState.cart.GetSum() > User.moneyAmount + User.bonusesAmount
                                ? "\nУ вас недостаточно средств, попробуйте убрать что-нибудь из корзины...\n"
                                : "";

                        MenuManager.RunMenu(
                            additionalInfo: additionalInfo + canBePayedStr,
                            menu: GetPaymentMenu()
                        );
                    },
                    MenuItem.Type.Secondary
                )
            );
            additionalMenuItems.Add(
                new MenuItem(
                    "Очистить корзину",
                    () =>
                        MenuManager.RunMenu(
                            additionalInfo: "Корзина очищена\n",
                            menu: GetMainMenu()
                        ),
                    MenuItem.Type.Secondary
                )
            );
        }

        // Создать меню с пунтами-продуктами и дополнительными
        return MenuManager.CreateSubMenu(
            new List<MenuItem>(primaryMenuItems.Concat(additionalMenuItems))
        );
    }

    // Меню взвешанного товара
    private static Menu GetWeighMenu()
    {
        return MenuManager.CreateSubMenu(
            new MenuItem(
                "Принять",
                () =>
                {
                    // Положить взвешанный товар в корзину
                    ProgramState.cart.PutProduct(WeighingMachine.weightingProduct ?? new());

                    // Вернуться в меню выбора товаров
                    MenuManager.RunMenu(
                        additionalInfo: $"Продукт добавлен в корзину, количество продуктов в корзине: {ProgramState.cart.GetCountOfProducts()}\n",
                        menu: GetProductsMenu()
                    );
                }
            )
        );
    }

    // Меню взвешиваемого товара
    private static Menu GetWeighableProductMenu()
    {
        return MenuManager.CreateSubMenu(
            new MenuItem(
                "Взвесить",
                () =>
                {
                    // Получение веса товара
                    WeighingMachine.GetWeight();

                    double weight = WeighingMachine.weightingProduct?.weight ?? 0;
                    double price = WeighingMachine.weightingProduct?.price ?? 0;

                    string titleLine = WeighingMachine.weightingProduct?.name ?? "";
                    string infoLine =
                        $"Вес: {Math.Round(weight, 2)} кг\nЦена: {Math.Round(weight, 2) * price}\n";

                    // Вывод меню полученного веса с дополнительной информацией
                    MenuManager.RunMenu(
                        additionalInfo: $"{titleLine}\n\n{infoLine}",
                        menu: GetWeighMenu()
                    );
                }
            )
        );
    }

    // Меню списка продуктов
    private static Menu GetProductsMenu()
    {
        return MenuManager.CreateSubMenu(
            new List<MenuItem>(
                // Проход по каждому продукту из каталога и конвертация его в пункт меню
                Catalog.products.Select(
                    product =>
                        new MenuItem(
                            // Название пункта меню - конвертированный в строку товар
                            product.ToString(),
                            () =>
                            {
                                // Если выбран взвешиваемый товар
                                if (product is WeighableProduct)
                                {
                                    // Установить текущий взвешиваемый товар
                                    WeighingMachine.PutProduct((WeighableProduct)product);

                                    // Вывести меню взвешиваемого товара
                                    MenuManager.RunMenu(GetWeighableProductMenu());
                                }
                                // Если выбран невзвешиваемый товар
                                else
                                {
                                    // Добавить его в корзину
                                    ProgramState.cart.PutProduct(product);

                                    // Вывести текущее меню еще раз
                                    MenuManager.RunMenu(
                                        additionalInfo: $"Продукт добавлен в корзину, количество продуктов в корзине: {ProgramState.cart.GetCountOfProducts()}\n",
                                        menu: GetProductsMenu()
                                    );
                                }
                            }
                        )
                )
            ),
            new MenuItem(
                "Корзина",
                () =>
                {
                    string additionalInfo =
                        ProgramState.cart.countDictionary.Count == 0 ? "Корзина пуста\n" : "";

                    MenuManager.RunMenu(additionalInfo: additionalInfo, menu: GetCartMenu());
                },
                MenuItem.Type.Secondary
            )
        );
    }

    // Меню информации о пользователе
    private static Menu GetInfoMenu()
    {
        return MenuManager.CreateSubMenu(
            new MenuItem(
                "Корзина",
                () =>
                {
                    string additionalInfo =
                        ProgramState.cart.countDictionary.Count == 0 ? "Корзина пуста\n" : "";

                    MenuManager.RunMenu(additionalInfo: additionalInfo, menu: GetCartMenu());
                },
                MenuItem.Type.Secondary
            )
        );
    }

    // Меню списка покупок
    private static Menu GetShoppingListMenu()
    {
        return MenuManager.CreateSubMenu(
            new MenuItem(
                "Корзина",
                () =>
                {
                    string additionalInfo =
                        ProgramState.cart.countDictionary.Count == 0 ? "Корзина пуста\n" : "";

                    MenuManager.RunMenu(additionalInfo: additionalInfo, menu: GetCartMenu());
                },
                MenuItem.Type.Secondary
            )
        );
    }

    // Главное меню
    private static Menu GetMainMenu()
    {
        return MenuManager.CreateMainMenu(
            new MenuItem(
                "Посмотреть информацию о себе",
                () =>
                {
                    string additionalInfo =
                        $"На счету: {User.moneyAmount} руб\n\nБонусная карта: {User.bonusCardNumber}\nКоличество бонусов: {User.bonusesAmount}\n";

                    MenuManager.RunMenu(additionalInfo: additionalInfo, menu: GetInfoMenu());
                }
            ),
            new MenuItem("Перейти к покупкам", () => MenuManager.RunMenu(GetProductsMenu())),
            new MenuItem(
                "Список покупок",
                () =>
                {
                    string shoppingList = "";

                    foreach (Product product in User.shoppingList)
                    {
                        shoppingList += $" - {product.ToString()}\n";
                    }

                    MenuManager.RunMenu(additionalInfo: shoppingList, menu: GetShoppingListMenu());
                },
                MenuItem.Type.Secondary
            ),
            new MenuItem(
                "Корзина",
                () =>
                {
                    string additionalInfo =
                        ProgramState.cart.countDictionary.Count == 0 ? "Корзина пуста\n" : "";

                    MenuManager.RunMenu(additionalInfo: additionalInfo, menu: GetCartMenu());
                },
                MenuItem.Type.Secondary
            )
        );
    }

    public static void Main()
    {
        // Вызов главного меню
        MenuManager.RunMenu(GetMainMenu());
    }
}
