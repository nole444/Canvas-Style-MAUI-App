using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.LearningManagement.DTO;

namespace MAUICanvas.ViewModels
{
    
    public class StudentViewViewModel : INotifyPropertyChanged
    {
       
        public string? Name { get; set; }
        public string? StudentClassification { get; set; }

        public ObservableCollection<StudentDTO> Students { get; private set; }
        public ICommand SelectStudentCommand { get; private set; }

        public string Title
        {
            get => "Student Menu";
        }
        private StudentDTO chosenStudent;
        public StudentDTO SelectedStudent 
        {
            
            get=> chosenStudent;
            set
            {
                if (chosenStudent != value)
                {
                    chosenStudent = value;
                    OnPropertyChanged(nameof(SelectedStudent));
                    NotifyPropertyChanged(nameof(Courses));
                    // Execute navigation when a student is selected
                    //NavigateToStudentCourses(value);
                }
            }
        
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public StudentViewViewModel()
        {
            Students = new ObservableCollection<StudentDTO>(StudentService.Current.Students);
            RefreshView();
            SelectedContentItem = new FileItem();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public void NavigateToStudentCourses(Student student)
        //{
        //    if (student != null)
        //    {
        //        // Use Shell navigation to go to the student courses page, passing the student ID as a parameter
        //        Shell.Current.GoToAsync($"//StudentCoursesView?studentId={student.Id}");
        //    }
        //}
        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
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


        private ObservableCollection<CoursesDTO> _courses = new ObservableCollection<CoursesDTO>();

        public ObservableCollection<CoursesDTO> Courses
        {
            get => _courses;
            set
            {
                _courses = value;
                NotifyPropertyChanged(nameof(Courses));
            }
        }

        // Properties for StudentName, StudentClassification, etc., need to be defined similarly


        public void RefreshView()
        {
            // Example: Re-fetch the students from the data source
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }
        public ObservableCollection<CoursesDTO> SelectedStudentCourses { get; } = new ObservableCollection<CoursesDTO>();
        public async Task LoadData(int studentId)
        {
            var student = StudentService.Current.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                SelectedStudent = student;
                StudentName = student.Name;  // Assumes you have a StudentName property to bind to the UI

                // Fetches only the courses that the student is enrolled in
                //var enrolledCourses = CourseService.Current.Courses.Where(course => course.Roster.Any(person => person.Id == studentId));
                //Courses.Clear();  // Clear existing entries
                //foreach (var course in enrolledCourses)
                //{

                //    Courses.Add(course);  // Add only the enrolled courses
                //}

                NotifyPropertyChanged(nameof(Courses));  // Notify the UI that the list of courses has changed
                NotifyPropertyChanged(nameof(StudentName));  // Update the UI to show the student's name
            }
            else
            {
                Courses.Clear();  // Clear the list if no student is found
                StudentName = "Student not found";  // Update UI to reflect no student found
                NotifyPropertyChanged(nameof(StudentName));  // Notify the UI update
            }
            //// Example: Fetch data (ensure these tasks are awaited and return the expected data)
            //var student = await StudentService.Current.GetStudentsAsync(); // Adjust this call as needed

            //// Simulate fetching courses for the student
            //var courses = await Task.Run(() => CourseService.Current.GetCoursesForStudent(studentId));
            //Courses.Clear();
            //foreach (var course in courses)
            //{
            //    Courses.Add(course);
            //}

            //// Make sure OnPropertyChanged is called for each updated property
            //OnPropertyChanged(nameof(People));
            //OnPropertyChanged(nameof(Courses));
        }
        public void LoadStudentData(int studentId)
        {
            var student = StudentService.Current.Students.FirstOrDefault(s => s.Id == studentId);
            SelectedStudent = student;
            StudentName = student.Name;
            // Assuming you have access to CourseService and it can fetch courses for a given student ID
            var courses = CourseService.Current.GetCoursesForStudent(student.Name);
            // Assuming StudentCourses is bound to the UI, populate it with the fetched courses
            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
            NotifyPropertyChanged(nameof(Courses));
            NotifyPropertyChanged(nameof(StudentName));
        }

        private string studentName;
        public string StudentName
        {
            get => studentName;
            set
            {
                if (studentName != value)
                {
                    studentName = value;
                    OnPropertyChanged(nameof(StudentName));
                }
            }
        }

      

        public string Description
        {
            get => course?.Description ?? string.Empty;
            set { if (course != null) course.Description = value; }
        }


        public int Id { get; set; }

        public string CourseCode
        {
            get => course?.Code ?? string.Empty;
            set { if (course != null) course.Code = value; }

        }
        private CoursesDTO course;
        
        public string SubmissionContent { get; set; }

        private FileItem _selectedContentItem;
        public FileItem SelectedContentItem
        {
            get => _selectedContentItem;
            set
            {
                if (_selectedContentItem != value)
                {
                    _selectedContentItem = value;
                    NotifyPropertyChanged(nameof(SelectedContentItem));
                }
            }
        }
        private CoursesDTO _selectedCourse;
        public CoursesDTO SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                if (_selectedCourse != value)
                {
                    _selectedCourse = value;
                    NotifyPropertyChanged(nameof(SelectedCourse)); 
                }
            }
        }


        private Assignment _selectedAssignment;
        public Assignment SelectedAssignment
        {
            get => _selectedAssignment;
            set
            {
                if (_selectedAssignment != value)
                {
                    _selectedAssignment = value;
                    NotifyPropertyChanged(nameof(SelectedAssignment));
                }
            }
        }


        public bool SubmitAssignment()
        {
            if (SelectedContentItem?.Path == null || SelectedStudent == null || SelectedAssignment == null)
            {
                return false;
            }

            int studentId = SelectedStudent.Id;
            string selectedCourseCode = SelectedCourse?.Code;
            int assignmentId = SelectedAssignment.Id;
            string fileItem = SelectedContentItem.Path;

            return CourseService.Current.SubmitAssignmentToCourse(studentId, selectedCourseCode, assignmentId, fileItem);
        }
    }
}

