using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class File
{
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public int AccessCount { get; set; }
    public long Size { get; set; }

    public File(string name, DateTime creationDate, int accessCount, long size)
    {
        Name = name;
        CreationDate = creationDate;
        AccessCount = accessCount;
        Size = size;
    }

    public override string ToString()
    {
        return $"Ім'я: {Name}, Дата створення: {CreationDate:dd.MM.yyyy HH:mm}, Кількість звернень: {AccessCount}, Розмір: {Size} байт";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<File> files = new List<File>
        {
            new File("document1.txt", new DateTime(2023, 10, 1, 10, 0, 0), 5, 1024),
            new File("image.jpg", new DateTime(2023, 10, 5, 14, 30, 0), 12, 2048000),
            new File("report.pdf", new DateTime(2023, 10, 10, 9, 15, 0), 3, 512000)
        };

        while (true)
        {
            Console.WriteLine("\n1. Додати файл\n2. Редагувати файл\n3. Видалити файл\n4. Виконати запит\n5. Вийти");
            Console.Write("Виберіть опцію: ");
            string choice = Console.ReadLine();

            if (choice == "5") break;

            switch (choice)
            {
                case "1":
                    AddFile(files);
                    break;
                case "2":
                    EditFile(files);
                    break;
                case "3":
                    DeleteFile(files);
                    break;
                case "4":
                    QueryFiles(files);
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }

    static void AddFile(List<File> files)
    {
        Console.Write("Введіть ім'я файлу: ");
        string name = Console.ReadLine();
        Console.Write("Введіть дату створення (dd.MM.yyyy HH:mm): ");
        DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);
        Console.Write("Введіть кількість звернень: ");
        int accessCount = int.Parse(Console.ReadLine());
        Console.Write("Введіть розмір (байти): ");
        long size = long.Parse(Console.ReadLine());

        files.Add(new File(name, date, accessCount, size));
        Console.WriteLine("Файл додано!");
    }

    static void EditFile(List<File> files)
    {
        Console.Write("Введіть ім'я файлу для редагування: ");
        string name = Console.ReadLine();
        var file = files.FirstOrDefault(f => f.Name == name);
        if (file == null)
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }

        Console.Write("Нове ім'я (Enter, щоб залишити): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName)) file.Name = newName;

        Console.Write("Нова дата (dd.MM.yyyy HH:mm, Enter, щоб залишити): ");
        string dateInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(dateInput))
            file.CreationDate = DateTime.ParseExact(dateInput, "dd.MM.yyyy HH:mm", null);

        Console.Write("Нова кількість звернень (Enter, щоб залишити): ");
        string accessInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(accessInput)) file.AccessCount = int.Parse(accessInput);

        Console.Write("Новий розмір (Enter, щоб залишити): ");
        string sizeInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(sizeInput)) file.Size = long.Parse(sizeInput);

        Console.WriteLine("Файл відредаговано!");
    }

    static void DeleteFile(List<File> files)
    {
        Console.Write("Введіть ім'я файлу для видалення: ");
        string name = Console.ReadLine();
        var file = files.FirstOrDefault(f => f.Name == name);
        if (file == null)
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }
        files.Remove(file);
        Console.WriteLine("Файл видалено!");
    }

    static void QueryFiles(List<File> files)
    {
        Console.WriteLine("Введіть початкову дату (формат: dd.MM.yyyy HH:mm):");
        DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);

        Console.WriteLine("Введіть кінцеву дату (формат: dd.MM.yyyy HH:mm):");
        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);

        var filteredFiles = files
            .Where(f => f.CreationDate >= startDate && f.CreationDate <= endDate)
            .OrderByDescending(f => f.AccessCount)
            .ToList();

        Console.WriteLine("\nФайли, створені в заданому діапазоні (відсортовані за кількістю звернень):");
        if (filteredFiles.Count == 0)
        {
            Console.WriteLine("Немає файлів у заданому діапазоні.");
        }
        else
        {
            using (StreamWriter writer = new StreamWriter("query_result.txt"))
            {
                foreach (var file in filteredFiles)
                {
                    Console.WriteLine(file);
                    writer.WriteLine(file);
                }
            }
            Console.WriteLine("Результати також збережено у query_result.txt");
        }
    }
}