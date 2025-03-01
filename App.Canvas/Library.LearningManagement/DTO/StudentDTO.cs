using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.DTO
{


    public class StudentDTO
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public StudentClassification Classification { get; set; }  // Using the enum type directly

        // Default constructor
        public StudentDTO()
        {
            Name = string.Empty;
            Id = ++lastId;
            Classification = StudentClassification.Freshman;  // Default value can be set as needed
        }

        // Constructor that takes a Student object
        public StudentDTO(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            Id = student.Id;
            Name = student.Name;
            Classification = student.Classification;  // Directly assigning the enum
        }
        private static int lastId = 0;
        public int Id
        {
            get; private set;
        }

        // ToString method to represent the StudentDTO as a string
        public override string ToString()
        {
            return $"{Name} - {Classification}";
        }
    }
}



