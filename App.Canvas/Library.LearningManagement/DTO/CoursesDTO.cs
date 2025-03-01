using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.LearningManagement.DTO
{
        public class CoursesDTO
        {
        public List<Module>? Modules { get; set; }
        public List<Assignment>? Assignments { get; set; }

        public List<StudentDTO>? Roster { get; set; }

        private static int lastId = 0;
        public int? Id
        {
            get; private set;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Code {  get; set; }

        public CoursesDTO()
        {
            Name = string.Empty;
            Description = string.Empty;
            Code = string.Empty;
            Id = ++lastId;
        }

        public CoursesDTO(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Code = course.Code;
           Description = course.Description;
            Modules = new List<Module>(course.Modules);
            Assignments = course.Assignments;
        //    Roster = course.Roster;
        }

        public override string ToString()
        {
            return $"Code: {Code}\nName: {Name} \nDescription: {Description}";
        }
    }
    }

