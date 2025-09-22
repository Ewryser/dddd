using PR1_SEM3_LOGIC;
using SEM3_PR1_MODEL;
using System;


namespace PR1_SEM3_CORE.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var logic = new PRLibraryLogic();


            //Инициализация строкового массива пунктов меню
            string[] menuItems = {"\n=== Коллекция пластинок ===",
                                  "1. Добавить пластинку:",
                                  "2. Показать все пластинки:",
                                  "3. Найти пластинку по Id:",
                                  "4. Редактировать характеристики пластинки:",
                                  "5. Удалить пластинку:",
                                  "6. Группировка пластинок по жанру:",
                                  "7. Поиск пластинки по году выпуска:",
                                  "8. Выход." };

            //Переменные выбора пункта меню
            int row = Console.CursorTop;
            int col = Console.CursorLeft;
            int index = 0;

            while (true)
            {
                DrawMenu(menuItems, row, col, index);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (index < menuItems.Length - 1) index++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        break;
                    case ConsoleKey.Enter:
                        switch (index)
                        {
                            case 8:
                                return;
                            default:
                                Console.WriteLine();
                                switch (index)
                                {
                                    case 1:
                                        int id = ReadUniqueId(logic);
                                        Console.Write("Название: ");
                                        string name = Console.ReadLine();
                                        Console.Write("Исполнитель: ");
                                        string musician = Console.ReadLine();
                                        Console.Write("Год выпуска: ");
                                        string year = Console.ReadLine();
                                        Console.Write("Жанр: ");
                                        string janre = Console.ReadLine();
                                        Console.Write("Лейбл: ");
                                        string symptoms = Console.ReadLine();
                                        Console.Write("Страна выпуска: ");
                                        string nativeRegion = Console.ReadLine();

                                        logic.AddRecord(id, name, musician, year, janre, symptoms, nativeRegion);
                                        Console.Write($"Пластинка '{name}' с ID '{id}' добавлена!");
                                        break;

                                    case 2:
                                        foreach (var p in logic.GetAllRecords())
                                        {
                                            Console.WriteLine(p);
                                        }
                                        break;

                                    case 3:
                                        Console.Write("Введите Id: ");
                                        int searchId = int.Parse(Console.ReadLine());
                                        var found = logic.GetRecordById(searchId);
                                        Console.WriteLine(found != null ? found.ToString() : "Пластинка не найдена.");
                                        break;

                                    case 4:
                                        Console.Write("Введите Id пластинки, характеристики которой Вы хотите изменить:\n");
                                        foreach (var b in logic.GetAllRecords())
                                            Console.WriteLine(b);

                                        if (!int.TryParse(Console.ReadLine(), out int updateId))
                                        {
                                            Console.WriteLine("Ошибка: Id должно быть числом!");
                                            break;
                                        }

                                        var recordToUpdate = logic.GetRecordById(updateId);
                                        if (recordToUpdate == null)
                                        {
                                            Console.WriteLine("Пластинка не найдена.");
                                            break;
                                        }

                                        Console.WriteLine($"Редактируем характеристики пластинки: {recordToUpdate}");

                                        bool editing = true;
                                        while (editing)
                                        {
                                            Console.WriteLine("\nЧто вы хотите изменить?");
                                            Console.WriteLine("1. Название");
                                            Console.WriteLine("2. Исполнителя");
                                            Console.WriteLine("3. Год");
                                            Console.WriteLine("4. Жанр");
                                            Console.WriteLine("5. Лейбл");
                                            Console.WriteLine("6. Страна выпуска");
                                            Console.WriteLine("7. Сохранить изменения и выйти");
                                            Console.Write("Ваш выбор: ");

                                            string editChoice = Console.ReadLine();
                                            switch (editChoice)
                                            {
                                                case "1":
                                                    Console.Write("Введите новое название: ");
                                                    recordToUpdate.Name = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "2":
                                                    Console.Write("Введите исполнителя: ");
                                                    recordToUpdate.Musician = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "3":
                                                    Console.Write("Введите год выпуска: ");
                                                    recordToUpdate.Year = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "4":
                                                    Console.Write("Введите жанр: ");
                                                    recordToUpdate.Janre = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "5":
                                                    Console.Write("Лейбл: ");
                                                    recordToUpdate.Laybel = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "6":
                                                    Console.Write("Страна исполнителя: ");
                                                    recordToUpdate.NativeRegion = Console.ReadLine();
                                                    Console.Clear();
                                                    break;

                                                case "7":
                                                    logic.UpdateRecord(recordToUpdate.Id, recordToUpdate.Name, recordToUpdate.Musician, recordToUpdate.Year, 
                                                        recordToUpdate.Janre, recordToUpdate.Laybel, recordToUpdate.NativeRegion);
                                                    Console.WriteLine("Изменения сохранены!");
                                                    editing = false; // выходим из цикла
                                                    Console.Clear();
                                                    break;

                                                default:
                                                    Console.WriteLine("Неверный выбор!");
                                                    Console.Clear();
                                                    break;
                                            }
                                        }
                                        break;

                                    case 5:
                                        Console.Write("Введите Id пластинки, которую хотите удалить:\n");
                                        foreach (var b in logic.GetAllRecords())
                                            Console.WriteLine(b);

                                        int deletedId = Convert.ToInt32(Console.ReadLine());

                                        bool deleted = logic.DeleteRecord(deletedId);
                                        Console.WriteLine(deleted ? "Пластинка удалена" : "Пластинка не найдена");
                                        break;

                                    case 6:
                                        var groups = logic.GroupByJanre();
                                        foreach (var g in groups)
                                        {
                                            Console.WriteLine($"\nЖанр: {g.Key}");
                                            foreach (var b in g.Value)
                                            {
                                                Console.WriteLine($" {b}");
                                            }
                                        }
                                        break;

                                    case 7:
                                        Console.Write("Введите год выпуска: ");
                                        string searchYear = Console.ReadLine();
                                        var result = logic.FindByYear(searchYear);
                                        if (result.Count == 0)
                                            Console.WriteLine("Пластинок, выпущенных в этот год, не найдено.");
                                        else
                                            result.ForEach(record => Console.WriteLine(record));
                                        break;
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;

                }
            }


        }

        //Проверка Id на уникальность.
        static int ReadUniqueId(PRLibraryLogic logic)
        {
            while(true)
            {
                Console.WriteLine("Введите уникальный ID: ");
                if (int.TryParse(Console.ReadLine(),out int id))
                {
                    if (!logic.ExistsId(id))
                    {
                        return id;
                    }
                    else
                    {
                        Console.WriteLine("Такой ID уже существует! Введите другой.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Введите число.");
                }
            }
        }

        //Переходы по меню
        static void DrawMenu(string[] menuItems, int row, int col, int index)
        {
            Console.SetCursorPosition(col, row);

            for (int i = 0; i < menuItems.Length; i++)
            {

                if (i == index)
                {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine(menuItems[i]); //Вывод позиций меню
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine("=================================");

        }
    }
}
