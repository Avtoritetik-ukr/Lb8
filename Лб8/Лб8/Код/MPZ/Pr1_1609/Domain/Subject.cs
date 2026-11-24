namespace Pr1_1609.Domain
{
    public sealed class Subject : IEntity
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Hour { get; set; }

        public Subject(string subjectName, int hour)
        {
            SubjectName = subjectName;
            Hour = hour;
        }

        public override string ToString()
        {
            return $"{Id} {SubjectName} {Hour}";
        }
    }
}
