using System;
using System.Text.RegularExpressions;
class Program
{
    static int[,] twoDArray; // двумерный массив
    static int[][] jaggedArray; // Рваный массив
    static string inputString = "";  // Строка для обработки

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите задачу:");
            Console.WriteLine("1. Работа с двумерным массивом");
            Console.WriteLine("2. Работа с рваным массивом");
            Console.WriteLine("3. Обработка строки");
            Console.WriteLine("4. Выход");
            
            Console.Write("Введите номер задачи: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Handle2DArray();
                    break;
                case "2":
                    HandleJaggedArray();
                    break;
                case "3":
                    HandleStringProcessing();
                    break;
                case "4":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    static void Handle2DArray()
    {
        Console.Clear();
        Console.WriteLine("--- Работа с двумерным массивом ---");
        Console.WriteLine("1. Создать массив вручную");
        Console.WriteLine("2. Создать массив случайным образом");
        Console.WriteLine("3. Добавить столбец с заданным номером");
        Console.WriteLine("4. Распечатать массив");
        Console.WriteLine("5. Вернуться в главное меню");
        Console.Write("Введите номер действия: ");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Create2DArrayManually(ref twoDArray);
                break;
            case "2":
                Create2DArrayRandomly(ref twoDArray);
                break;
            case "3":
                AddColumnTo2DArray(ref twoDArray);
                break;
            case "4":
                Print2DArray(twoDArray);
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

    static void Create2DArrayManually(ref int[,] array)
    {
        Console.Clear();
        Console.WriteLine("--- Создание двумерного массива вручную ---");

        // Ограничения на количество строк и столбцов
        int maxRows = 10; // Максимум 10 строк
        int maxCols = 10; // Максимум 10 столбцов

        // Ввод количества строк с проверкой на допустимость
        int rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        while (rows <= 0 || rows > maxRows)
        {
            Console.WriteLine($"Ошибка: количество строк должно быть от 1 до {maxRows}.");
            rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        }

        // Ввод количества столбцов с проверкой на допустимость
        int cols = GetValidInteger($"Введите количество столбцов (не более {maxCols}): ");
        while (cols <= 0 || cols > maxCols)
        {
            Console.WriteLine($"Ошибка: количество столбцов должно быть от 1 до {maxCols}.");
            cols = GetValidInteger($"Введите количество столбцов (не более {maxCols}): ");
        }

        // Создание массива с заданными размерами
        array = new int[rows, cols];

        // Ввод элементов массива
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = GetValidInteger($"Введите элемент [{i+1}, {j+1}]: ");
            }
        }

        Console.WriteLine("Массив успешно создан.");
        Print2DArray(array);
    }
    
