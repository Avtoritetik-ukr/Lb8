using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Validation;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public sealed class HR_CRUD_Address : CrudBase<Address>
    {

        public HR_CRUD_Address(DB db, StateMachine stateMachine) : base(db, stateMachine)
        {
        }

        public override int Create() => CreateAddress();
        public override void Read() => ReadAddress();
        public override void Update() => UpdateAddress();
        public override void Delete() => DeleteAddress();
        public override void GetAll() => GetAllAddresses();

        public int CreateAddress()
        {
            if (!stateMachine.CheckAdminGuard("Create Address")) return -1;

            string studentAddress = InputHelper.ReadString("Адреса студента: ");
            int studentId = InputHelper.ReadInt("ID студента: ");

            string? error = Validator.ValidateAddress(db, studentAddress, studentId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return -1;
            }

            Address address = new Address(studentAddress, studentId);
            int id = db.DBAdrress.AddItem(address);
            db.SaveAddresses();
            Logger.Write("Додано", $"Адреса ID {id} для студента ID {studentId}");
            Console.WriteLine($"[Успішно] Адресу додано з ID: {id}");
            return id;
        }

        public void ReadAddress()
        {
            int id = InputHelper.ReadInt("Введіть ID адреси: ");
            Address? address = db.DBAdrress.GetItem(id);
            if (address != null)
            {
                Console.WriteLine(address);
            }
            else
            {
                Console.WriteLine($"[Помилка] Адреси з ID: {id} не знайдено.");
            }
        }

        public void UpdateAddress()
        {
            if (!stateMachine.CheckAdminGuard("Update Address")) return;

            int id = InputHelper.ReadInt("Введіть ID адреси для оновлення: ");
            Address? oldAddress = db.DBAdrress.GetItem(id);
            if (oldAddress == null)
            {
                Console.WriteLine($"[Помилка] Адреси з ID: {id} не знайдено.");
                return;
            }

            string studentAddress = InputHelper.ReadString("Нова адреса: ");
            int studentId = InputHelper.ReadInt("Новий ID студента: ");

            string? error = Validator.ValidateAddress(db, studentAddress, studentId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return;
            }

            Address newAddress = new Address(studentAddress, studentId);
            db.DBAdrress.UpdateItem(oldAddress, newAddress);
            db.SaveAddresses();
            Logger.Write("Оновлено", $"Адреса ID {id} для студента ID {studentId}");
            Console.WriteLine($"[Успішно] Адресу з ID: {id} оновлено.");
        }

        public void DeleteAddress()
        {
            if (!stateMachine.CheckAdminGuard("Delete Address")) return;

            int id = InputHelper.ReadInt("Введіть ID адреси для видалення: ");
            Address? address = db.DBAdrress.GetItem(id);
            if (address != null)
            {
                db.DBAdrress.DeleteItem(address);
                db.SaveAddresses();
                Logger.Write("Видалено", $"Адреса ID {id}");
                Console.WriteLine($"[Успішно] Адресу з ID: {id} видалено.");
            }
            else
            {
                Console.WriteLine($"[Помилка] Адреси з ID: {id} не знайдено.");
            }
        }

        public void GetAllAddresses()
        {
            Console.WriteLine("Усі адреси:");
            foreach (Address address in db.DBAdrress.Items)
            {
                Console.WriteLine(address);
            }
        }
    }
}
