using System;

using Pr1_1609.BusinessLogic.CRUD;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class GroupSubject_CRUD_Menu : MenuBase
    {
        private readonly Department_CRUD_GroupSubject department_CRUD_GroupSubject;

        public GroupSubject_CRUD_Menu(Department_CRUD_GroupSubject department_CRUD_GroupSubject)
        {
            this.department_CRUD_GroupSubject = department_CRUD_GroupSubject;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- GroupSubject CRUD Menu (Закріплення предметів за групами) ---");
            Console.WriteLine("1. Create (Закріпити предмет за групою)");
            Console.WriteLine("2. Read (Знайти закріплення за ID)");
            Console.WriteLine("3. Update (Оновити закріплення)");
            Console.WriteLine("4. Delete (Видалити закріплення)");
            Console.WriteLine("5. GetAll (Переглянути всі закріплення)");
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
                        department_CRUD_GroupSubject.CreateGroupSubject();
                        break;
                    case "2":
                        department_CRUD_GroupSubject.ReadGroupSubject();
                        break;
                    case "3":
                        department_CRUD_GroupSubject.UpdateGroupSubject();
                        break;
                    case "4":
                        department_CRUD_GroupSubject.DeleteGroupSubject();
                        break;
                    case "5":
                        department_CRUD_GroupSubject.GetAllGroupSubjects();
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
