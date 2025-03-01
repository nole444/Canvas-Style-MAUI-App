using Library.LearningManagement.Models;
using Library.LearningManagement.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Utilities;
using Newtonsoft.Json;

namespace Library.LearningManagement.Services
{
    public class CourseService
    {
        // public List<Course> courseList = new List<Course>();
        // private List<Course> courseList;

       
        //Singleton pattern
        private static CourseService? instance;
        //singleton
        public static CourseService Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new CourseService();
                }
                return instance;
            }
        }
        private CourseService() {
            //courseList = new List<Course>();
          
            var response = new WebRequestHandler().Get("/Courses").Result;
            if (!string.IsNullOrEmpty(response))
            {
                _courses = JsonConvert.DeserializeObject<List<CoursesDTO>>(response) ?? new List<CoursesDTO>();
            }
            else
            {
                _courses = new List<CoursesDTO>();
            }
        }
        public void Add(CoursesDTO course)
        {
            //here i am using my Fakedatabase class to add courses for the MAUI app
            Courses.Add(course);
            //courseList.Add(course);
        }

        private List<CoursesDTO> _courses;
        public List<CoursesDTO> Courses
        {
            get
            {
                return _courses ?? (_courses = new List<CoursesDTO>());
            }

        }
        public List<Module> Modules
        {
            get
            {
                return FakeDatabase.Modules;
            }

        }
     /*   public IEnumerable<CoursesDTO> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper()) || s.Description.ToUpper().Contains(query.ToUpper()) || s.Code.ToUpper().Contains(query.ToUpper()));
        }*/
        public string AddStudentToCourseRoster(string courseCode, int studentId)
        {
            
            var course = Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.InvariantCultureIgnoreCase));
            if (course == null)
            {
                return "Course not found.";
            }

            if (course.Roster == null)
            {
                course.Roster = new List<StudentDTO>();
            }

            var student = StudentService.Current.Students.FirstOrDefault(s => s.Id == studentId);
            if (student== null)
            {
                return "Student not found.";
            }


          

            

                if (course.Roster.Any(s => s.Id == studentId))
                {
                    return "Student already enrolled in this course.";
                }
            

            course.Roster.Add(student);
            return $"Student {student.Name} added to course {course.Name}.";
        }
        public IEnumerable<CoursesDTO> GetCoursesForStudent(string studentName)
        {
            return Courses.Where(course => course.Roster.Any(student => student.Name == studentName));
        }
        public bool AddModuleToCourse(string courseCode, string moduleName, string moduleDescription)
        {
            var course = Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
            if (course == null) return false;

            Module newModule = new Module
            {
                Name = moduleName,
                Description = moduleDescription
            };

            course.Modules.Add(newModule);
            return true;
        }
        public bool AddAssignmentToCourse(string courseCode, string groupName, string assignmentName, string description, decimal points, DateTime dueDate)
        {
            var course = FakeDatabase.Courses.FirstOrDefault(c => c.Code == courseCode);
            if (course == null)
                throw new InvalidOperationException("Course not found.");

            // Find the specified group or create a new one if it does not exist
            var assignmentGroup = course.AssignmentGroups.FirstOrDefault(ag => ag.Name == groupName);
            if (assignmentGroup == null)
            {
                assignmentGroup = new AssignmentGroup
                {
                    Name = groupName,
                    Assignments = new List<Assignment>()
                };
                course.AssignmentGroups.Add(assignmentGroup);
                var assignment = new Assignment
                {
                    Name = assignmentName,
                    Description = description,
                    TotalPointsAvailable = points,
                    DueDate = dueDate
                };
                assignmentGroup.Assignments.Add(assignment);
                
                return true;
            }
            return false;
          }
                    //Method for submitting an assingment file to be graded by instructor
        public bool SubmitAssignmentToCourse(int studentId, string courseCode, int assignmentId, string filePath)
        {
            // Find the course
            var course = FakeDatabase.Courses.FirstOrDefault(c => c.Code == courseCode);
            if (course == null)
                return false;

            // Find the student
            var student = course.Roster.FirstOrDefault(p => p.Id == studentId);
            if (student == null)
                return false;

            // Find the assignment
            var assignment = course.Assignments.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment == null)
                return false;

            // Create and add the submission
            var submission = new Submission
            {
                Student = student,
                Assignment = assignment,
                FilePath = filePath
            };

            course.Submissions.Add(submission);
            return true;
        }


        public bool AddContentToModule(string courseCode, string moduleName, string contentName, string description, string path)
        {
            // Find the course with the given course code
            var course = Courses.FirstOrDefault(c => c.Code == courseCode);
            if (course == null)
            {
                throw new InvalidOperationException("Course not found.");
            }

            // Find the module within the course with the given module name
            var module = course.Modules.FirstOrDefault(m => m.Name == moduleName);
            if (module == null)
            {
                throw new InvalidOperationException("Module not found.");
            }

            // Create a new content item with the provided details
            var contentItem = new ContentItem
            {
                Name = contentName,
                Description = description,
                Path = path
            };

            // Add the new content item to the module's content list
            module.Content.Add(contentItem);

            // Assuming there is a mechanism to save or update the course details in the data store
            // SaveChanges(); // Uncomment or implement as per your context

            return true;
        }
        public bool UpdateCourse(string courseCode, string newCode, string newName, string newDescription)
        {

           
            
            var course = Courses.FirstOrDefault(c => c.Code == courseCode);
            if (course == null)
                return false;

            if (!string.IsNullOrWhiteSpace(newName))
                course.Name = newName;

            if (!string.IsNullOrWhiteSpace(newDescription))
                course.Description = newDescription;

            if(!string.IsNullOrWhiteSpace(courseCode))
                course.Code = newCode;

            // If using a real database, commit changes here
            // dbContext.SaveChanges();
           
            var response = new WebRequestHandler().Post("/Courses", course).Result;
            var updatedCourse = JsonConvert.DeserializeObject<CoursesDTO>(response);

            var existingCourse = _courses.FirstOrDefault(c => c.Id == updatedCourse.Id);
            var index = _courses.IndexOf(existingCourse);
            _courses.RemoveAt(index);
            _courses.Insert(index, updatedCourse);

            return true;
        }

        public bool DeleteCourse(string courseCode)
        {
            var courseId = Courses.FirstOrDefault(c => c.Code == courseCode).Id;
            var handler = new WebRequestHandler().Delete($"/Students/Delete/{courseId}");
            var course = Courses.FirstOrDefault(c => c.Code == courseCode);
            if (course != null)
            {
                Courses.Remove(course);
                return true; // Successfully removed the course
            }
            return false; // Course not found
        }

        private WebRequestHandler _webRequestHandler = new WebRequestHandler();
        //API editions

        public void DeleteCourse(int courseId)
        {
          
            var handler = new WebRequestHandler().Delete($"Delete/{courseId}");
            var courseToDelete = Courses.FirstOrDefault(c=> c.Id == courseId);

            if(courseToDelete != null)
            {
                Courses.Remove(courseToDelete);
            }
        }

        public CoursesDTO? Get(int id)
        {
            return Courses.FirstOrDefault(c=>c.Id == id);
        }


        // Example of updating the course with a DTO instead of individual fields
        public async Task AddOrUpdateCourse(CoursesDTO c)
        {
            var response = new WebRequestHandler().Post("/Courses", c).Result;
            var updatedCourse = JsonConvert.DeserializeObject<CoursesDTO>(response);
            if (updatedCourse != null)
            {
                var existingCourse = _courses.FirstOrDefault(c=> c.Id==updatedCourse.Id);
                if (existingCourse == null)
                {
                    _courses.Add(updatedCourse);
                }
                else
                {
                    var index = _courses.IndexOf(existingCourse);
                    _courses.RemoveAt(index);
                    _courses.Insert(index, updatedCourse);
                }
            }
        }

        public IEnumerable<CoursesDTO> Search(string query)
        {
            return Courses.Where(c=> c.Name.ToUpper().Contains(query.ToUpper()));
        }

        //public string AddStudentToCourseRoster(int courseId, int studentId)
        //{
        //    // Retrieve the course using the provided Get method
        //    var course = Get(courseId);
        //    if (course == null)
        //    {
        //        return "Course not found.";
        //    }

        //    // Retrieve the student using the provided Get method
        //    var student = StudentService.Current.Students.FirstOrDefault(s => s.Id == studentId);
        //    if (student == null)
        //    {
        //        return "Student not found.";
        //    }

        //    // Check if the student is already enrolled in the course
        //    if (course.Roster.Any(s => s.Id == studentId))
        //    {
        //        return "Student already enrolled in this course.";
        //    }

        //    // Add the student to the course roster
        //    course.Roster.Add(student);

        //    // Update the course
        //    AddOrUpdateCourse(course);

        //    return $"Student {student.Name} added to course {course.Name}.";
        //}

   
    }
}
