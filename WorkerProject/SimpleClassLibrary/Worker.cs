using System;

namespace SimpleClassLibrary
{
    public class Worker
    {
        protected string fullName;
        protected string homeCity;
        protected DateTime startDate;
        protected Company workPlace;
        
        // Нові поля для премії
        protected decimal bonusUAH;
        protected decimal bonusUSD;
        protected decimal bonusEUR;

        // Конструктори
        public Worker()
        {
            fullName = "Невідомо";
            homeCity = "Невідомо";
            startDate = DateTime.Now;
            workPlace = new Company();
            bonusUAH = 0;
            bonusUSD = 0;
            bonusEUR = 0;
        }

        public Worker(string fullName, string homeCity, DateTime startDate, Company workPlace)
        {
            this.fullName = fullName;
            this.homeCity = homeCity;
            this.startDate = startDate;
            this.workPlace = new Company(workPlace);
            bonusUAH = 0;
            bonusUSD = 0;
            bonusEUR = 0;
        }

        public Worker(string fullName, string homeCity, int year, int month, int day, Company workPlace)
        {
            this.fullName = fullName;
            this.homeCity = homeCity;
            this.startDate = new DateTime(year, month, day);
            this.workPlace = new Company(workPlace);
            bonusUAH = 0;
            bonusUSD = 0;
            bonusEUR = 0;
        }

        // Конструктор копіювання
        public Worker(Worker other)
        {
            fullName = other.fullName;
            homeCity = other.homeCity;
            startDate = other.startDate;
            workPlace = new Company(other.workPlace);
            bonusUAH = other.bonusUAH;
            bonusUSD = other.bonusUSD;
            bonusEUR = other.bonusEUR;
        }

        // Властивості
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string HomeCity
        {
            get { return homeCity; }
            set { homeCity = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public Company WorkPlace
        {
            get { return workPlace; }
            set { workPlace = new Company(value); }
        }

        // Нові властивості для премії
        public decimal BonusUAH
        {
            get { return bonusUAH; }
            set { bonusUAH = value; }
        }

        public decimal BonusUSD
        {
            get { return bonusUSD; }
            set { bonusUSD = value; }
        }

        public decimal BonusEUR
        {
            get { return bonusEUR; }
            set { bonusEUR = value; }
        }

        // Методи
        public int GetWorkExperience()
        {
            DateTime currentDate = DateTime.Now;
            int months = (currentDate.Year - startDate.Year) * 12 + currentDate.Month - startDate.Month;
            
            if (currentDate.Day < startDate.Day)
            {
                months--;
            }
            
            return Math.Max(0, months);
        }

        public bool LivesNotFarFromTheMainOffice()
        {
            return string.Equals(homeCity, workPlace.MainOfficeCity, StringComparison.OrdinalIgnoreCase);
        }

        // Новий метод для введення премії
        public void InputBonus()
        {
            Console.WriteLine("\n=== Введення премії для працівника ===");
            Console.WriteLine("Оберіть валюту для введення премії:");
            Console.WriteLine("1. Гривні (UAH)");
            Console.WriteLine("2. Долари (USD)");
            Console.WriteLine("3. Євро (EUR)");
            Console.Write("Ваш вибір (1-3): ");

            string choice = Console.ReadLine();
            decimal bonusAmount;

            Console.Write("Введіть розмір премії: ");
            while (!decimal.TryParse(Console.ReadLine(), out bonusAmount) || bonusAmount < 0)
            {
                Console.Write("Будь ласка, введіть коректну суму премії: ");
            }

            // Курси валют (можна зробити константами або виносити в налаштування)
            const decimal usdToUah = 39.5m;
            const decimal eurToUah = 42.8m;
            const decimal usdToEur = 0.92m;

            switch (choice)
            {
                case "1": // UAH
                    bonusUAH = bonusAmount;
                    bonusUSD = bonusAmount / usdToUah;
                    bonusEUR = bonusAmount / eurToUah;
                    break;
                case "2": // USD
                    bonusUSD = bonusAmount;
                    bonusUAH = bonusAmount * usdToUah;
                    bonusEUR = bonusAmount * usdToEur;
                    break;
                case "3": // EUR
                    bonusEUR = bonusAmount;
                    bonusUAH = bonusAmount * eurToUah;
                    bonusUSD = bonusAmount / usdToEur;
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Премія не встановлена.");
                    return;
            }

            Console.WriteLine("Премія успішно встановлена!");
        }

        // Новий метод для виведення премії у всіх валютах
        public void DisplayBonus()
        {
            Console.WriteLine("\n=== Розмір премії ===");
            Console.WriteLine($"Гривні (UAH): {bonusUAH:F2} ₴");
            Console.WriteLine($"Долари (USD): {bonusUSD:F2} $");
            Console.WriteLine($"Євро (EUR): {bonusEUR:F2} €");
        }

        // Перевизначення ToString з додаванням інформації про премію
        public override string ToString()
        {
            string workerInfo = $"Працівник: {fullName}, Місто: {homeCity}, Дата початку: {startDate:yyyy-MM-dd}\n{workPlace}";
            string bonusInfo = $"\nПремія: {bonusUAH:F2} ₴ | {bonusUSD:F2} $ | {bonusEUR:F2} €";
            return workerInfo + bonusInfo;
        }
    }
}