    static void Create2DArrayRandomly(ref int[,] array)
    {
        Console.Clear();
        Console.WriteLine("--- Создание двумерного массива случайным образом ---");

        // Ограничения на количество строк и столбцов
        int maxRows = 10; // Максимум 10 строк
        int maxCols = 10; // Максимум 10 столбцов

        // Ввод количества строк с проверкой на допустимость
        int rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        while (rows <= 0 || rows > maxRows)
        {
            Console.WriteLine($"Ошибка: количество строк должно быть от 1 до {maxRows}.");
            rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        }

        // Ввод количества столбцов с проверкой на допустимость
        int cols = GetValidInteger($"Введите количество столбцов (не более {maxCols}): ");
        while (cols <= 0 || cols > maxCols)
        {
            Console.WriteLine($"Ошибка: количество столбцов должно быть от 1 до {maxCols}.");
            cols = GetValidInteger($"Введите количество столбцов (не более {maxCols}): ");
        }

        // Создание массива с заданными размерами
        array = new int[rows, cols];
        Random rand = new Random();

        // Заполнение массива случайными числами
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = rand.Next(1, 101); // Заполнение случайными числами от 1 до 100
            }
        }

        Console.WriteLine("Массив случайным образом создан.");
        Print2DArray(array);
    }
    
    static void AddColumnTo2DArray(ref int[,] array)
    {
        Console.Clear();
        Console.WriteLine("--- Добавление столбца в двумерный массив ---");

        if (array == null)
        {
            Console.WriteLine("Массив не создан.");
            return;
        }

        // Запрос индекса столбца для добавления (с нумерацией с 1)
        int newColIndex = GetValidInteger("Введите номер столбца для добавления (нумерация с 1): ");

        // Преобразуем индекс в стандартный для массива (начинающийся с 0)
        newColIndex--; // Вычитаем 1, чтобы преобразовать в индекс массива, начиная с 0

        // Проверяем, чтобы индекс был в допустимом диапазоне
        if (newColIndex < 0 || newColIndex > array.GetLength(1))
        {
            Console.WriteLine("Некорректный индекс столбца.");
            return;
        }

        // Создаем новый массив с дополнительным столбцом
        int[,] newArray = new int[array.GetLength(0), array.GetLength(1) + 1];

        // Копируем старые данные в новый массив, добавляя новый столбец
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < newColIndex; j++)
            {
                newArray[i, j] = array[i, j];
            }
            // Добавляем пустой столбец с заданным значением (по умолчанию 0)
            newArray[i, newColIndex] = 0;
            for (int j = newColIndex; j < array.GetLength(1); j++)
            {
                newArray[i, j + 1] = array[i, j];
            }
        }

        // Переназначаем ссылку на новый массив
        array = newArray;

        Console.WriteLine("Столбец успешно добавлен.");
        Print2DArray(array);
    }

    static int GetValidInteger(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректное целое число.");
            }
        }
    }

    static void Print2DArray(int[,] array)
    {
        if (array == null)
        {
            Console.WriteLine("Массив не создан.");
            return;
        }

        Console.WriteLine("Содержимое массива:");
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
// ---------------------------------------------------------------------------------------------------------------------
    static void HandleJaggedArray()
    {
        Console.Clear();
        Console.WriteLine("--- Работа с рваным массивом ---");
        Console.WriteLine("1. Создать массив вручную");
        Console.WriteLine("2. Создать массив случайно");
        Console.WriteLine("3. Удалить самую короткую строку");
        Console.WriteLine("4. Распечатать массив");
        Console.WriteLine("5. Вернуться в главное меню");
        Console.Write("Введите номер действия: ");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                CreateJaggedArrayManually(ref jaggedArray);
                break;
            case "2":
                CreateJaggedArrayRandomly(ref jaggedArray);
                break;
            case "3":
                RemoveShortestRow(ref jaggedArray);
                break;
            case "4":
                PrintJaggedArray(jaggedArray);
                break;
            case "5":
                return; // Возвращаемся в главное меню
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }
    static void CreateJaggedArrayManually(ref int[][] array)
    {
        Console.Clear();
        Console.WriteLine("--- Создание рваного массива вручную ---");

        // Ограничения на количество строк и длину строк
        int maxRows = 10;  // Максимум 10 строк
        int maxCols = 10;  // Максимум 10 элементов в каждой строке

        // Ввод количества строк с проверкой на допустимость
        int rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        while (rows <= 0 || rows > maxRows)
        {
            Console.WriteLine($"Ошибка: количество строк должно быть от 1 до {maxRows}.");
            rows = GetValidInteger($"Введите количество строк (не более {maxRows}): ");
        }

        // Создаем рваный массив
        array = new int[rows][];

        // Ввод элементов в строках
        for (int i = 0; i < rows; i++)
        {
            int cols = GetValidInteger($"Введите количество элементов в строке {i + 1} (не более {maxCols}): ");
            while (cols <= 0 || cols > maxCols)
            {
                Console.WriteLine($"Ошибка: количество элементов в строке должно быть от 1 до {maxCols}.");
                cols = GetValidInteger($"Введите количество элементов в строке {i + 1} (не более {maxCols}): ");
            }

            array[i] = new int[cols];

            // Ввод значений элементов строки
            for (int j = 0; j < cols; j++)
            {
                array[i][j] = GetValidInteger($"Введите элемент [{i + 1}, {j + 1}]: ");
            }
        }

        Console.WriteLine("Рваный массив успешно создан.");
        PrintJaggedArray(array);
    }
    
    static void CreateJaggedArrayRandomly(ref int[][] array)
    {
        Console.Clear();
        Console.WriteLine("--- Случайное создание рваного массива ---");

        // Ограничения на количество строк и длину строк
        int maxRows = 10;  // Максимум 10 строк
        int maxCols = 10;  // Максимум 10 элементов в каждой строке

        // Генерируем случайное количество строк (от 1 до maxRows)
        Random rand = new Random();
        int rows = rand.Next(1, maxRows + 1);

        // Создаем рваный массив
        array = new int[rows][];

        // Для каждой строки генерируем случайное количество элементов (от 1 до maxCols)
        for (int i = 0; i < rows; i++)
        {
            int cols = rand.Next(1, maxCols + 1);  // Случайное количество элементов в строке
            array[i] = new int[cols];

            // Заполняем строку случайными числами от 1 до 100
            for (int j = 0; j < cols; j++)
            {
                array[i][j] = rand.Next(1, 101);  // Случайное число от 1 до 100
            }
        }

        Console.WriteLine("Рваный массив случайным образом создан.");
        PrintJaggedArray(array);
    }
    
    static void RemoveShortestRow(ref int[][] array)
    {
        if (array == null)
        {
            Console.WriteLine("Рваный массив не создан.");
            return;
        }

        int shortestRowIndex = 0;
        int shortestRowLength = array[0].Length;

        // Находим индекс самой короткой строки
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].Length < shortestRowLength)
            {
                shortestRowIndex = i;
                shortestRowLength = array[i].Length;
            }
        }

        // Создаем новый массив, который будет содержать все строки, кроме самой короткой
        int[][] newJaggedArray = new int[array.Length - 1][];
        int newIndex = 0;

        // Копируем все строки, кроме самой короткой
        for (int i = 0; i < array.Length; i++)
        {
            if (i != shortestRowIndex)
            {
                newJaggedArray[newIndex] = array[i];
                newIndex++;
            }
        }

        // Переназначаем ссылку на новый массив
        array = newJaggedArray;

        Console.WriteLine($"Самая короткая строка: {shortestRowIndex+1} - успешно удалена.");
        PrintJaggedArray(array);
    }
    
    // Метод для распечатки рваного массива
    static void PrintJaggedArray(int[][] array)
    {
        if (array == null)
        {
            Console.WriteLine("Рваный массив не создан.");
            return;
        }

        Console.WriteLine("Содержимое рваного массива:");
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                Console.Write(array[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
    // ---------------------------------------------------------------------------------------------------------------------

    static void HandleStringProcessing()
    {
        Console.Clear();
        Console.WriteLine("--- Обработка строки ---");
        Console.WriteLine("1. Ввести строку с клавиатуры");
        Console.WriteLine("2. Использовать тестовую строку");
        Console.WriteLine("3. Перевернуть четные слова");
        Console.WriteLine("4. Распечатать строку");
        Console.WriteLine("5. Вернуться в главное меню");
        Console.Write("Введите номер действия: ");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                EnterStringFromKeyboard(ref inputString);
                break;
            case "2":
                UseTestString();
                break;
            case "3":
                ReverseEvenWords(ref inputString);
                break;
            case "4":
                if (string.IsNullOrEmpty(inputString)) Console.WriteLine("Строка пустая.");
                else Console.WriteLine(inputString);
                break;
            case "5":
                return; // Возвращаемся в главное меню
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }
    
    static void EnterStringFromKeyboard(ref string input)
    {
        Console.Clear();
        Console.WriteLine("Введите строку для обработки:");
        input = Console.ReadLine();
        Console.WriteLine($"Введенная строка: {input}");
    }
    
    static void UseTestString()
    {
        string constString = "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.";
        string tempConstString = constString;
        ReverseEvenWords(ref constString);
        constString = tempConstString;
    }
    
    static void ReverseEvenWords(ref string input)
    {
        char[] punctuationMarks = { '.', '!', '?' };
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Строка не введена.");
            return;
        }
        else if (!punctuationMarks.Contains(input[^1]))
        {
            Console.WriteLine($"Строка должна заканчиваться одним из знаков препинания: {string.Join(", ", punctuationMarks)}");
            return;
        }

        // Разделяем строку на слова, включая пробелы и знаки препинания
        string[] parts = Regex.Split(input, @"(\s+|[.,;:!?])");

        int wordIndex = 0; // Индекс для подсчета только слов (пропускаем пробелы и знаки препинания)
        for (int i = 0; i < parts.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(parts[i]) || Regex.IsMatch(parts[i], @"[.,;:!?]"))
            {
                continue; // Пропускаем пробелы и знаки препинания
            }

            if (wordIndex % 2 == 1) // Четное слово (по человеческой нумерации: 2, 4, ...)
            {
                char[] wordArray = parts[i].ToCharArray();
                Array.Reverse(wordArray); // Переворачиваем слово
                parts[i] = new string(wordArray); // Присваиваем перевернутое слово обратно
            }

            wordIndex++;
        }

        // Собираем строку обратно
        string result = string.Join("", parts);
        input = result;
        Console.WriteLine($"Результат: {input}");
    }
}
