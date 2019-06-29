namespace InMemoryDatabase.Sample.Models
{
    using InMemoryDatabase.Attributes;

    public class Student
    {
        [Identifier]
        public int ClassNumber { get; set; }

        [Identifier(2)]
        public int StudentNumber { get; set; }

        public string Name { get; set; }
    }
}