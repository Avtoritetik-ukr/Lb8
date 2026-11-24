using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.Navigation
{
    public sealed class StateMachine
    {
        public MenuState CurrentState { get; private set; } = MenuState.Start;
        public DB Database { get; }
        public UserRole Role { get; set; } = UserRole.Admin;

        public StateMachine(DB database)
        {
            Database = database;
            OnEnter(MenuState.Start);
        }

        public bool TransitionTo(MenuState nextState, Func<bool>? guard = null)
        {
            if (guard != null && !guard())
            {
                Logger.Write("Guard", $"Перехід {CurrentState} -> {nextState} відхилено захисною умовою");
                return false;
            }

            OnExit(CurrentState);
            CurrentState = nextState;
            OnEnter(CurrentState);
            return true;
        }

        public bool CheckAdminGuard(string operationName)
        {
            if (Role == UserRole.Admin) return true;
            Console.WriteLine($"[Доступ заборонено] Операція '{operationName}' доступна лише адміністратору.");
            Logger.Write("Guard", $"Відхилено '{operationName}' для ролі {Role}");
            return false;
        }

        private void OnEnter(MenuState state)
        {
            Logger.Write("Стан", $"Вхід у стан: {state}");
            if (state == MenuState.Exit)
            {
                Database.SaveAll();
                Console.WriteLine("[Успішно] Усі дані збережено у БД. Роботу завершено.");
            }
        }

        private void OnExit(MenuState state)
        {
            Logger.Write("Стан", $"Вихід зі стану: {state}");
        }
    }
}
