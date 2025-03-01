using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Course
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Code { get; set; }

        public List<Student>? Roster { get; set; }
        private static int lastId = 0;
        public int? Id
        {
            get; private set;
        }
        protected void SetId(int id)
        {
            Id = id;
        }

       public List<AssignmentGroup>? AssignmentGroups { get; set; }

        //Compresses assignments in a group
        public IEnumerable<Assignment>? Assignment
        {
            get
            {
                return AssignmentGroups.SelectMany(ag => ag.Assignments);
            }
            
         }

        public List<Assignment>? Assignments { get; set; }

        //List is needed because IEnumarables are typically used for read only access
        public List<Submission>? Submissions { get; set; }    

        public List<Module>? Modules { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} \nCode: {Code}\nName: {Name} \nDescription: {Description}";
        }

        public Course()
        {

            Roster = new List<Student>();
            AssignmentGroups = new List<AssignmentGroup>();
            Modules = new List<Module>();
            Submissions = new List<Submission>();
            Id = ++lastId;
        }

        public string Display
        {
            get
            {
                return $"{ToString()} \n {Description} \n \n" +
                    $"Roster: \n {string.Join("\n", Roster.Select(s => s.ToString()).ToArray())}"+
                    $"Assignments: \n {string.Join("\n", AssignmentGroups.Select(s => s.ToString()).ToArray())}" +
                    $"Modules: \n {string.Join("\n", Modules.Select(s => s.ToString().ToArray()))}";
            }
                
        }
    }
}

