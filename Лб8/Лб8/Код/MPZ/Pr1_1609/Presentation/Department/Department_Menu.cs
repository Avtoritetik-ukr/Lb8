using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Department_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly Department_CRUD_Menu department_CRUD_Menu;
        private readonly Department_Services_Menu department_Services_Menu;

        public Department_Menu(
            StateMachine stateMachine,
            Department_CRUD_Menu department_CRUD_Menu,
            Department_Services_Menu department_Services_Menu)
        {
            this.stateMachine = stateMachine;
            this.department_CRUD_Menu = department_CRUD_Menu;
            this.department_Services_Menu = department_Services_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n=== Department Menu (Кафедра) ===");
            Console.WriteLine("1. Department CRUD Menu");
            Console.WriteLine("2. Department Services Menu");
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
                        stateMachine.TransitionTo(MenuState.DepartmentCRUDMenu);
                        department_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.DepartmentServicesMenu);
                        department_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentMenu);
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
