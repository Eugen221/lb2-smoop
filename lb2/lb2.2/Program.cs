using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<string> fileNames = File.ReadAllLines("firstFile.txt").ToList();

        while (true)
        {
            Console.WriteLine("\nДоступні файли:");
            for (int i = 0; i < fileNames.Count; i++)
                Console.WriteLine($"{i + 1}. {fileNames[i]}");

            Console.Write("\nВиберіть файл (номер) або 0 для виходу: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice == 0)
                break;

            if (choice < 1 || choice > fileNames.Count)
            {
                Console.WriteLine("Невірний вибір!");
                continue;
            }

            string selectedFile = fileNames[choice - 1];
            AnalyzeFile(selectedFile);
        }
    }

    static void AnalyzeFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }

        string text = File.ReadAllText(filePath);
        var words = text.ToLower()
            .Split(new[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .Select(g => new { Word = g.Key, Count = g.Count() })
            .ToList();

        Console.WriteLine($"\nСтатистика для {filePath}:");
        using (StreamWriter writer = new StreamWriter($"{Path.GetFileNameWithoutExtension(filePath)}_stats.txt"))
        {
            foreach (var word in words)
            {
                Console.WriteLine($"Слово: {word.Word}, Кількість: {word.Count}");
                writer.WriteLine($"Слово: {word.Word}, Кількість: {word.Count}");
            }
        }
        Console.WriteLine($"Статистику збережено у {Path.GetFileNameWithoutExtension(filePath)}_stats.txt");
    }
}