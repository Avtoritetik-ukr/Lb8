using System;

using Pr1_1609.BusinessLogic.CRUD;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.Department
{
    public sealed class Subject_CRUD_Menu : MenuBase
    {
        private readonly Department_CRUD_Subject department_CRUD_Subject;

        public Subject_CRUD_Menu(Department_CRUD_Subject department_CRUD_Subject)
        {
            this.department_CRUD_Subject = department_CRUD_Subject;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Subject CRUD Menu (Управління предметами) ---");
            Console.WriteLine("1. Create (Додати предмет)");
            Console.WriteLine("2. Read (Знайти предмет за ID)");
            Console.WriteLine("3. Update (Оновити предмет)");
            Console.WriteLine("4. Delete (Видалити предмет)");
            Console.WriteLine("5. GetAll (Переглянути всі предмети)");
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
                        department_CRUD_Subject.CreateSubject();
                        break;
                    case "2":
                        department_CRUD_Subject.ReadSubject();
                        break;
                    case "3":
                        department_CRUD_Subject.UpdateSubject();
                        break;
                    case "4":
                        department_CRUD_Subject.DeleteSubject();
                        break;
                    case "5":
                        department_CRUD_Subject.GetAllSubjects();
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
