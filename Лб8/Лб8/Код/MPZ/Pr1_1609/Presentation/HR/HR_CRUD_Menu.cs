using System;

using Pr1_1609.BusinessLogic.Navigation;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class HR_CRUD_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly Student_CRUD_Menu student_CRUD_Menu;
        private readonly Address_CRUD_Menu address_CRUD_Menu;
        private readonly Group_CRUD_Menu group_CRUD_Menu;

        public HR_CRUD_Menu(
            StateMachine stateMachine,
            Student_CRUD_Menu student_CRUD_Menu,
            Address_CRUD_Menu address_CRUD_Menu,
            Group_CRUD_Menu group_CRUD_Menu)
        {
            this.stateMachine = stateMachine;
            this.student_CRUD_Menu = student_CRUD_Menu;
            this.address_CRUD_Menu = address_CRUD_Menu;
            this.group_CRUD_Menu = group_CRUD_Menu;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- HR CRUD Menu ---");
            Console.WriteLine("1. Student CRUD Menu");
            Console.WriteLine("2. Address CRUD Menu");
            Console.WriteLine("3. Group CRUD Menu");
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
                        stateMachine.TransitionTo(MenuState.StudentCRUDMenu);
                        student_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRCRUDMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.AddressCRUDMenu);
                        address_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRCRUDMenu);
                        break;
                    case "3":
                        stateMachine.TransitionTo(MenuState.GroupCRUDMenu);
                        group_CRUD_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.HRCRUDMenu);
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
