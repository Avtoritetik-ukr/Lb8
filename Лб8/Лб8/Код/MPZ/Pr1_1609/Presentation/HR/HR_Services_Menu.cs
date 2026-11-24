using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class HR_Services_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly HR_StudentAddress_Services_Menu hr_StudentAddress_Services_Menu;
        private readonly HR_Student_Services_Menu hr_Student_Services_Menu;

        public HR_Services_Menu(
            StateMachine stateMachine,
            HR_StudentAddress_Services_Menu hr_StudentAddress_Services_Menu,
            HR_Student_Services_Menu hr_Student_Services_Menu)
        {
            this.stateMachine = stateMachine;
            this.hr_StudentAddress_Services_Menu = hr_StudentAddress_Services_Menu;
            this.hr_Student_Services_Menu = hr_Student_Services_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- HR Services Menu ---");
            Console.WriteLine("1. HR StudentAddress Services Menu");
            Console.WriteLine("2. HR Student Services Menu");
            Console.WriteLine("0. Назад");
        }

        public override void HandleMenu()
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Оберіть розділ: ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        stateMachine.TransitionTo(MenuState.HRStudentAddressServicesMenu);
                        hr_StudentAddress_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRServicesMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.HRStudentServicesMenu);
                        hr_Student_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRServicesMenu);
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
