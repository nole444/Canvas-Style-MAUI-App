using Library.LearningManagement.Models;
using App.Canvas.Helpers;
using System.ComponentModel.DataAnnotations;
using Library.LearningManagement.Services;

namespace App.Canvas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var studentSrv = new StudentService();
            var studentHelper = new StudentsHelper();
            var courseHelper = new CourseHelper();
           

            int cont = 1;

            while (cont == 1)
            {
                Console.WriteLine("Choose an option: ");

                Console.WriteLine("1. Add a student to enrollment");

                Console.WriteLine("2. Update enrolled students");

                Console.WriteLine("3. List all students who are enrolled");

                Console.WriteLine("4. Search for students enrolled");

                Console.WriteLine("5. Add student to course roster");

                Console.WriteLine("6. Add a course");

                Console.WriteLine("7. Update a course");

                Console.WriteLine("8. List all courses");

                Console.WriteLine("9. Search for a course");

                Console.WriteLine("10. Remove a student from a course");

                Console.WriteLine("11. Add a module to a course");

                Console.WriteLine("12. Delete a module");

                Console.WriteLine("13. Submit an assignment");

                Console.WriteLine("14. List all submissions");

                Console.WriteLine("15. Grade a submission");

                Console.WriteLine("16. Exit");

                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {

                    if (result == 1)
                    {
                        studentHelper.AddOrUpdateStudent();
                    }
                    else if(result == 2)
                    {
                       // studentHelper.UpdateStudentRecord();   
                    }
                    else if (result == 3)
                    {
                        studentHelper.ListStudents();
                    }
                    else if (result == 4)
                    {
                       // studentHelper.SearchForStudents();
                    }
                    else if(result == 5)
                    {
                       // courseHelper.AddStudentToCourse();
                    }
                    else if (result == 6)
                    {
                       // courseHelper.AddOrUpdateCourse();
                    }
                    else if(result == 7)
                    {
                       // courseHelper.UpdateCourseRecord();
                    }
                    else if(result == 8)
                    {
                       // courseHelper.ListCourses();
                      // courseHelper.SearchForCourses();
                    }
                    else if(result == 9)
                    {
                        Console.WriteLine("Enter a query: ");
                        var query = Console.ReadLine() ?? string.Empty;
                       // courseHelper.SearchForCourses(query);
                    }
                    else if(result == 10)
                    {
                        courseHelper.RemoveStudentFromCourse();
                    }
                    else if(result == 11)
                    {
                       // courseHelper.AddModule();
                    }
                    else if(result == 12)
                    {
                       // courseHelper.AddAssignment();
                    }
                    else if(result == 13)
                    {
                        //courseHelper.AddSubmission();
                    }
                    else if(result == 14)
                    {
                       // courseHelper.ListSubmissions();
                    }
                    else if(result == 15)
                    {
                       // courseHelper.GradeSubmission();
                    }
                    else if (result == 16)
                    {
                        cont = 0;
                    }
                }
            }

        }
    }
}

