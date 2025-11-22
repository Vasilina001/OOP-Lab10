using System;
using System.Collections.Generic;
using System.Linq;
using SimpleClassLibrary;

namespace DictionaryWorkerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryWorkerManager manager = new DictionaryWorkerManager();
            manager.Run();
        }
    }

    public class DictionaryWorkerManager
    {
        private Dictionary<int, Worker> workersDictionary;
        private int nextId;

        public DictionaryWorkerManager()
        {
            workersDictionary = new Dictionary<int, Worker>();
            nextId = 1;
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ПРАЦІВНИКАМИ З Dictionary ===");
                Console.WriteLine("1. Створити та додати працівника");
                Console.WriteLine("2. Показати всіх працівників");
                Console.WriteLine("3. Пошук працівника за ID");
                Console.WriteLine("4. Пошук працівника за іменем");
                Console.WriteLine("5. Видалити працівника");
                Console.WriteLine("6. Оновити дані працівника");
                Console.WriteLine("7. Розрахувати стаж роботи");
                Console.WriteLine("8. Перевірити проживання біля офісу");
                Console.WriteLine("9. Ввести премію для працівника");
                Console.WriteLine("10. Показати премії всіх працівників");
                Console.WriteLine("11. Статистика");
                Console.WriteLine("12. Очистити словник");
                Console.WriteLine("13. Вихід");
                Console.Write("Оберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddWorker();
                        break;
                    case "2":
                        DisplayAllWorkers();
                        break;
                    case "3":
                        FindWorkerById();
                        break;
                    case "4":
                        FindWorkerByName();
                        break;
                    case "5":
                        RemoveWorker();
                        break;
                    case "6":
                        UpdateWorker();
                        break;
                    case "7":
                        CalculateWorkExperience();
                        break;
                    case "8":
                        CheckLivingNearOffice();
                        break;
                    case "9":
                        InputBonusForWorker();
                        break;
                    case "10":
                        DisplayAllBonuses();
                        break;
                    case "11":
                        ShowStatistics();
                        break;
                    case "12":
                        ClearDictionary();
                        break;
                    case "13":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірна опція. Натисніть будь-яку клавішу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // 1. Створення об'єкта класу-колекції та додавання елементів
        private void AddWorker()
        {
            Console.WriteLine("\n=== Створення нового працівника ===");

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
            Worker worker = new Worker(fullName, homeCity, startDate, company);

            // Додавання премії
            Console.Write("\nБажаєте ввести премію для цього працівника? (так/ні): ");
            string inputBonus = Console.ReadLine().ToLower();
            if (inputBonus == "так" || inputBonus == "1")
            {
                worker.InputBonus();
            }

            // Додавання до Dictionary
            workersDictionary.Add(nextId, worker);
            Console.WriteLine($"\n Працівника додано з ID: {nextId}");
            Console.WriteLine($" Загальна кількість працівників: {workersDictionary.Count}");
            nextId++;

            Console.ReadKey();
        }

        // 2. Відображення всіх елементів
        private void DisplayAllWorkers()
        {
            Console.WriteLine($"\n=== ВСІ ПРАЦІВНИКИ ({workersDictionary.Count} всього) ===");
            
            if (workersDictionary.Count == 0)
            {
                Console.WriteLine("Словник порожній");
            }
            else
            {
                foreach (var kvp in workersDictionary)
                {
                    Console.WriteLine($"\n--- Працівник ID: {kvp.Key} ---");
                    DisplayWorkerInfo(kvp.Value);
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.ReadKey();
        }

        // 3. Пошук елементів
        private void FindWorkerById()
        {
            Console.Write("Введіть ID працівника для пошуку: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невірний ID!");
                Console.ReadKey();
                return;
            }

            // Використання TryGetValue для безпечного пошуку
            if (workersDictionary.TryGetValue(id, out Worker worker))
            {
                Console.WriteLine($"\n Знайдено працівника з ID: {id}");
                DisplayWorkerInfo(worker);
            }
            else
            {
                Console.WriteLine($" Працівник з ID {id} не знайдений");
            }

            Console.ReadKey();
        }

        private void FindWorkerByName()
        {
            Console.Write("Введіть ім'я або частину імені для пошуку: ");
            string searchName = Console.ReadLine().ToLower();

            // Використання LINQ для пошуку
            var foundWorkers = workersDictionary
                .Where(kvp => kvp.Value.FullName.ToLower().Contains(searchName))
                .ToList();

            Console.WriteLine($"\n=== РЕЗУЛЬТАТИ ПОШУКУ ЗА ІМЕНЕМ '{searchName}' ===");
            
            if (foundWorkers.Count == 0)
            {
                Console.WriteLine("Працівників не знайдено");
            }
            else
            {
                foreach (var kvp in foundWorkers)
                {
                    Console.WriteLine($"\n--- Працівник ID: {kvp.Key} ---");
                    Console.WriteLine($" Повне ім'я: {kvp.Value.FullName}");
                    Console.WriteLine($" Місто: {kvp.Value.HomeCity}");
                    Console.WriteLine($" Посада: {kvp.Value.WorkPlace.Position}");
                    Console.WriteLine($" Зарплата: {kvp.Value.WorkPlace.Salary:C}");
                }
                Console.WriteLine($"\nЗнайдено: {foundWorkers.Count} працівників");
            }

            Console.ReadKey();
        }

        // 4. Вилучення елементів
        private void RemoveWorker()
        {
            Console.Write("Введіть ID працівника для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(" Невірний ID!");
                Console.ReadKey();
                return;
            }

            if (workersDictionary.Remove(id))
            {
                Console.WriteLine($" Працівник з ID {id} успішно видалений");
                Console.WriteLine($" Залишилось працівників: {workersDictionary.Count}");
            }
            else
            {
                Console.WriteLine($" Працівник з ID {id} не знайдений");
            }

            Console.ReadKey();
        }

        // Оновлення даних працівника
        private void UpdateWorker()
        {
            Console.Write("Введіть ID працівника для оновлення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(" Невірний ID!");
                Console.ReadKey();
                return;
            }

            if (!workersDictionary.ContainsKey(id))
            {
                Console.WriteLine($" Працівник з ID {id} не знайдений");
                Console.ReadKey();
                return;
            }

            Worker worker = workersDictionary[id];
            Console.WriteLine($"\nОновлення працівника: {worker.FullName}");

            Console.Write("Нове повне ім'я (залиште порожнім, щоб не змінювати): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                worker.FullName = newName;
            }

            Console.Write("Нове місто проживання (залиште порожнім, щоб не змінювати): ");
            string newCity = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCity))
            {
                worker.HomeCity = newCity;
            }

            Console.WriteLine(" Дані працівника оновлено!");
            Console.ReadKey();
        }

        // Додаткові методи
        private void CalculateWorkExperience()
        {
            Console.WriteLine("\n=== СТАЖ РОБОТИ ===");
            
            if (workersDictionary.Count == 0)
            {
                Console.WriteLine("Словник порожній");
            }
            else
            {
                foreach (var kvp in workersDictionary)
                {
                    Console.WriteLine($"{kvp.Value.FullName}: {kvp.Value.GetWorkExperience()} місяців");
                }
            }

            Console.ReadKey();
        }

        private void CheckLivingNearOffice()
        {
            Console.WriteLine("\n=== ПРОЖИВАННЯ БІЛЯ ГОЛОВНОГО ОФІСУ ===");
            
            if (workersDictionary.Count == 0)
            {
                Console.WriteLine("Словник порожній");
            }
            else
            {
                foreach (var kvp in workersDictionary)
                {
                    string status = kvp.Value.LivesNotFarFromTheMainOffice() ? " ТАК" : " НІ";
                    Console.WriteLine($"{kvp.Value.FullName}: {status}");
                }
            }

            Console.ReadKey();
        }

        private void InputBonusForWorker()
        {
            Console.Write("Введіть ID працівника для введення премії: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(" Невірний ID!");
                Console.ReadKey();
                return;
            }

            if (workersDictionary.TryGetValue(id, out Worker worker))
            {
                worker.InputBonus();
            }
            else
            {
                Console.WriteLine($" Працівник з ID {id} не знайдений");
            }

            Console.ReadKey();
        }

        private void DisplayAllBonuses()
        {
            Console.WriteLine("\n=== ПРЕМІЇ ВСІХ ПРАЦІВНИКІВ ===");
            
            if (workersDictionary.Count == 0)
            {
                Console.WriteLine("Словник порожній");
            }
            else
            {
                foreach (var kvp in workersDictionary)
                {
                    Console.WriteLine($"\n Працівник ID: {kvp.Key} - {kvp.Value.FullName}");
                    kvp.Value.DisplayBonus();
                }
            }

            Console.ReadKey();
        }

        // 5. Визначення кількості елементів та статистика
        private void ShowStatistics()
        {
            Console.WriteLine("\n=== СТАТИСТИКА СЛОВНИКА ===");
            Console.WriteLine($" Загальна кількість працівників: {workersDictionary.Count}");
            
            if (workersDictionary.Count > 0)
            {
                // Групування за містами
                var cityGroups = workersDictionary
                    .GroupBy(kvp => kvp.Value.HomeCity)
                    .OrderByDescending(g => g.Count());
                
                Console.WriteLine("\n Розподіл за містами:");
                foreach (var group in cityGroups)
                {
                    Console.WriteLine($"  {group.Key}: {group.Count()} працівників");
                }

                // Статистика зайнятості
                var fullTimeCount = workersDictionary.Count(kvp => kvp.Value.WorkPlace.IsFullTimeEmployee);
                var partTimeCount = workersDictionary.Count - fullTimeCount;
                
                Console.WriteLine($"\n Тип зайнятості:");
                Console.WriteLine($"  Повний робочий день: {fullTimeCount}");
                Console.WriteLine($"  Неповний робочий день: {partTimeCount}");

                // Середній стаж
                double avgExperience = workersDictionary.Average(kvp => kvp.Value.GetWorkExperience());
                Console.WriteLine($"\n Середній стаж роботи: {avgExperience:F1} місяців");

                // Працівники з найбільшим стажем
                var topExperienced = workersDictionary
                    .OrderByDescending(kvp => kvp.Value.GetWorkExperience())
                    .Take(3);
                
                Console.WriteLine($"\n Топ-3 працівників за стажем:");
                foreach (var kvp in topExperienced)
                {
                    Console.WriteLine($"  {kvp.Value.FullName}: {kvp.Value.GetWorkExperience()} місяців");
                }
            }

            Console.ReadKey();
        }

        // 6. Очищення колекції
        private void ClearDictionary()
        {
            if (workersDictionary.Count == 0)
            {
                Console.WriteLine("Словник вже порожній");
                Console.ReadKey();
                return;
            }

            Console.Write("Ви впевнені, що хочете очистити весь словник? (так/ні): ");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "так" || confirmation == "1")
            {
                workersDictionary.Clear();
                nextId = 1;
                Console.WriteLine(" Словник повністю очищено!");
            }
            else
            {
                Console.WriteLine("Очищення скасовано");
            }

            Console.ReadKey();
        }

        private void DisplayWorkerInfo(Worker worker)
        {
            Console.WriteLine(worker.ToString());
            Console.WriteLine($" Стаж роботи: {worker.GetWorkExperience()} місяців");
            Console.WriteLine($" Мешкає біля головного офісу: {(worker.LivesNotFarFromTheMainOffice() ? " ТАК" : " НІ")}");
        }
    }
}
