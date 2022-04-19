namespace Store;

// Класс, генерирующий случайные данные
public static class Faker
{
    // Метод, генерирующий случайное целое число между заданным минимальным и максимальным значениями (включая оба)
    public static int GenerateInt32(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }

    // Метод, генерирующий список случайной длины, элементы берутся их заданного списка
    public static List<T> GenerateListFromOtherOne<T>(List<T> list)
    {
        int count = GenerateInt32(1, list.Count - 1);

        List<T> newList = new(ShuffleList(list));
        newList.RemoveRange(0, newList.Count - count);

        return newList;
    }

    // Метод, меняющий порядок элементов в заданном списке случайным образом
    private static List<T> ShuffleList<T>(List<T> list)
    {
        List<T> newList = new(list);
        Random random = new Random();

        for (int i = 0; i < list.Count; i++)
        {
            int j = random.Next(0, list.Count);

            T item = list[i];
            list[i] = list[j];
            list[j] = item;
        }

        return newList;
    }
}
