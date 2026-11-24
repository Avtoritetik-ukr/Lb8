using System;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Validation;
using Pr1_1609.BusinessLogic.Navigation;
using Pr1_1609.Infrastructure;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public sealed class HR_CRUD_Student : CrudBase<Student>
    {

        public HR_CRUD_Student(DB db, StateMachine stateMachine) : base(db, stateMachine)
        {
        }

        public override int Create() => CreateStudent();
        public override void Read() => ReadStudent();
        public override void Update() => UpdateStudent();
        public override void Delete() => DeleteStudent();
        public override void GetAll() => GetAllStudents();

        public int CreateStudent()
        {
            if (!stateMachine.CheckAdminGuard("Create Student")) return -1;

            string firstName = InputHelper.ReadString("Ім'я студента: ");
            string lastName = InputHelper.ReadString("Прізвище студента: ");
            int age = InputHelper.ReadInt("Вік: ");
            int groupId = InputHelper.ReadInt("ID групи: ");

            string? error = Validator.ValidateStudent(db, firstName, lastName, age, groupId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return -1;
            }

            Student student = new Student(firstName, lastName, age, groupId);
            int id = db.DBStudent.AddItem(student);
            db.SaveStudents();
            Logger.Write("Додано", $"Студент ID {id}: {firstName} {lastName}");
            Console.WriteLine($"[Успішно] Студента {firstName} {lastName} додано з ID: {id}");
            return id;
        }

        public void ReadStudent()
        {
            int id = InputHelper.ReadInt("Введіть ID студента: ");
            Student? student = db.DBStudent.GetItem(id);
            if (student != null)
            {
                Console.WriteLine(student);
            }
            else
            {
                Console.WriteLine($"[Помилка] Студента з ID: {id} не знайдено.");
            }
        }

        public void UpdateStudent()
        {
            if (!stateMachine.CheckAdminGuard("Update Student")) return;

            int id = InputHelper.ReadInt("Введіть ID студента для оновлення: ");
            Student? oldStudent = db.DBStudent.GetItem(id);
            if (oldStudent == null)
            {
                Console.WriteLine($"[Помилка] Студента з ID: {id} не знайдено.");
                return;
            }

            string firstName = InputHelper.ReadString("Нове ім'я: ");
            string lastName = InputHelper.ReadString("Нове прізвище: ");
            int age = InputHelper.ReadInt("Новий вік: ");
            int groupId = InputHelper.ReadInt("Новий ID групи: ");

            string? error = Validator.ValidateStudent(db, firstName, lastName, age, groupId);
            if (error != null)
            {
                Console.WriteLine($"[Помилка] {error}");
                return;
            }

            Student newStudent = new Student(firstName, lastName, age, groupId);
            db.DBStudent.UpdateItem(oldStudent, newStudent);
            db.SaveStudents();
            Logger.Write("Оновлено", $"Студент ID {id}: {firstName} {lastName}");
            Console.WriteLine($"[Успішно] Дані студента з ID: {id} оновлено.");
        }

        public void DeleteStudent()
        {
            if (!stateMachine.CheckAdminGuard("Delete Student")) return;

            int id = InputHelper.ReadInt("Введіть ID студента для видалення: ");
            Student? student = db.DBStudent.GetItem(id);
            if (student == null)
            {
                Console.WriteLine($"[Помилка] Студента з ID: {id} не знайдено.");
                return;
            }

            db.DBStudent.DeleteItem(student);
            int removedAddresses = db.DBAdrress.Items.RemoveAll(a => a.StudentId == id);
            db.SaveStudents();
            db.SaveAddresses();
            Logger.Write("Видалено", $"Студент ID {id} (адрес видалено: {removedAddresses})");
            Console.WriteLine($"[Успішно] Студента з ID: {id} видалено (видалено адрес: {removedAddresses}).");
        }

        public void GetAllStudents()
        {
            Console.WriteLine("Усі студенти:");
            foreach (Student student in db.DBStudent.Items)
            {
                Console.WriteLine(student);
            }
        }
    }
}
