namespace InMemoryDatabase.Sample.Models.Adapters
{
    using InMemoryDatabase.Extensions;

    public static class StudentAdapters
    {
        public static Student ToStudent(this StudentDTO dto)
        {
            return new Student
            {
                ClassNumber = dto.ClassNumber,
                Name = dto.Name,
                StudentNumber = dto.StudentNumber
            };
        }

        public static StudentDTO ToDTO(this Student student)
        {
            return new StudentDTO
            {
                ClassNumber = student.ClassNumber,
                Name = student.Name,
                StudentNumber = student.StudentNumber,
                _id = student.GetIdentifier()
            };
        }
    }
}