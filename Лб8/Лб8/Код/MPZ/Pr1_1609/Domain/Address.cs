namespace Pr1_1609.Domain
{
    public sealed class Address : IEntity
    {
        public int Id { get; set; }
        public string StudentAdrress { get; set; }
        public int StudentId { get; set; }

        public Address(string studentAdrress, int studentId)
        {
            StudentAdrress = studentAdrress;
            StudentId = studentId;
        }

        public override string ToString()
        {
            return $"{Id} {StudentAdrress} {StudentId}";
        }
    }
}
