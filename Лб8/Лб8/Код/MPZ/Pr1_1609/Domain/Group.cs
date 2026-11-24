namespace Pr1_1609.Domain
{
    public sealed class Group : IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public Group(string groupName)
        {
            GroupName = groupName;
        }

        public override string ToString()
        {
            return $"{Id} {GroupName}";
        }
    }
}
