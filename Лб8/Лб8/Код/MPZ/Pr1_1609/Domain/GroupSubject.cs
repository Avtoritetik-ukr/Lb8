namespace Pr1_1609.Domain
{
    public sealed class GroupSubject : IEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectID { get; set; }

        public GroupSubject(int groupId, int subjectID)
        {
            GroupId = groupId;
            SubjectID = subjectID;
        }

        public override string ToString()
        {
            return $"{Id} {GroupId} {SubjectID}";
        }
    }
}
