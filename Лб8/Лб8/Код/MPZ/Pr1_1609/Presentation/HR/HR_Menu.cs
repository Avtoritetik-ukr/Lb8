using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class HR_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly HR_CRUD_Menu hr_CRUD_Menu;
        private readonly HR_Services_Menu hr_Services_Menu;

        public HR_Menu(StateMachine stateMachine, HR_CRUD_Menu hr_CRUD_Menu, HR_Services_Menu hr_Services_Menu)
        {
            this.stateMachine = stateMachine;
            this.hr_CRUD_Menu = hr_CRUD_Menu;
            this.hr_Services_Menu = hr_Services_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n=== HR Menu (Відділ кадрів) ===");
            Console.WriteLine("1. HR CRUD Menu");
            Console.WriteLine("2. HR Services Menu");
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
                        stateMachine.TransitionTo(MenuState.HRCRUDMenu);
                        hr_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.HRServicesMenu);
                        hr_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRMenu);
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
