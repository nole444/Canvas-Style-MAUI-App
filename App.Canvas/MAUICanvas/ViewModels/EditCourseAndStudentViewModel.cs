using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUICanvas.ViewModels
{
    class EditCourseAndStudentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CoursesDTO> Courses
        {
            get
            {
                return new ObservableCollection<CoursesDTO>(CourseService.Current.Courses);
            }
        }

        public ObservableCollection<StudentDTO> Students { get; set; }

        private CoursesDTO _selectedCourse;
        public CoursesDTO SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                if (_selectedCourse != value)
                {
                    _selectedCourse = value;
                    OnPropertyChanged(nameof(SelectedCourse));
                }
            }
        }
        private StudentDTO _selectedStudent;
        public StudentDTO SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    OnPropertyChanged(nameof(SelectedStudent));
                }
            }
        }

        public string NewCourseCode { get; set; }

        public string NewClassification {  get; set; }


        public ICommand UpdateCourseCommand { get; private set; }
        public ICommand UpdateStudentCommand { get; private set; }

        public ICommand DeleteCourseCommand { get; private set; }
        public ICommand DeleteStudentCommand { get; private set; }

        public EditCourseAndStudentViewModel()
        {
            CourseService courseService = CourseService.Current;
            StudentService studentService = StudentService.Current;

            Students = new ObservableCollection<StudentDTO>(studentService.Students.Cast<StudentDTO>());

            UpdateCourseCommand = new Command(() =>
            {

                if (SelectedCourse != null)
                    CourseService.Current.UpdateCourse(SelectedCourse.Code, NewCourseCode, SelectedCourse.Name, SelectedCourse.Description);
            });

            UpdateStudentCommand = new Command(() =>
            {
                StudentClassification classification = new StudentClassification();

                if (NewClassification == "S")
                {
                    classification = StudentClassification.Senior;
                }
                else if (NewClassification == "J")
                {
                    classification = StudentClassification.Junior;
                }
                else if (NewClassification == "O")
                {
                    classification = StudentClassification.Sophmore;
                }
                else if (NewClassification == "F")
                {
                    classification = StudentClassification.Freshman;
                }
                else
                {
                    classification = StudentClassification.Freshman;
                }
                if (SelectedStudent != null)
                    StudentService.Current.UpdateStudent(SelectedStudent.Id, SelectedStudent.Name, classification);
            });

            DeleteCourseCommand = new Command(() =>
            {
                if (SelectedCourse != null)
                    CourseService.Current.DeleteCourse(SelectedCourse.Code);
            });

            DeleteStudentCommand = new Command(() =>
            {
                if (SelectedStudent != null)
                    StudentService.Current.DeleteStudent(SelectedStudent.Id);
            });
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
