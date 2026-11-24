using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Department_CRUD_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly Subject_CRUD_Menu subject_CRUD_Menu;
        private readonly GroupSubject_CRUD_Menu groupSubject_CRUD_Menu;

        public Department_CRUD_Menu(
            StateMachine stateMachine,
            Subject_CRUD_Menu subject_CRUD_Menu,
            GroupSubject_CRUD_Menu groupSubject_CRUD_Menu)
        {
            this.stateMachine = stateMachine;
            this.subject_CRUD_Menu = subject_CRUD_Menu;
            this.groupSubject_CRUD_Menu = groupSubject_CRUD_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Department CRUD Menu ---");
            Console.WriteLine("1. Subject CRUD Menu");
            Console.WriteLine("2. GroupSubject CRUD Menu");
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
                        stateMachine.TransitionTo(MenuState.SubjectCRUDMenu);
                        subject_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentCRUDMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.GroupSubjectCRUDMenu);
                        groupSubject_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.DepartmentCRUDMenu);
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
