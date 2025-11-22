using System;
using System.Collections;
using System.Collections.Generic;

namespace SortedListDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedListDemoApp app = new SortedListDemoApp();
            app.Run();
        }
    }

    public class SortedListDemoApp
    {
        private SortedList stringList;
        private SortedList doubleList;
        private SortedList intList;

        public SortedListDemoApp()
        {
            stringList = new SortedList();
            doubleList = new SortedList();
            intList = new SortedList();
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== ДЕМОНСТРАЦІЯ SortedList ===");
                Console.WriteLine("1. Додати елемент до string SortedList");
                Console.WriteLine("2. Додати елемент до double SortedList");
                Console.WriteLine("3. Додати елемент до int SortedList");
                Console.WriteLine("4. Показати всі SortedList");
                Console.WriteLine("5. Пошук елементів");
                Console.WriteLine("6. Видалити елементи");
                Console.WriteLine("7. Очистити всі колекції");
                Console.WriteLine("8. Статистика");
                Console.WriteLine("9. Вихід");
                Console.Write("Оберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddToStringSortedList();
                        break;
                    case "2":
                        AddToDoubleSortedList();
                        break;
                    case "3":
                        AddToIntSortedList();
                        break;
                    case "4":
                        DisplayAllSortedLists();
                        break;
                    case "5":
                        SearchInSortedLists();
                        break;
                    case "6":
                        RemoveFromSortedLists();
                        break;
                    case "7":
                        ClearSortedLists();
                        break;
                    case "8":
                        ShowStatistics();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірна опція. Натисніть будь-яку клавішу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddToStringSortedList()
        {
            Console.WriteLine("\n=== Додавання до string SortedList ===");
            
            Console.Write("Введіть ключ (число): ");
            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Невірний ключ!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть значення (рядок): ");
            string value = Console.ReadLine();

            if (!stringList.ContainsKey(key))
            {
                stringList.Add(key, value);
                Console.WriteLine($"Елемент додано: [{key}] = \"{value}\"");
            }
            else
            {
                Console.WriteLine("Елемент з таким ключем вже існує!");
            }

            Console.WriteLine($"Кількість елементів у stringList: {stringList.Count}");
            Console.ReadKey();
        }

        private void AddToDoubleSortedList()
        {
            Console.WriteLine("\n=== Додавання до double SortedList ===");
            
            Console.Write("Введіть ключ (число): ");
            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Невірний ключ!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть значення (double): ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Невірне значення!");
                Console.ReadKey();
                return;
            }

            if (!doubleList.ContainsKey(key))
            {
                doubleList.Add(key, value);
                Console.WriteLine($"Елемент додано: [{key}] = {value:F2}");
            }
            else
            {
                Console.WriteLine("Елемент з таким ключем вже існує!");
            }

            Console.WriteLine($"Кількість елементів у doubleList: {doubleList.Count}");
            Console.ReadKey();
        }

        private void AddToIntSortedList()
        {
            Console.WriteLine("\n=== Додавання до int SortedList ===");
            
            Console.Write("Введіть ключ (число): ");
            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Невірний ключ!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть значення (int): ");
            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Невірне значення!");
                Console.ReadKey();
                return;
            }

            if (!intList.ContainsKey(key))
            {
                intList.Add(key, value);
                Console.WriteLine($"Елемент додано: [{key}] = {value}");
            }
            else
            {
                Console.WriteLine("Елемент з таким ключем вже існує!");
            }

            Console.WriteLine($"Кількість елементів у intList: {intList.Count}");
            Console.ReadKey();
        }

        private void DisplayAllSortedLists()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("=== ВСІ SortedList КОЛЕКЦІЇ ===");

            DisplaySortedList("string SortedList", stringList, value => $"\"{value}\"");
            DisplaySortedList("double SortedList", doubleList, value => $"{value:F2}");
            DisplaySortedList("int SortedList", intList, value => $"{value}");

            Console.WriteLine($"\nЗагальна статистика:");
            Console.WriteLine($"stringList: {stringList.Count} елементів");
            Console.WriteLine($"doubleList: {doubleList.Count} елементів");
            Console.WriteLine($"intList: {intList.Count} елементів");
            Console.WriteLine($"Всього елементів: {stringList.Count + doubleList.Count + intList.Count}");
            
            Console.ReadKey();
        }

        private void DisplaySortedList(string title, SortedList list, Func<object, string> formatter)
        {
            Console.WriteLine($"\n=== {title} ===");
            if (list.Count == 0)
            {
                Console.WriteLine("Колекція порожня");
            }
            else
            {
                foreach (DictionaryEntry item in list)
                {
                    Console.WriteLine($"[{item.Key}] = {formatter(item.Value)}");
                }
            }
        }

        private void SearchInSortedLists()
        {
            Console.WriteLine("\n=== ПОШУК ЕЛЕМЕНТІВ ===");
            
            Console.Write("Введіть ключ для пошуку: ");
            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Невірний ключ!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n=== РЕЗУЛЬТАТИ ПОШУКУ ===");
            
            SearchAndDisplay(stringList, key, "stringList", value => $"\"{value}\"");
            SearchAndDisplay(doubleList, key, "doubleList", value => $"{value:F2}");
            SearchAndDisplay(intList, key, "intList", value => $"{value}");
            
            Console.ReadKey();
        }

        private void SearchAndDisplay(SortedList list, int key, string listName, Func<object, string> formatter)
        {
            if (list.ContainsKey(key))
            {
                Console.WriteLine($"{listName}: Знайдено - {formatter(list[key])}");
            }
            else
            {
                Console.WriteLine($"{listName}: Не знайдено");
            }
        }

        private void RemoveFromSortedLists()
        {
            Console.WriteLine("\n=== ВИДАЛЕННЯ ЕЛЕМЕНТІВ ===");
            
            Console.Write("Введіть ключ для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Невірний ключ!");
                Console.ReadKey();
                return;
            }

            bool removedAny = false;

            if (stringList.ContainsKey(key))
            {
                object removedValue = stringList[key];
                stringList.Remove(key);
                Console.WriteLine($"Видалено з stringList: ключ {key}, значення \"{removedValue}\"");
                removedAny = true;
            }

            if (doubleList.ContainsKey(key))
            {
                object removedValue = doubleList[key];
                doubleList.Remove(key);
                Console.WriteLine($"Видалено з doubleList: ключ {key}, значення {removedValue:F2}");
                removedAny = true;
            }

            if (intList.ContainsKey(key))
            {
                object removedValue = intList[key];
                intList.Remove(key);
                Console.WriteLine($"Видалено з intList: ключ {key}, значення {removedValue}");
                removedAny = true;
            }

            if (!removedAny)
            {
                Console.WriteLine("Елемент з таким ключем не знайдено в жодній колекції");
            }
            else
            {
                Console.WriteLine($"\nОновлена статистика:");
                Console.WriteLine($"stringList: {stringList.Count} елементів");
                Console.WriteLine($"doubleList: {doubleList.Count} елементів");
                Console.WriteLine($"intList: {intList.Count} елементів");
            }

            Console.ReadKey();
        }

        private void ClearSortedLists()
        {
            stringList.Clear();
            doubleList.Clear();
            intList.Clear();
            
            Console.WriteLine("Всі колекції очищено!");
            Console.WriteLine($"stringList: {stringList.Count} елементів");
            Console.WriteLine($"doubleList: {doubleList.Count} елементів");
            Console.WriteLine($"intList: {intList.Count} елементів");
            
            Console.ReadKey();
        }

        private void ShowStatistics()
        {
            Console.WriteLine("\n=== СТАТИСТИКА КОЛЕКЦІЙ ===");
            
            Console.WriteLine($"Загальна кількість елементів: {stringList.Count + doubleList.Count + intList.Count}");
            Console.WriteLine($"stringList: {stringList.Count} елементів");
            Console.WriteLine($"doubleList: {doubleList.Count} елементів");
            Console.WriteLine($"intList: {intList.Count} елементів");

            if (stringList.Count > 0)
            {
                Console.WriteLine($"\nstringList - перший елемент: [{stringList.GetKey(0)}] = \"{stringList.GetByIndex(0)}\"");
                Console.WriteLine($"stringList - останній елемент: [{stringList.GetKey(stringList.Count - 1)}] = \"{stringList.GetByIndex(stringList.Count - 1)}\"");
            }

            if (doubleList.Count > 0)
            {
                Console.WriteLine($"\ndoubleList - перший елемент: [{doubleList.GetKey(0)}] = {doubleList.GetByIndex(0):F2}");
                Console.WriteLine($"doubleList - останній елемент: [{doubleList.GetKey(doubleList.Count - 1)}] = {doubleList.GetByIndex(doubleList.Count - 1):F2}");
            }

            if (intList.Count > 0)
            {
                Console.WriteLine($"\nintList - перший елемент: [{intList.GetKey(0)}] = {intList.GetByIndex(0)}");
                Console.WriteLine($"intList - останній елемент: [{intList.GetKey(intList.Count - 1)}] = {intList.GetByIndex(intList.Count - 1)}");
            }

            Console.ReadKey();
        }
    }
}