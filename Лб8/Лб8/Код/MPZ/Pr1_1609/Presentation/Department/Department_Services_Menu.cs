using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Department_Services_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly Department_StudentAddress_Services_Menu department_StudentAddress_Services_Menu;
        private readonly Department_Student_Services_Menu department_Student_Services_Menu;

        public Department_Services_Menu(
            StateMachine stateMachine,
            Department_StudentAddress_Services_Menu department_StudentAddress_Services_Menu,
            Department_Student_Services_Menu department_Student_Services_Menu)
        {
            this.stateMachine = stateMachine;
            this.department_StudentAddress_Services_Menu = department_StudentAddress_Services_Menu;
            this.department_Student_Services_Menu = department_Student_Services_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Department Services Menu ---");
            Console.WriteLine("1. Department StudentAddress Services Menu");
            Console.WriteLine("2. Department Student Services Menu");
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
                        stateMachine.TransitionTo(MenuState.DepartmentStudentAddressServicesMenu);
                        department_StudentAddress_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentServicesMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.DepartmentStudentServicesMenu);
                        department_Student_Services_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentServicesMenu);
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
