using System;

using Pr1_1609.BusinessLogic.Services;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class HR_StudentAddress_Services_Menu : MenuBase
    {
        private readonly HR_StudentAddress_Services hr_StudentAddress_Services;

        public HR_StudentAddress_Services_Menu(HR_StudentAddress_Services hr_StudentAddress_Services)
        {
            this.hr_StudentAddress_Services = hr_StudentAddress_Services;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- HR StudentAddress Services Menu ---");
            Console.WriteLine("1. Get students with addresses (Студенти з адресами)");
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
                        hr_StudentAddress_Services.GetStudentsWithAddresses();
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
