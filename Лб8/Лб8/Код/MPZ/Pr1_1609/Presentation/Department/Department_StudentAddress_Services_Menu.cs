using System;

using Pr1_1609.BusinessLogic.Services;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Department_StudentAddress_Services_Menu : MenuBase
    {
        private readonly Department_StudentAddress_Services department_StudentAddress_Services;

        public Department_StudentAddress_Services_Menu(Department_StudentAddress_Services department_StudentAddress_Services)
        {
            this.department_StudentAddress_Services = department_StudentAddress_Services;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Department StudentAddress Services Menu ---");
            Console.WriteLine("1. Get students with addresses (Повний звіт по студентах та адресах)");
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
                        department_StudentAddress_Services.GetStudentsWithAddresses();
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
