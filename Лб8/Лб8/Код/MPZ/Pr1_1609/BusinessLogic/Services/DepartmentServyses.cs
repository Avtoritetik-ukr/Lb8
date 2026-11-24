using System;
using System.Linq;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;

namespace Pr1_1609.BusinessLogic.Services
{
    public sealed class DepartmentServyses
    {
        private readonly DB db;

        public DepartmentServyses(DB db)
        {
            this.db = db;
        }

        public void GetAllInformationAboutStudients()
        {
            if (db.DBStudent.Items.Count == 0)
            {
                Console.WriteLine("Список студентів порожній.");
                return;
            }

            foreach (Student student in db.DBStudent.Items)
            {
                Console.WriteLine(student);
                foreach (Address address in db.DBAdrress.Items.Where(a => a.StudentId == student.Id))
                {
                    Console.WriteLine("\tАдреса: " + address.StudentAdrress);
                }

                Group? group = db.DBGroup.GetItem(student.GroupId);
                if (group != null)
                {
                    Console.WriteLine("\tГрупа: " + group.GroupName);
                    foreach (GroupSubject groupSubject in db.DBgroupSubject.Items.Where(gs => gs.GroupId == group.Id))
                    {
                        Subject? subject = db.DBSubject.GetItem(groupSubject.SubjectID);
                        if (subject != null)
                        {
                            Console.WriteLine($"\t\tПредмет: {subject.SubjectName} ({subject.Hour} год.)");
                        }
                    }
                }
            }
        }

        public void GetStudentsWithAddresses()
        {
            if (db.DBStudent.Items.Count == 0)
            {
                Console.WriteLine("Список студентів порожній.");
                return;
            }

            Console.WriteLine("Студенти та їх адреси:");
            foreach (Student student in db.DBStudent.Items)
            {
                var addresses = db.DBAdrress.Items.Where(a => a.StudentId == student.Id).ToList();
                if (addresses.Count > 0)
                {
                    foreach (var addr in addresses)
                    {
                        Console.WriteLine($"{student} -> Адреса: {addr.StudentAdrress}");
                    }
                }
                else
                {
                    Console.WriteLine($"{student} -> Адреса: не вказана");
                }
            }
        }

        public void GetStudentInfoByLastName(string lastName)
        {
            var matches = db.DBStudent.Items
                .Where(s => s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
            {
                Console.WriteLine($"[Помилка] Студента з прізвищем '{lastName}' не знайдено.");
                return;
            }

            foreach (var student in matches)
            {
                Console.WriteLine("\n--- Знайдено студента ---");
                Console.WriteLine(student);

                var address = db.DBAdrress.Items.FirstOrDefault(a => a.StudentId == student.Id);
                if (address != null) Console.WriteLine($"Адреса: {address.StudentAdrress}");

                Group? group = db.DBGroup.GetItem(student.GroupId);
                if (group != null)
                {
                    Console.WriteLine($"Група: {group.GroupName}");
                    Console.WriteLine("Предмети групи:");
                    foreach (var gs in db.DBgroupSubject.Items.Where(x => x.GroupId == group.Id))
                    {
                        Subject? subj = db.DBSubject.GetItem(gs.SubjectID);
                        if (subj != null) Console.WriteLine($" - {subj.SubjectName} ({subj.Hour} год.)");
                    }
                }
            }
        }

        public void GetStudentWorkload(int studentId)
        {
            Student? student = db.DBStudent.GetItem(studentId);
            if (student == null)
            {
                Console.WriteLine("[Помилка] Студента не знайдено.");
                return;
            }

            int totalHours = 0;
            Console.WriteLine($"\nСтудент: {student.FirstName} {student.LastName} (група ID: {student.GroupId})");
            foreach (var gs in db.DBgroupSubject.Items.Where(x => x.GroupId == student.GroupId))
            {
                Subject? subj = db.DBSubject.GetItem(gs.SubjectID);
                if (subj != null)
                {
                    Console.WriteLine($" - {subj.SubjectName}: {subj.Hour} год.");
                    totalHours += subj.Hour;
                }
            }

            Console.WriteLine($"Загальне навантаження: {totalHours} годин.");
        }
    }
}
