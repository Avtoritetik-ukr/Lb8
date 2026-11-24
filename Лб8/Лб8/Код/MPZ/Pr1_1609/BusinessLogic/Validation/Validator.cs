using System;
using System.Linq;

using Pr1_1609.Domain;
using Pr1_1609.DataAccess;

namespace Pr1_1609.BusinessLogic.Validation
{
    public static class Validator
    {
        public const int MinAge = 16;
        public const int MaxAge = 100;
        public const int MaxHours = 1000;

        public static string? ValidateStudent(DB db, string firstName, string lastName, int age, int groupId)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return "Ім'я та прізвище не можуть бути порожніми.";
            if (age < MinAge || age > MaxAge)
                return $"Вік має бути в межах {MinAge}..{MaxAge}.";
            if (db.DBGroup.GetItem(groupId) == null)
                return $"Групи з ID {groupId} не існує.";
            return null;
        }

        public static string? ValidateAddress(DB db, string addressText, int studentId)
        {
            if (string.IsNullOrWhiteSpace(addressText))
                return "Адреса не може бути порожньою.";
            if (db.DBStudent.GetItem(studentId) == null)
                return $"Студента з ID {studentId} не існує.";
            return null;
        }

        public static string? ValidateGroup(DB db, string groupName, int excludeId = 0)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return "Назва групи не може бути порожньою.";
            if (db.DBGroup.Items.Any(g => g.Id != excludeId &&
                                          g.GroupName.Equals(groupName.Trim(), StringComparison.OrdinalIgnoreCase)))
                return $"Група '{groupName}' вже існує.";
            return null;
        }

        public static string? ValidateSubject(string subjectName, int hours)
        {
            if (string.IsNullOrWhiteSpace(subjectName))
                return "Назва предмета не може бути порожньою.";
            if (hours <= 0 || hours > MaxHours)
                return $"Кількість годин має бути в межах 1..{MaxHours}.";
            return null;
        }

        public static string? ValidateGroupSubject(DB db, int groupId, int subjectId)
        {
            if (db.DBGroup.GetItem(groupId) == null)
                return $"Групи з ID {groupId} не існує.";
            if (db.DBSubject.GetItem(subjectId) == null)
                return $"Предмета з ID {subjectId} не існує.";
            if (db.DBgroupSubject.Items.Any(gs => gs.GroupId == groupId && gs.SubjectID == subjectId))
                return "Цей предмет вже закріплено за групою.";
            return null;
        }
    }
}