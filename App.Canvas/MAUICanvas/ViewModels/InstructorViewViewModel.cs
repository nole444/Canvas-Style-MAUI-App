using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Library.LearningManagement.DTO;
//using static Android.Graphics.ImageDecoder;

namespace MAUICanvas.ViewModels
{
    public class InstructorViewViewModel: INotifyPropertyChanged
    {
        //private IEnumerable<string> students;
        /*public IEnumerable<string> Students
        {
            get
            {
                return students;
            }
        }*/
        // private ObservableCollection<Person> _persons;

        //Added for roster addition of students
       // public ICommand AddStudentToCourseCommand { get; private set; }
        public InstructorViewViewModel()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            //Added for course roster addition of students
         //  AddStudentToCourseCommand = new Command(AddStudentToCourse);
        }
        //Added for placing student in course roster
        public void AddStudentToCourse(string selectedCourse, int studentId)
        {
            if (SelectedStudent != null && SelectedCourse != null)
            {
                var resultMessage = CourseService.Current.AddStudentToCourseRoster(selectedCourse, studentId);
            }
        }

        public ObservableCollection<StudentDTO> People
        {
            get
            {
                //This will grab all of the students that match the query given by the instructor regardless of capitilzation
                var filteredList = StudentService.Current.Students.Where(s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<StudentDTO>(filteredList);
                //return new ObservableCollection<Person>(StudentService.Current.Students);
            }
        }

        public ObservableCollection<CoursesDTO> Courses
        {
            get
            {
                return new ObservableCollection<CoursesDTO>(CourseService.Current.Courses);
            }
        }

    
        public string Title
        {
            get => "Instructor Menu";
        }
        public bool IsEnrollmentsVisible
        {
            get; set;
        }
        public bool IsCoursesVisible
        {
            get; set;
        }
        public void ShowEnrollments()
        {
            IsEnrollmentsVisible=true;
            IsCoursesVisible=false;
            NotifyPropertyChanged("IsEnrollmentVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }
        public void ShowCourses()
        {
            IsEnrollmentsVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsEnrollmentVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public CoursesDTO SelectedCourse { get; set; }

        public StudentDTO SelectedStudent { get; set; }    
     
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadData(int studentId)
        {
            var student = StudentService.Current.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                SelectedStudent = student;
               // StudentName = student.Name;  // Assumes you have a StudentName property to bind to the UI

                // Fetches only the courses that the student is enrolled in
                var enrolledCourses = CourseService.Current.Courses.Where(course => course.Roster.Any(person => person.Id == studentId));
                SelectedStudentCourses.Clear();  // Clear existing entries
                foreach (var course in enrolledCourses)
                {

                    SelectedStudentCourses.Add(course);  // Add only the enrolled courses
                }

                NotifyPropertyChanged(nameof(SelectedStudentCourses));  // Notify the UI that the list of courses has changed
               // NotifyPropertyChanged(nameof(StudentName));  // Update the UI to show the student's name
            }
            else
            {
               SelectedStudentCourses.Clear();  // Clear the list if no student is found
               // StudentName = "Student not found";  // Update UI to reflect no student found
              //  NotifyPropertyChanged(nameof(StudentName));  // Notify the UI update
            }
        }

        private string query;
       public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
                //DisplayStudentCourses(SelectedStudent?.Id ?? 0);
            }

        }
        // Collection to hold the selected student's courses
       public ObservableCollection<CoursesDTO> SelectedStudentCourses { get; } = new ObservableCollection<CoursesDTO>();
        //public void DisplayStudentCourses(int studentId)
        //{
        //    SelectedStudentCourses.Clear(); // Clear the existing courses
        //    if (studentId != 0)
        //    {
        //        // Here you should implement the logic to get the student's courses based on studentId
        //        // For now, I'm just using an example placeholder method
        //        var enrolledCourses = CourseService.Current.GetCoursesForStudent(studentId);
        //        foreach (var course in enrolledCourses)
        //        {
        //            SelectedStudentCourses.Add(new CoursesDTO(course));
        //        }
        //    }
        //    NotifyPropertyChanged(nameof(SelectedStudentCourses)); // Notify the UI to update
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddEnrollmentClick(Shell s)
        {
            var idParam = SelectedStudent?.Id ?? 0;
            s.GoToAsync($"//StudentDetail?studentId={idParam}");
        }
        public void AddCourseClick(Shell s) {
            RefreshView();
            s.GoToAsync($"//CourseDetail");
        }
        public void RefreshView()
        {
            //Courses = new ObservableCollection<CoursesDTO>(CourseService.Current.Courses);
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }

        StudentDTO StudentModel { get; set; }
        public void SaveStudent()
        {
             StudentService.Current.AddOrUpdateStudent(StudentModel);
          
        }


    }
}
