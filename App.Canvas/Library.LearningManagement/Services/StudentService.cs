using Library.LearningManagement.DataBase;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;
using Library.LearningManagement.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class StudentService
    {


       // private List<Person> studentList;
        private static StudentService? instance;
        //singleton
        public static StudentService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentService();
                }
                return instance;
            }
        }
        private List<StudentDTO> _students;
        public List<StudentDTO> Students
        {
            get
            {
                return _students ?? (_students = new List<StudentDTO>());
            }

        }
        private StudentService()
        {
            var response = new WebRequestHandler().Get("/Students").Result;
            if (!string.IsNullOrEmpty(response))
            {
                _students = JsonConvert.DeserializeObject<List<StudentDTO>>(response) ?? new List<StudentDTO>();
            }
            else
            {
                _students = new List<StudentDTO>();
            }

            //studentList = new List<Person>();
        }
        public void Add(StudentDTO student)
        {
            Students.Add(student);
            //studentList.Add(student);
        }

        /*public List<Student> Students
        {
            get
            {
                return studentList;
            }

        }*/
        //public IEnumerable<Student> Search(string query)
        //{
        //   return Students.Where(s => (s != null) && s.Name.ToUpper().Contains(query.ToUpper()));
        //    //return studentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        //}
        //New additions for MauiHW
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            // In a real application, this method would asynchronously fetch data from a database or external service.
            // Here, we're simulating asynchronous operation using Task.FromResult to wrap the synchronous operation.
            return await Task.FromResult(FakeDatabase.People.Where(p => p is Student).Cast<Student>());
        }

        public async Task AddAsync(Student student)
        {
            // Simulating async operation. In reality, this would involve asynchronous IO operations.
            await Task.Run(() => FakeDatabase.People.Add(student));
        }

        public async Task<IEnumerable<Student>> SearchAsync(string query)
        {
            // Simulating an asynchronous search operation
            var students = await GetStudentsAsync();
            return students.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

        public bool UpdateStudent(int studentId, string newName, StudentClassification newClassification)
        {
            var student = Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                return false;

            if (!string.IsNullOrWhiteSpace(newName))
                student.Name = newName;

            if (student.Classification != newClassification)
                student.Classification = newClassification;

            // If using a real database, commit changes here
            // dbContext.SaveChanges();
            var response = new WebRequestHandler().Post("/Students", student).Result;
            var updatedStudent = JsonConvert.DeserializeObject<StudentDTO>(response);

          //  var existingStudent = _students.FirstOrDefault(c => c.Id == student.Id);
            var index = _students.IndexOf(student);
            _students.RemoveAt(index);
            _students.Insert(index, updatedStudent);

            return true;
        }

        public bool DeleteStudent(int studentId)
        {

            var handler = new WebRequestHandler().Delete($"/Students/Delete/{studentId}");
            var student = Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                Students.Remove(student);
                return true; // Successfully removed the student
            }
            return false; // Student not found
        }
        private WebRequestHandler _webRequestHandler = new WebRequestHandler();
        public async Task AddOrUpdateStudent(StudentDTO student)
        {
            var response = new WebRequestHandler().Post("/Students", student).Result;
            var updatedStudent = JsonConvert.DeserializeObject<StudentDTO>(response);
            if (updatedStudent != null)
            {
                var existingStudent = Students.FirstOrDefault(s => s.Id == updatedStudent.Id);
                if (existingStudent == null)
                {
                    Students.Add(updatedStudent);
                }
                else
                {
                    var index = Students.IndexOf(existingStudent);
                    Students.RemoveAt(index);
                    Students.Insert(index, updatedStudent);
                }
            }
        }
        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            string url = $"/api/students/{studentId}";
            var response = await _webRequestHandler.Delete(url);
            return response.Contains("Success"); // Adjust based on actual API response
        }

        public async Task<List<StudentDTO>> GetAllStudentsAsync()
        {
            string url = "/api/students";
            var response = await _webRequestHandler.Get(url);
            return JsonConvert.DeserializeObject<List<StudentDTO>>(response);
        }
        public StudentDTO? Get(int id)
        {
            return Students.FirstOrDefault(p => p.Id == id);
        }

    }
}
