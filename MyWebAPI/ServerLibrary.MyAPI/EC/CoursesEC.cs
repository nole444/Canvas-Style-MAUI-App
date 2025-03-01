using ServerLibrary.MyAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;



namespace ServerLibrary.MyAPI.EC
{
    public class CoursesEC
    {
        // Get all courses and map them to CourseDTOs
        public IEnumerable<CoursesDTO> GetAll()
        {
            return FakeDatabase.Courses.Select(c => new CoursesDTO(c));
        }

        // Get a single course by ID and map it to CourseDTO
        public CoursesDTO Get(int id)
        {
            var course = FakeDatabase.Courses.FirstOrDefault(c => c.Id == id);
            return course != null ? new CoursesDTO(course) : null;
        }

        // Add a new course or update an existing one based on the provided CourseDTO
        public CoursesDTO AddOrUpdate(CoursesDTO courseDto)
        {
            var course = FakeDatabase.Courses.FirstOrDefault(c => c.Id == courseDto.Id);
            if (course == null)
            {
                // Adding new course
                course = new Course
                {
                   
                    Name = courseDto.Name,
                    Code = courseDto.Code,
                    Description = courseDto.Description
                };
                FakeDatabase.Courses.Add(course);
            }
            else
            {
                // Updating existing course
                course.Name = courseDto.Name;
                course.Description = courseDto.Description;
            }
            return new CoursesDTO(course);
        }

        // Delete a course and return the CourseDTO of the deleted course
        public CoursesDTO Delete(int id)
        {
            var course = FakeDatabase.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                FakeDatabase.Courses.Remove(course);
                return new CoursesDTO(course);
            }
            return null;
        }

        public IEnumerable<CoursesDTO> Search(string query = "")
        {
            return FakeDatabase.Courses.Where(c => c.Name.ToUpper()
                .Contains(query.ToUpper()))
                .Take(1000)
                .Select(c => new CoursesDTO(c));
        }
    }
}
