using System;

namespace SimpleClassLibrary
{
    public class Company
    {
        protected string name;
        protected string mainOfficeCity;
        protected string position;
        protected decimal salary;
        protected bool isFullTimeEmployee;

        // Конструктори
        public Company()
        {
            name = "Невідомо";
            mainOfficeCity = "Невідомо";
            position = "Невідомо";
            salary = 0;
            isFullTimeEmployee = false;
        }

        public Company(string name, string mainOfficeCity, string position, decimal salary)
        {
            this.name = name;
            this.mainOfficeCity = mainOfficeCity;
            this.position = position;
            this.salary = salary;
            this.isFullTimeEmployee = true;
        }

        public Company(string name, string mainOfficeCity, string position, decimal salary, bool isFullTimeEmployee)
        {
            this.name = name;
            this.mainOfficeCity = mainOfficeCity;
            this.position = position;
            this.salary = salary;
            this.isFullTimeEmployee = isFullTimeEmployee;
        }

        // Конструктор копіювання
        public Company(Company other)
        {
            name = other.name;
            mainOfficeCity = other.mainOfficeCity;
            position = other.position;
            salary = other.salary;
            isFullTimeEmployee = other.isFullTimeEmployee;
        }

        // Властивості
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string MainOfficeCity
        {
            get { return mainOfficeCity; }
            set { mainOfficeCity = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public bool IsFullTimeEmployee
        {
            get { return isFullTimeEmployee; }
            set { isFullTimeEmployee = value; }
        }

        // Перевизначення ToString
        public override string ToString()
        {
            string employmentType = isFullTimeEmployee ? "Повний робочий день" : "Неповний робочий день";
            return $"Компанія: {name}, Головний офіс: {mainOfficeCity}, Посада: {position}, Зарплата: {salary:C}, {employmentType}";
        }
    }
}
