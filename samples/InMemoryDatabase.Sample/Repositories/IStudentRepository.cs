namespace InMemoryDatabase.Sample.Repositories
{
    using System.Collections.Generic;
    using InMemoryDatabase.Sample.Models;

    public interface IStudentRepository
    {
        string Save(StudentDTO student);

        StudentDTO Get(string id);

        IList<StudentDTO> Get(string name = null, int? classNumber = null, int? sutdenNumber = null);

        void Update(StudentDTO student);

        void Delete(string id);
    }
}