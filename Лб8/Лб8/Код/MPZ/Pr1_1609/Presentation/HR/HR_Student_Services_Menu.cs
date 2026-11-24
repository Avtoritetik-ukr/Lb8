using System;

using Pr1_1609.BusinessLogic.Services;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class HR_Student_Services_Menu : MenuBase
    {
        private readonly HR_Student_Services hr_Student_Services;

        public HR_Student_Services_Menu(HR_Student_Services hr_Student_Services)
        {
            this.hr_Student_Services = hr_Student_Services;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- HR Student Services Menu ---");
            Console.WriteLine("1. Get student by Surname (Пошук студента за прізвищем)");
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
                        hr_Student_Services.GetStudentBySurname();
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
