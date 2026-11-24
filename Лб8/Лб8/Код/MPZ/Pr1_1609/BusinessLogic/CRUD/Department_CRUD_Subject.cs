using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Validation;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public sealed class Department_CRUD_Subject : CrudBase<Subject>
    {

        public Department_CRUD_Subject(DB db, StateMachine stateMachine) : base(db, stateMachine)
        {
        }

        public override int Create() => CreateSubject();
        public override void Read() => ReadSubject();
        public override void Update() => UpdateSubject();
        public override void Delete() => DeleteSubject();
        public override void GetAll() => GetAllSubjects();

        public int CreateSubject()
        {
            if (!stateMachine.CheckAdminGuard("Create Subject")) return -1;

            string subjectName = InputHelper.ReadString("Назва предмета: ");
            int hours = InputHelper.ReadInt("Кількість годин: ");

            string? error = Validator.ValidateSubject(subjectName, hours);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return -1;
            }

            Subject subject = new Subject(subjectName, hours);
            int id = db.DBSubject.AddItem(subject);
            db.SaveSubjects();
            Logger.Write("Додано", $"Предмет ID {id}: {subjectName} ({hours} год.)");
            Console.WriteLine($"[Успішно] Предмет '{subjectName}' додано з ID: {id}");
            return id;
        }

        public void ReadSubject()
        {
            int id = InputHelper.ReadInt("Введіть ID предмета: ");
            Subject? subject = db.DBSubject.GetItem(id);
            if (subject != null)
            {
                Console.WriteLine(subject);
            }
            else
            {
                Console.WriteLine($"[Помилка] Предмет з ID: {id} не знайдено.");
            }
        }

        public void UpdateSubject()
        {
            if (!stateMachine.CheckAdminGuard("Update Subject")) return;

            int id = InputHelper.ReadInt("Введіть ID предмета для оновлення: ");
            Subject? oldSubject = db.DBSubject.GetItem(id);
            if (oldSubject == null)
            {
                Console.WriteLine($"[Помилка] Предмет з ID: {id} не знайдено.");
                return;
            }

            string subjectName = InputHelper.ReadString("Нова назва предмета: ");
            int hours = InputHelper.ReadInt("Нова кількість годин: ");

            string? error = Validator.ValidateSubject(subjectName, hours);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return;
            }

            Subject newSubject = new Subject(subjectName, hours);
            db.DBSubject.UpdateItem(oldSubject, newSubject);
            db.SaveSubjects();
            Logger.Write("Оновлено", $"Предмет ID {id}: {subjectName} ({hours} год.)");
            Console.WriteLine($"[Успішно] Предмет з ID: {id} оновлено.");
        }

        public void DeleteSubject()
        {
            if (!stateMachine.CheckAdminGuard("Delete Subject")) return;

            int id = InputHelper.ReadInt("Введіть ID предмета для видалення: ");
            Subject? subject = db.DBSubject.GetItem(id);
            if (subject == null)
            {
                Console.WriteLine($"[Помилка] Предмет з ID: {id} не знайдено.");
                return;
            }

            db.DBSubject.DeleteItem(subject);
            db.DBgroupSubject.Items.RemoveAll(gs => gs.SubjectID == id);
            db.SaveSubjects();
            db.SaveGroupSubjects();
            Logger.Write("Видалено", $"Предмет ID {id}: {subject.SubjectName}");
            Console.WriteLine($"[Успішно] Предмет з ID: {id} видалено.");
        }

        public void GetAllSubjects()
        {
            Console.WriteLine("Усі предмети:");
            foreach (Subject subject in db.DBSubject.Items)
            {
                Console.WriteLine(subject);
            }
        }
    }
}
