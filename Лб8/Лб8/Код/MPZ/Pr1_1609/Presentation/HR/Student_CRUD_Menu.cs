using System;

using Pr1_1609.BusinessLogic.CRUD;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class Student_CRUD_Menu : MenuBase
    {
        private readonly HR_CRUD_Student hr_CRUD_Student;

        public Student_CRUD_Menu(HR_CRUD_Student hr_CRUD_Student)
        {
            this.hr_CRUD_Student = hr_CRUD_Student;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Student CRUD Menu (Управління студентами) ---");
            Console.WriteLine("1. Create (Додати студента)");
            Console.WriteLine("2. Read (Знайти студента за ID)");
            Console.WriteLine("3. Update (Оновити дані студента)");
            Console.WriteLine("4. Delete (Видалити студента)");
            Console.WriteLine("5. GetAll (Переглянути всіх студентів)");
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
                        hr_CRUD_Student.CreateStudent();
                        break;
                    case "2":
                        hr_CRUD_Student.ReadStudent();
                        break;
                    case "3":
                        hr_CRUD_Student.UpdateStudent();
                        break;
                    case "4":
                        hr_CRUD_Student.DeleteStudent();
                        break;
                    case "5":
                        hr_CRUD_Student.GetAllStudents();
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
