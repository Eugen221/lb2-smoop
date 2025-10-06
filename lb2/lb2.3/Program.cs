using System;
using System.Collections.Generic;
using System.IO;

public class PrintJob
{
    public string User { get; set; }
    public string DocumentName { get; set; }
    public int Priority { get; set; } // 1 (lowest) to 5 (highest)
    public DateTime RequestTime { get; set; }

    public PrintJob(string user, string documentName, int priority)
    {
        User = user;
        DocumentName = documentName;
        Priority = Math.Clamp(priority, 1, 5);
        RequestTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Користувач: {User}, Документ: {DocumentName}, Пріоритет: {Priority}, Час: {RequestTime:dd.MM.yyyy HH:mm:ss}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var printQueue = new PriorityQueue<PrintJob, int>();
        var printHistory = new List<PrintJob>();

        while (true)
        {
            Console.WriteLine("\n1. Додати завдання друку\n2. Обробити чергу\n3. Показати історію\n4. Зберегти історію у файл\n5. Вийти");
            Console.Write("Виберіть опцію: ");
            string choice = Console.ReadLine();

            if (choice == "5") break;

            switch (choice)
            {
                case "1":
                    AddPrintJob(printQueue);
                    break;
                case "2":
                    ProcessQueue(printQueue, printHistory);
                    break;
                case "3":
                    ShowHistory(printHistory);
                    break;
                case "4":
                    SaveHistory(printHistory);
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }

    static void AddPrintJob(PriorityQueue<PrintJob, int> queue)
    {
        Console.Write("Введіть ім'я користувача: ");
        string user = Console.ReadLine();
        Console.Write("Введіть назву документа: ");
        string docName = Console.ReadLine();
        Console.Write("Введіть пріоритет (1-5): ");
        if (!int.TryParse(Console.ReadLine(), out int priority))
            priority = 1;

        var job = new PrintJob(user, docName, priority);
        queue.Enqueue(job, -priority); // Negative for higher priority first
        Console.WriteLine("Завдання додано до черги!");
    }

    static void ProcessQueue(PriorityQueue<PrintJob, int> queue, List<PrintJob> history)
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня!");
            return;
        }

        Console.WriteLine("\nОбробка черги:");
        while (queue.TryDequeue(out PrintJob job, out _))
        {
            Console.WriteLine($"Друкується: {job}");
            history.Add(job);
        }
    }

    static void ShowHistory(List<PrintJob> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("Історія порожня!");
            return;
        }

        Console.WriteLine("\nІсторія друку:");
        foreach (var job in history)
        {
            Console.WriteLine(job);
        }
    }

    static void SaveHistory(List<PrintJob> history)
    {
        using (StreamWriter writer = new StreamWriter("print_history.txt"))
        {
            foreach (var job in history)
            {
                writer.WriteLine(job);
            }
        }
        Console.WriteLine("Історію збережено у print_history.txt");
    }
}