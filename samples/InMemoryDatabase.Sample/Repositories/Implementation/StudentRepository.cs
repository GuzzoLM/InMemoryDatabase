namespace InMemoryDatabase.Sample.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using InMemoryDatabase.Sample.Models;
    using InMemoryDatabase.Sample.Models.Adapters;

    public class StudentRepository : IStudentRepository
    {
        private readonly IInMemoryCollection<Student> _studentCollection;

        public StudentRepository(IInMemoryCollection<Student> studentCollection)
        {
            _studentCollection = studentCollection;
        }

        public void Delete(string id)
        {
            _studentCollection.Delete(id);
        }

        public StudentDTO Get(string id)
        {
            try
            {
                return _studentCollection.Get(id).ToDTO();
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public IList<StudentDTO> Get(string name = null, int? classNumber = null, int? sutdentNumber = null)
        {
            IEnumerable<Student> students = null;
            Func<Student, bool> filter;

            if (!string.IsNullOrEmpty(name))
            {
                filter = x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
                students = students?.Where(filter)
                    ?? _studentCollection.Where(filter);
            }

            if (classNumber.HasValue)
            {
                filter = x => x.ClassNumber == classNumber.Value;
                students = students?.Where(filter)
                    ?? _studentCollection.Where(filter);
            }

            if (sutdentNumber.HasValue)
            {
                filter = x => x.StudentNumber == sutdentNumber.Value;
                students = students?.Where(filter)
                    ?? _studentCollection.Where(filter);
            }

            return students?.Select(x => x.ToDTO())?.ToList()
                ?? new List<StudentDTO>();
        }

        public string Save(StudentDTO student)
        {
            return _studentCollection.Save(student.ToStudent());
        }

        public void Update(StudentDTO student)
        {
            _studentCollection.Update(student.ToStudent());
        }
    }
}