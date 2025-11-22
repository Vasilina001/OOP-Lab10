using System;
using SimpleClassLibrary;

class Program
{
    static void Main(string[] args)
    {
        Worker[] workers = null;
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== Система управління працівниками ===");
            Console.WriteLine("1. Створити масив працівників");
            Console.WriteLine("2. Показати всіх працівників");
            Console.WriteLine("3. Показати конкретного працівника");
            Console.WriteLine("4. Розрахувати стаж роботи");
            Console.WriteLine("5. Перевірити проживання біля головного офісу");
            Console.WriteLine("6. Ввести премію для працівника");
            Console.WriteLine("7. Показати премії всіх працівників");
            Console.WriteLine("8. Вийти");
            Console.Write("Оберіть опцію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    workers = CreateWorkersArray();
                    break;
                case "2":
                    DisplayAllWorkers(workers);
                    break;
                case "3":
                    DisplaySpecificWorker(workers);
                    break;
                case "4":
                    CalculateWorkExperience(workers);
                    break;
                case "5":
                    CheckLivingNearOffice(workers);
                    break;
                case "6":
                    InputBonusForWorker(workers);
                    break;
                case "7":
                    DisplayAllBonuses(workers);
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Невірна опція. Натисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Статичний метод для створення масиву працівників
    public static Worker[] CreateWorkersArray()
    {
        Console.Write("Введіть кількість працівників: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.Write("Будь ласка, введіть коректне додатне число: ");
        }

        Worker[] workers = new Worker[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведення даних для працівника #{i + 1}:");

            Console.Write("Повне ім'я: ");
            string fullName = Console.ReadLine();

            Console.Write("Місто проживання: ");
            string homeCity = Console.ReadLine();

            Console.Write("Дата початку роботи (рррр-мм-дд): ");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("Будь ласка, введіть коректну дату (рррр-мм-дд): ");
            }

            Console.WriteLine("\nІнформація про компанію:");
            Console.Write("Назва компанії: ");
            string companyName = Console.ReadLine();

            Console.Write("Місто головного офісу: ");
            string mainOfficeCity = Console.ReadLine();

            Console.Write("Посада: ");
            string position = Console.ReadLine();

            Console.Write("Зарплата: ");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.Write("Будь ласка, введіть коректну зарплату: ");
            }

            Console.Write("Працює на повний робочий день (так/ні): ");
            string fullTimeInput = Console.ReadLine().ToLower();
            bool isFullTime = fullTimeInput == "так" || fullTimeInput == "true" || fullTimeInput == "1";

            Company company = new Company(companyName, mainOfficeCity, position, salary, isFullTime);
            workers[i] = new Worker(fullName, homeCity, startDate, company);

            // Запит на введення премії
            Console.Write("\nБажаєте ввести премію для цього працівника? (так/ні): ");
            string inputBonus = Console.ReadLine().ToLower();
            if (inputBonus == "так" || inputBonus == "1")
            {
                workers[i].InputBonus();
            }
        }

        Console.WriteLine($"\nУспішно створено {n} працівників. Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
        return workers;
    }

    // Статичний метод для відображення конкретного працівника
    public static void DisplayWorker(Worker worker)
    {
        if (worker == null)
        {
            Console.WriteLine("Працівник не існує.");
            return;
        }

        Console.WriteLine("=== Деталі працівника ===");
        Console.WriteLine(worker.ToString());
        Console.WriteLine($"Стаж роботи: {worker.GetWorkExperience()} місяців");
        Console.WriteLine($"Мешкає біля головного офісу: {(worker.LivesNotFarFromTheMainOffice() ? "ТАК" : "НІ")}");
        Console.WriteLine("========================");
    }

    // Статичний метод для відображення всіх працівників
    public static void DisplayAllWorkers(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
        }
        else
        {
            Console.WriteLine($"\n=== Всі працівники ({workers.Length} всього) ===");
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"\nПрацівник #{i + 1}:");
                DisplayWorker(workers[i]);
            }
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }

    // Додаткові методи для меню
    private static void DisplaySpecificWorker(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
            Console.ReadKey();
            return;
        }

        Console.Write($"Введіть номер працівника (1-{workers.Length}): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= workers.Length)
        {
            DisplayWorker(workers[index - 1]);
        }
        else
        {
            Console.WriteLine("Невірний номер.");
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }

    private static void CalculateWorkExperience(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n=== Стаж роботи ===");
        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.FullName}: {worker.GetWorkExperience()} місяців");
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }

    private static void CheckLivingNearOffice(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n=== Проживання біля головного офісу ===");
        foreach (var worker in workers)
        {
            string status = worker.LivesNotFarFromTheMainOffice() ? "ТАК" : "НІ";
            Console.WriteLine($"{worker.FullName}: {status}");
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }

    // Новий метод для введення премії
    private static void InputBonusForWorker(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
            Console.ReadKey();
            return;
        }

        Console.Write($"Введіть номер працівника для введення премії (1-{workers.Length}): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= workers.Length)
        {
            workers[index - 1].InputBonus();
        }
        else
        {
            Console.WriteLine("Невірний номер.");
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }

    // Новий метод для відображення всіх премій
    private static void DisplayAllBonuses(Worker[] workers)
    {
        if (workers == null || workers.Length == 0)
        {
            Console.WriteLine("Немає доступних працівників. Будь ласка, спочатку створіть масив працівників.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n=== Премії всіх працівників ===");
        for (int i = 0; i < workers.Length; i++)
        {
            Console.WriteLine($"\nПрацівник #{i + 1}: {workers[i].FullName}");
            workers[i].DisplayBonus();
        }
        Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
        Console.ReadKey();
    }
}