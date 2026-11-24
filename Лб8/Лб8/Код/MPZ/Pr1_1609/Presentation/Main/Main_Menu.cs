using System;

using Pr1_1609.Domain;
using Pr1_1609.BusinessLogic.Services;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Presentation.HR;
using Pr1_1609.Presentation.Department;
using Pr1_1609.Infrastructure;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Main
{
    public sealed class Main_Menu : MenuBase
    {
        private readonly StateMachine stateMachine;
        private readonly DepartmentServyses departmentServices;
        private readonly HR_Menu hr_Menu;
        private readonly Department_Menu department_Menu;

        public Main_Menu(
            StateMachine stateMachine,
            DepartmentServyses departmentServices,
            HR_Menu hr_Menu,
            Department_Menu department_Menu)
        {
            this.stateMachine = stateMachine;
            this.departmentServices = departmentServices;
            this.hr_Menu = hr_Menu;
            this.department_Menu = department_Menu;
        }

        public override void ShowMenu()
        {
            string roleTitle = stateMachine.Role == UserRole.Admin ? "Адміністратор" : "Викладач";
            Console.WriteLine("\n==================================================");
            Console.WriteLine($"    Main Menu (Головне меню) | Роль: {roleTitle}");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. HR Menu (Відділ кадрів)");
            Console.WriteLine("2. Department Menu (Кафедра)");
            Console.WriteLine("3. Змінити роль");
            Console.WriteLine("0. Exit / Save DB (Вихід та збереження БД)");
        }

        public override void HandleMenu()
        {
            while (stateMachine.CurrentState == MenuState.MainMenu)
            {
                ShowMenu();
                Console.Write("Оберіть розділ: ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        stateMachine.TransitionTo(MenuState.HRMenu);
                        hr_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.MainMenu);
                        break;
                    case "2":
                        stateMachine.TransitionTo(MenuState.DepartmentMenu);
                        department_Menu.HandleMenu();
                        stateMachine.TransitionTo(MenuState.MainMenu);
                        break;
                    case "3":
                        UserRole newRole = SelectRole();
                        stateMachine.Role = newRole;
                        Logger.Write("Вхід", $"Змінено роль на: {newRole}");
                        break;
                    case "0":
                        stateMachine.TransitionTo(MenuState.Exit);
                        return;
                    default:
                        Console.WriteLine("[Помилка] Невірний вибір, спробуйте ще раз.");
                        break;
                }
            }
        }

        public static UserRole SelectRole()
        {
            Console.WriteLine("\nОберіть роль: 1 — Адміністратор, 2 — Викладач (лише перегляд)");
            return InputHelper.ReadInt("Роль: ", 1, 2) == 1 ? UserRole.Admin : UserRole.Teacher;
        }
    }
}
