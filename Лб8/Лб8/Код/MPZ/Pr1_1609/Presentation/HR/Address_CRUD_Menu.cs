using System;

using Pr1_1609.BusinessLogic.CRUD;

using Pr1_1609.Presentation;

namespace Pr1_1609.Presentation.HR
{
    public sealed class Address_CRUD_Menu : MenuBase
    {
        private readonly HR_CRUD_Address hr_CRUD_Address;

        public Address_CRUD_Menu(HR_CRUD_Address hr_CRUD_Address)
        {
            this.hr_CRUD_Address = hr_CRUD_Address;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- Address CRUD Menu (Управління адресами) ---");
            Console.WriteLine("1. Create (Додати адресу)");
            Console.WriteLine("2. Read (Знайти адресу за ID)");
            Console.WriteLine("3. Update (Оновити адресу)");
            Console.WriteLine("4. Delete (Видалити адресу)");
            Console.WriteLine("5. GetAll (Переглянути всі адреси)");
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
                        hr_CRUD_Address.CreateAddress();
                        break;
                    case "2":
                        hr_CRUD_Address.ReadAddress();
                        break;
                    case "3":
                        hr_CRUD_Address.UpdateAddress();
                        break;
                    case "4":
                        hr_CRUD_Address.DeleteAddress();
                        break;
                    case "5":
                        hr_CRUD_Address.GetAllAddresses();
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
