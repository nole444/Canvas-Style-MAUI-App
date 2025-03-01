 namespace Library.LearningManagement.Models
    {
        public class Person
        {
            public string Name { get; set; }
        private static int lastId = 0;
        public int Id
        {
            get; private set;
        }
        protected void SetId(int id)
        {
            Id = id;
        }

        // public Dictionary<int, double> Grades { get; set; }

        // public StudentClassification Classification { get; set; }

      //  public List<Student> Students { get; private set; }
        public Person()
            {
                Name = string.Empty;
                 Id = ++lastId;
            }

            public override string ToString()
            {
                return $"[{Id}] {Name}";
            }


        }

        public enum StudentClassification
        {
            Freshman, Sophmore, Junior, Senior
        }
    }

