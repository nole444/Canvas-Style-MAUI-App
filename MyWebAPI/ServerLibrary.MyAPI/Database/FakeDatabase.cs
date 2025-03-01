using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LearningManagement.Models;


namespace ServerLibrary.MyAPI.Database
{
    public class FakeDatabase
    {
        //Singleton pattern
        private FakeDatabase()
        {
            instance = null;

        }
        public static List<Course> Courses { get; set; } = new List<Course>();

        public static List<Student> Students { get; set; } = new List<Student>();

        private static FakeDatabase? instance;

        public static FakeDatabase Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new FakeDatabase();
                }
                return instance;
            }
        }
    }
}
