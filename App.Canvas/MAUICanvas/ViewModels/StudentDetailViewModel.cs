using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICanvas.ViewModels
{
    public class StudentDetailViewModel
    {
        public string Name { get; set; }

        public string ClassificationString { get; set; }

        public void AddStudent()
        {
            StudentClassification classification = new StudentClassification();
            
            if (ClassificationString == "S")
            {
                classification = StudentClassification.Senior;
            }
            else if (ClassificationString == "J")
            {
                classification = StudentClassification.Junior;
            }
            else if (ClassificationString == "O")
            {
                classification = StudentClassification.Sophmore;
            }
            else if (ClassificationString == "F")
            {
                classification = StudentClassification.Freshman;
            }
            else
            {
                classification = StudentClassification.Freshman;
            }


            
                var studentDto = new StudentDTO { Name = Name, Classification = classification};
                StudentService.Current.AddOrUpdateStudent(studentDto);
                Shell.Current.GoToAsync("//InstructorView");
       
       
        }
    }
}
