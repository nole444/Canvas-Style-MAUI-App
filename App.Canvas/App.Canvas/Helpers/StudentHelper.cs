using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Canvas.Helpers
{
    internal class StudentsHelper
    {
        private StudentService studentService;
        private CourseService courseService;

        public StudentsHelper()
        {
            studentService = StudentService.Current;

            courseService = CourseService.Current; 
        }
        public void AddOrUpdateStudent(Student? studentSelected = null)
        {

            Console.WriteLine("Enter student id: ");
            var id = Console.ReadLine();

            Console.WriteLine("Enter student name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter student classification [(F)reshman, S(O)phmore, (J)unior, (S)enior]: ");
            var classification = Console.ReadLine() ?? string.Empty;

            StudentClassification classifEnum;

            if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                classifEnum = StudentClassification.Junior;
            }
            else if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                classifEnum = StudentClassification.Sophmore;
            }
            else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                classifEnum = StudentClassification.Senior;
            }
            else
            {
                classifEnum = StudentClassification.Freshman;
            }

            bool create = false;

            if (studentSelected == null)
            {
                  create = true;
                  studentSelected = new Student();
            }

            studentSelected.SetStudentId(int.Parse(id ?? "0"));
            studentSelected.Name = name ?? string.Empty;
            studentSelected.Classification = classifEnum;

            if (create)
            {
              //  studentService.Add(studentSelected);
            }

        }

        //Add back
      /*  public void UpdateStudentRecord()
        {
            studentService.Students.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("Select a student to update: ");
            

            var inputStr = Console.ReadLine();

            if(int.TryParse(inputStr, out int inputInt))
            {
                var studentSelected = studentService.Students.FirstOrDefault(studentService => studentService.Id == inputInt);
                if(studentSelected != null)
                {
                    AddOrUpdateStudent(studentSelected);
                }
            }
        }*/
        public void ListStudents()
        {
            studentService.Students.ToList().ForEach(Console.WriteLine);

            Console.WriteLine("Choose a student");

            var inputStr = Console.ReadLine();

            var inputInt = int.Parse(inputStr ?? "0");

            Console.WriteLine("Students courses:");

           courseService.Courses.Where(c => c.Roster.Any(s => s.Id == inputInt)).ToList().ForEach(Console.WriteLine);
        }


        //Add BACk
     //   public void SearchForStudents()
      /*  {
            Console.WriteLine("Enter a query: ");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);
        }*/



    }
}
