using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Assignment
    {
        public int Id 
        {
            get; private set;
        } 
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal TotalPointsAvailable { get; set; }

        public DateTime DueDate { get; set; }

        private static int lastId = 0; 

        public override string ToString()
        {
            return $" {Id} {Name} {Description} {DueDate}";    
        }
        public Assignment()
        {
            Id = ++lastId;
        }

    }
}
