using System;

using Pr1_1609.BusinessLogic.Services;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Department_Student_Services_Menu : MenuBase
    {
        private readonly Department_Student_Services department_Student_Services;

        public Department_Student_Services_Menu(Department_Student_Services department_Student_Services)
        {
            this.department_Student_Services = department_Student_Services;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Department Student Services Menu ---");
            Console.WriteLine("1. Get student by Surname (Пошук студента за прізвищем)");
            Console.WriteLine("2. Get student workload (Навантаження студента)");
            Console.WriteLine("0. Назад");
        }

        public override void HandleMenu()
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Оберіть дію: ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        department_Student_Services.GetStudentBySurname();
                        break;
                    case "2":
                        department_Student_Services.GetStudentWorkload();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("[Помилка] Невірний вибір, спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
