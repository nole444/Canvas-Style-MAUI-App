using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Student : Person
    {
  

        public Dictionary<int, double> Grades { get; set; }

        public StudentClassification Classification { get; set; }

        public Student()
        {
            Grades = new Dictionary<int, double>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }
        public void SetStudentId(int id)
        {
            // Here, you could add additional logic or validation if necessary
            base.SetId(id); // Call the protected SetId method in Person
        }

    }

}

