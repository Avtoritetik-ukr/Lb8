using System;
using System.Linq;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Validation;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public sealed class HR_CRUD_Group : CrudBase<Group>
    {

        public HR_CRUD_Group(DB db, StateMachine stateMachine) : base(db, stateMachine)
        {
        }

        public override int Create() => CreateGroup();
        public override void Read() => ReadGroup();
        public override void Update() => UpdateGroup();
        public override void Delete() => DeleteGroup();
        public override void GetAll() => GetAllGroups();

        public int CreateGroup()
        {
            if (!stateMachine.CheckAdminGuard("Create Group")) return -1;

            string groupName = InputHelper.ReadString("Назва групи: ");

            string? error = Validator.ValidateGroup(db, groupName);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return -1;
            }

            Group group = new Group(groupName);
            int id = db.DBGroup.AddItem(group);
            db.SaveGroups();
            Logger.Write("Додано", $"Група ID {id}: {groupName}");
            Console.WriteLine($"[Успішно] Групу '{groupName}' додано з ID: {id}");
            return id;
        }

        public void ReadGroup()
        {
            int id = InputHelper.ReadInt("Введіть ID групи: ");
            Group? group = db.DBGroup.GetItem(id);
            if (group != null)
            {
                Console.WriteLine(group);
            }
            else
            {
                Console.WriteLine($"[Помилка] Групи з ID: {id} не знайдено.");
            }
        }

        public void UpdateGroup()
        {
            if (!stateMachine.CheckAdminGuard("Update Group")) return;

            int id = InputHelper.ReadInt("Введіть ID групи для оновлення: ");
            Group? oldGroup = db.DBGroup.GetItem(id);
            if (oldGroup == null)
            {
                Console.WriteLine($"[Помилка] Групи з ID: {id} не знайдено.");
                return;
            }

            string groupName = InputHelper.ReadString("Нова назва групи: ");

            string? error = Validator.ValidateGroup(db, groupName, id);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return;
            }

            Group newGroup = new Group(groupName);
            db.DBGroup.UpdateItem(oldGroup, newGroup);
            db.SaveGroups();
            Logger.Write("Оновлено", $"Група ID {id}: {groupName}");
            Console.WriteLine($"[Успішно] Групу з ID: {id} оновлено.");
        }

        public void DeleteGroup()
        {
            if (!stateMachine.CheckAdminGuard("Delete Group")) return;

            int id = InputHelper.ReadInt("Введіть ID групи для видалення: ");
            Group? group = db.DBGroup.GetItem(id);
            if (group == null)
            {
                Console.WriteLine($"[Помилка] Групи з ID: {id} не знайдено.");
                return;
            }

            int studentsCount = db.DBStudent.Items.Count(s => s.GroupId == id);
            if (studentsCount > 0)
            {
                Console.WriteLine($"[Помилка] Неможливо видалити групу: у ній {studentsCount} студент(ів).");
                return;
            }

            db.DBGroup.DeleteItem(group);
            db.DBgroupSubject.Items.RemoveAll(gs => gs.GroupId == id);
            db.SaveGroups();
            db.SaveGroupSubjects();
            Logger.Write("Видалено", $"Група ID {id}: {group.GroupName}");
            Console.WriteLine($"[Успішно] Групу з ID: {id} видалено.");
        }

        public void GetAllGroups()
        {
            Console.WriteLine("Усі групи:");
            foreach (Group group in db.DBGroup.Items)
            {
                Console.WriteLine(group);
            }
        }
    }
}
