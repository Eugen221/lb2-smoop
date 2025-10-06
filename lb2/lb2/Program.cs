using System;
using System.Collections.Generic;
using System.Linq;


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
        return $"Iм'я: {Name}, Дата створення: {CreationDate:dd.MM.yyyy HH:mm}, Кiлькiсть звернень: {AccessCount}, Розмiр: {Size} байт";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення масиву об'єктів типу File
        File[] files = new File[]
        {
            new File("document1.txt", new DateTime(2023, 10, 1, 10, 0, 0), 5, 1024),
            new File("image.jpg", new DateTime(2023, 10, 5, 14, 30, 0), 12, 2048000),
            new File("report.pdf", new DateTime(2023, 10, 10, 9, 15, 0), 3, 512000),
            new File("script.js", new DateTime(2023, 10, 15, 16, 45, 0), 8, 4096),
            new File("video.mp4", new DateTime(2023, 10, 20, 11, 20, 0), 20, 104857600),
            new File("archive.zip", new DateTime(2023, 10, 25, 18, 10, 0), 1, 5242880),
            new File("config.ini", new DateTime(2023, 11, 1, 8, 5, 0), 15, 256),
            new File("data.csv", new DateTime(2023, 11, 5, 13, 40, 0), 7, 8192)
        };

        Console.WriteLine("Введiть початкову дату (формат: dd.MM.yyyy HH:mm):");
        DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);

        Console.WriteLine("Введiть кiнцеву дату (формат: dd.MM.yyyy HH:mm):");
        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);

        // Фільтрація файлів у заданому діапазоні та сортування за кількістю звернень (зменшення)
        var filteredFiles = files
            .Where(f => f.CreationDate >= startDate && f.CreationDate <= endDate)
            .OrderByDescending(f => f.AccessCount)
            .ToArray();

        Console.WriteLine("\nФайли, створенi в заданому дiапазонi (вiдсортованi за кiлькiстю звернень):");
        if (filteredFiles.Length == 0)
        {
            Console.WriteLine("Немає файлiв у заданому дiапазонi.");
        }
        else
        {
            foreach (var file in filteredFiles)
            {
                Console.WriteLine(file);
            }
        }

        Console.ReadKey();
    }
}