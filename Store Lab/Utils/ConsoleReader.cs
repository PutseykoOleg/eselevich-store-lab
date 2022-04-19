namespace Store;

// Класс, описывающий методы чтения значений из консоли
public static class ConsoleReader
{
    // Метод, считывающий из консоли целое число между заданным минимальным и максимальным значениями (включая оба)
    public static int ReadInt32(string inputLine, string errorLine, int min, int max)
    {
        // Считанное значение
        int result = default;

        // Является ли введенное значение валидным
        bool isInputValid = false;

        do
        {
            // Вывести строку с предложением о вводе
            Console.Write($"\n{inputLine}: ");

            // Считанное значение
            string? numberStr = Console.ReadLine();

            // Попытка спарсить введенное значение в целое число
            int parseResult = 0;
            bool isInputNumber = numberStr != null && Int32.TryParse(numberStr, out parseResult);

            // Если попытка была удачной
            if (isInputNumber)
            {
                // Инициализировать считанное значение
                result = Int32.Parse(numberStr!);
                // Проверить входит ли оно в границы
                isInputValid = result >= min && result <= max;
            }

            // Если не входит или попытка парсинга была неудачной
            if (!isInputValid)
            {
                // Вывести сообщение об ошибке
                Console.WriteLine($"\n{errorLine}");
            }
        }
        // Продолжать так до тех пор, пока считанное значение не будет валидным
        while (!isInputValid);

        return result;
    }

    // Метод, считывающий из консоли значение "да" или "нет" и возващающий соответствующее булевое значение
    public static bool ReadBool(string inputLine, string errorLine)
    {
        // Считанное значение
        bool result = default;

        // Является ли введенное значение валидным
        bool isInputValid = false;

        do
        {
            // Вывести строку с предложением о вводе
            Console.Write($"\n{inputLine}: ");

            // Считанное значение
            string? str = Console.ReadLine();
            isInputValid = str == "y" || str == "n" || str == "Y" || str == "N";

            // Если введенное значение валидно
            if (isInputValid)
            {
                // Инициализировать считанное значение
                result = str == "y" || str == "Y" ? true : false;
            }
            else
            {
                // Вывести сообщение об ошибке
                Console.WriteLine($"\n{errorLine}");
            }
        }
        // Продолжать так до тех пор, пока считанное значение не будет валидным
        while (!isInputValid);

        return result;
    }
}
