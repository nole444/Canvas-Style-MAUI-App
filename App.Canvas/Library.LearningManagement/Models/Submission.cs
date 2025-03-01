using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Submission
    {
       
        private int lastId =0;
        public int Id
        {
            get; private set;
        }
       public Person? Student {  get; set; }
        
        public Assignment Assignment { get; set; }   

        public string Content { get; set; } 

        public decimal Grade { get; set; }

        public string FilePath { get; set; }



        public Submission()
        {
            Id = ++lastId;
            Content = string.Empty;
            FilePath = string.Empty;
        }

        public override string ToString()
        {
            return $"{Id} {Student.Name}: {Assignment} {Grade}";
        }
    }
}
