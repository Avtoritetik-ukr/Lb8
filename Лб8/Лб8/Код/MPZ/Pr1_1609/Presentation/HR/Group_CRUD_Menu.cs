using System;

using Pr1_1609.BusinessLogic.CRUD;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class Group_CRUD_Menu : MenuBase
    {
        private readonly HR_CRUD_Group hr_CRUD_Group;

        public Group_CRUD_Menu(HR_CRUD_Group hr_CRUD_Group)
        {
            this.hr_CRUD_Group = hr_CRUD_Group;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Group CRUD Menu (Управління групами) ---");
            Console.WriteLine("1. Create (Додати групу)");
            Console.WriteLine("2. Read (Знайти групу за ID)");
            Console.WriteLine("3. Update (Оновити групу)");
            Console.WriteLine("4. Delete (Видалити групу)");
            Console.WriteLine("5. GetAll (Переглянути всі групи)");
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
                        hr_CRUD_Group.CreateGroup();
                        break;
                    case "2":
                        hr_CRUD_Group.ReadGroup();
                        break;
                    case "3":
                        hr_CRUD_Group.UpdateGroup();
                        break;
                    case "4":
                        hr_CRUD_Group.DeleteGroup();
                        break;
                    case "5":
                        hr_CRUD_Group.GetAllGroups();
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
