using ServerLibrary.MyAPI.Database;
using System.Collections.Generic;
using System.Linq;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;

namespace ServerLibrary.MyAPI.EC
{
    public class StudentsEC
    {
        public IEnumerable<StudentDTO> GetAll()
        {
            return FakeDatabase.Students.Select(s => new StudentDTO(s));
        }

        public StudentDTO Get(int id)
        {
            var student = FakeDatabase.Students.FirstOrDefault(s => s.Id == id);
            return student != null ? new StudentDTO(student) : null;
        }

        public StudentDTO AddOrUpdate(StudentDTO studentDto)
        {
            var student = FakeDatabase.Students.FirstOrDefault(s => s.Id == studentDto.Id);
            if (student == null)
            {
                student = new Student
                {
                    Name = studentDto.Name,
                    Classification =studentDto.Classification
                };
                FakeDatabase.Students.Add(student);
            }
            else
            {
                student.Name = studentDto.Name;
                student.Classification = studentDto.Classification;
            }
            return new StudentDTO(student);
        }

        public StudentDTO Delete(int id)
        {
            var student = FakeDatabase.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                FakeDatabase.Students.Remove(student);
                return new StudentDTO(student);
            }
            return null;
        }

        public IEnumerable<StudentDTO> Search(string query = "")
        {
            int.TryParse(query, out int queryId);  // Try to convert the query to an integer for ID comparison

            return FakeDatabase.Students
                .Where(s => s.Name.ToUpper().Contains(query.ToUpper()) || s.Id == queryId)  // Check if the name contains the query or the ID matches
                .Take(1000)  // Limit the results to prevent performance issues
                .Select(s => new StudentDTO(s));  // Transform each Student to a StudentDTO
        }
    }
}

