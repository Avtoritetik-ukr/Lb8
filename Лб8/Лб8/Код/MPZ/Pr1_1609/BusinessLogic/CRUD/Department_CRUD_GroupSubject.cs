using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Validation;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public sealed class Department_CRUD_GroupSubject : CrudBase<GroupSubject>
    {

        public Department_CRUD_GroupSubject(DB db, StateMachine stateMachine) : base(db, stateMachine)
        {
        }

        public override int Create() => CreateGroupSubject();
        public override void Read() => ReadGroupSubject();
        public override void Update() => UpdateGroupSubject();
        public override void Delete() => DeleteGroupSubject();
        public override void GetAll() => GetAllGroupSubjects();

        public int CreateGroupSubject()
        {
            if (!stateMachine.CheckAdminGuard("Create GroupSubject")) return -1;

            int groupId = InputHelper.ReadInt("ID групи: ");
            int subjectId = InputHelper.ReadInt("ID предмета: ");

            string? error = Validator.ValidateGroupSubject(db, groupId, subjectId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return -1;
            }

            GroupSubject groupSubject = new GroupSubject(groupId, subjectId);
            int id = db.DBgroupSubject.AddItem(groupSubject);
            db.SaveGroupSubjects();
            Logger.Write("Додано", $"Закріплення ID {id}: група ID {groupId} — предмет ID {subjectId}");
            Console.WriteLine($"[Успішно] Закріплення додано з ID: {id}");
            return id;
        }

        public void ReadGroupSubject()
        {
            int id = InputHelper.ReadInt("Введіть ID закріплення: ");
            GroupSubject? gs = db.DBgroupSubject.GetItem(id);
            if (gs != null)
            {
                string groupName = db.DBGroup.GetItem(gs.GroupId)?.GroupName ?? "?";
                string subjectName = db.DBSubject.GetItem(gs.SubjectID)?.SubjectName ?? "?";
                Console.WriteLine($"{gs.Id} {groupName} (ID {gs.GroupId}) — {subjectName} (ID {gs.SubjectID})");
            }
            else
            {
                Console.WriteLine($"[Помилка] Закріплення з ID: {id} не знайдено.");
            }
        }

        public void UpdateGroupSubject()
        {
            if (!stateMachine.CheckAdminGuard("Update GroupSubject")) return;

            int id = InputHelper.ReadInt("Введіть ID закріплення для оновлення: ");
            GroupSubject? oldGroupSubject = db.DBgroupSubject.GetItem(id);
            if (oldGroupSubject == null)
            {
                Console.WriteLine($"[Помилка] Закріплення з ID: {id} не знайдено.");
                return;
            }

            int groupId = InputHelper.ReadInt("Новий ID групи: ");
            int subjectId = InputHelper.ReadInt("Новий ID предмета: ");

            string? error = Validator.ValidateGroupSubject(db, groupId, subjectId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return;
            }

            GroupSubject newGroupSubject = new GroupSubject(groupId, subjectId);
            db.DBgroupSubject.UpdateItem(oldGroupSubject, newGroupSubject);
            db.SaveGroupSubjects();
            Logger.Write("Оновлено", $"Закріплення ID {id}: група ID {groupId} — предмет ID {subjectId}");
            Console.WriteLine($"[Успішно] Закріплення з ID: {id} оновлено.");
        }

        public void DeleteGroupSubject()
        {
            if (!stateMachine.CheckAdminGuard("Delete GroupSubject")) return;

            int id = InputHelper.ReadInt("Введіть ID закріплення для видалення: ");
            GroupSubject? groupSubject = db.DBgroupSubject.GetItem(id);
            if (groupSubject != null)
            {
                db.DBgroupSubject.DeleteItem(groupSubject);
                db.SaveGroupSubjects();
                Logger.Write("Видалено", $"Закріплення ID {id}");
                Console.WriteLine($"[Успішно] Закріплення з ID: {id} видалено.");
            }
            else
            {
                Console.WriteLine($"[Помилка] Закріплення з ID: {id} не знайдено.");
            }
        }

        public void GetAllGroupSubjects()
        {
            Console.WriteLine("Усі закріплення предметів за групами:");
            foreach (GroupSubject gs in db.DBgroupSubject.Items)
            {
                string groupName = db.DBGroup.GetItem(gs.GroupId)?.GroupName ?? "?";
                string subjectName = db.DBSubject.GetItem(gs.SubjectID)?.SubjectName ?? "?";
                Console.WriteLine($"{gs.Id} {groupName} (ID {gs.GroupId}) — {subjectName} (ID {gs.SubjectID})");
            }
        }
    }
}
