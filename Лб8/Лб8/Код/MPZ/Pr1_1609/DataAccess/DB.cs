using Pr1_1609.Domain;

namespace Pr1_1609.DataAccess
{
    public sealed class DB
    {
        public DBItem<Student> DBStudent { get; set; } = new DBItem<Student>();
        public DBItem<Address> DBAdrress { get; set; } = new DBItem<Address>();
        public DBItem<Group> DBGroup { get; set; } = new DBItem<Group>();
        public DBItem<Subject> DBSubject { get; set; } = new DBItem<Subject>();
        public DBItem<GroupSubject> DBgroupSubject { get; set; } = new DBItem<GroupSubject>();
        public Configuration Config { get; } = Configuration.Load();

        public DB()
        {
            if (!LoadAll())
            {
                Inicialize();
                SaveAll();
            }
        }

        private const string StudentsFile = "students.json";
        private const string AddressesFile = "addresses.json";
        private const string GroupsFile = "groups.json";
        private const string SubjectsFile = "subjects.json";
        private const string GroupSubjectsFile = "groupSubjects.json";

        public void SaveStudents() => DBStudent.SaveToJson(Config.GetDataPath(StudentsFile), Config.BackupOnSave);
        public void SaveAddresses() => DBAdrress.SaveToJson(Config.GetDataPath(AddressesFile), Config.BackupOnSave);
        public void SaveGroups() => DBGroup.SaveToJson(Config.GetDataPath(GroupsFile), Config.BackupOnSave);
        public void SaveSubjects() => DBSubject.SaveToJson(Config.GetDataPath(SubjectsFile), Config.BackupOnSave);
        public void SaveGroupSubjects() => DBgroupSubject.SaveToJson(Config.GetDataPath(GroupSubjectsFile), Config.BackupOnSave);

        public bool LoadStudents() => DBStudent.LoadFromJson(Config.GetDataPath(StudentsFile));
        public bool LoadAddresses() => DBAdrress.LoadFromJson(Config.GetDataPath(AddressesFile));
        public bool LoadGroups() => DBGroup.LoadFromJson(Config.GetDataPath(GroupsFile));
        public bool LoadSubjects() => DBSubject.LoadFromJson(Config.GetDataPath(SubjectsFile));
        public bool LoadGroupSubjects() => DBgroupSubject.LoadFromJson(Config.GetDataPath(GroupSubjectsFile));

        public void SaveAll()
        {
            SaveStudents();
            SaveAddresses();
            SaveGroups();
            SaveSubjects();
            SaveGroupSubjects();
        }

        public bool LoadAll()
        {
            bool loaded = LoadStudents();
            loaded |= LoadAddresses();
            loaded |= LoadGroups();
            loaded |= LoadSubjects();
            loaded |= LoadGroupSubjects();
            return loaded;
        }

        public void Inicialize()
        {
            InicializeStudents();
            InicializeAddress();
            InicializeGroup();
            InicializeSubject();
            InicializeGroupSubject();
        }

        public void InicializeStudents()
        {
            DBStudent.AddItem(new Student("Іван", "Петренко", 18, 2));
            DBStudent.AddItem(new Student("Петро", "Романенко", 19, 2));
            DBStudent.AddItem(new Student("Роман", "Василенко", 20, 1));
        }

        public void InicializeAddress()
        {
            DBAdrress.AddItem(new Address("Київ", 3));
            DBAdrress.AddItem(new Address("Черкаси", 1));
            DBAdrress.AddItem(new Address("Полтава", 2));
        }

        public void InicializeGroup()
        {
            DBGroup.AddItem(new Group("ІПЗ-34"));
            DBGroup.AddItem(new Group("ІПЗ-35"));
        }

        public void InicializeSubject()
        {
            DBSubject.AddItem(new Subject("Програмування", 100));
            DBSubject.AddItem(new Subject("Математика", 200));
            DBSubject.AddItem(new Subject("Англійська мова", 300));
        }

        public void InicializeGroupSubject()
        {
            DBgroupSubject.AddItem(new GroupSubject(1, 2));
            DBgroupSubject.AddItem(new GroupSubject(1, 3));
            DBgroupSubject.AddItem(new GroupSubject(2, 1));
            DBgroupSubject.AddItem(new GroupSubject(2, 2));
        }
    }
}
