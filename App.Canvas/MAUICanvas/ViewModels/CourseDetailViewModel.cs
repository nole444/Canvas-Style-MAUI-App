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
using System.Reflection;
using Module = Library.LearningManagement.Models.Module;
using Library.LearningManagement.Utilities;
using Library.LearningManagement.DTO;
//using static Android.Graphics.ImageDecoder;
//using Android.Telecom;
//using static Android.Provider.Contacts;
//using static Android.App.DownloadManager;

namespace MAUICanvas.ViewModels
{
    class CourseDetailViewModel: INotifyPropertyChanged
    {

        private CoursesDTO _model;
        public CoursesDTO Model
        {
            get => _model;
            set
            {
                _model = value;
                NotifyPropertyChanged();
            }
        }
        public CourseDetailViewModel() {
            //course = new Course();
            Model = new CoursesDTO();
            //  Model = courses;
            // Modules = new ObservableCollection<Module>(Modules);
            // AddAssignmentCommand = new Command(AddAssignment);
            // GradeAssignmentCommand = new Command(GradeAssignment);


        }

    

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(Courses));
            }

        }
        //public ObservableCollection<Person> People
        //{
        //    get
        //    {
        //        //This will grab all of the students that match the query given by the instructor regardless of capitilzation
        //        var filteredList = StudentService.Current.Students.Where(s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
        //        return new ObservableCollection<Person>(filteredList);
        //        //return new ObservableCollection<Person>(StudentService.Current.Students);
        //    }
        //}

        public ObservableCollection<CoursesDTO> Courses
        {
            get
            {
                return new ObservableCollection<CoursesDTO>(CourseService.Current.Courses);
            }
        }
        public ObservableCollection<Module> Modules { get; set; } = new ObservableCollection<Module>();

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set { if (Model != null) Model.Name = value;}
        }


        public string Description
        {
            get => Model?.Description ?? string.Empty;
            set { if (Model != null) Model.Description = value; }
        }
        
        
        public int Id { get; set; }

        public string CourseCode
        {
            get => Model?.Code ?? string.Empty;
            set { if (Model != null) Model.Code = value; }

        }
        private CoursesDTO course;

        public void AddCourse(Shell s)
        {
            CourseService.Current.AddOrUpdateCourse(new CoursesDTO { Name= Name, Code = CourseCode, Description = Description});
            //Didnt have this initially and my cource code picker list would not show any courses I added. Needed to update the UI when creating a course
            RefreshStudentsAndCourses();
            s.GoToAsync("//InstructorView");
            
        }
       // public ObservableCollection<Student> EnrolledStudents { get; set; } = new ObservableCollection<Student>();
        public Student SelectedStudent { get; set; }
        public Course SelectedCourse { get; set; }
       
        

        private Module _selectedModule;
        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    NotifyPropertyChanged(nameof(SelectedModule));
                }
            }
        }

        private string _contentName;
        public string ContentName
        {
            get => _contentName;

            set
            {
                if (_contentName != value)
                {
                    _contentName = value;
                    NotifyPropertyChanged(nameof(ContentName));
                }
            }
        }
        private string _contentDescrip;
        public string ContentDescrip
        {
            get => _contentDescrip;

            set
            {
                if (_contentDescrip != value)
                {
                    _contentDescrip = value;
                    NotifyPropertyChanged(nameof(ContentDescrip));
                }
            }
        }

        private string _contentPath;
        public string ContentPath
        {
            get => _contentPath;

            set
            {
                if (_contentPath != value)
                {
                    _contentPath = value;
                    NotifyPropertyChanged(nameof(ContentPath));
                }
            }
        }

       


        // Add a method to handle the actual enrollment action from the UI
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       // public void EnrollStudent(string courseCode, int studentId)
       // {
          //  var resultMessage = CourseService.Current.AddStudentToCourse(courseCode, studentId);
            // Here, you could update the UI to show the resultMessage, for example, using a dialog or notification.
     //  }
        public void RefreshStudentsAndCourses()
        {
            // Refresh the list of students and courses.
            // This could involve re-calling LoadStudentsForCourse for the selected course,
            // or updating People and Courses collections based on the new data.
            //NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }

        //New additions!!!! Delete if major error occurs
        //********************************************
        //********************************************
        //********************************************
        //********************************************
        public ICommand AddModuleCommand { get; private set; }
        public ICommand AddAssignmentCommand { get; private set; }
        public ICommand GradeAssignmentCommand { get; private set; }
        //  public ObservableCollection<Module> Modules { get; set; }


       

    

        //Add back if it breaks. Im trying to get api to work
        public void AddModuleToSelectedCourse()
         {

                  if (SelectedCourse != null && !string.IsNullOrEmpty(ModuleName) && !string.IsNullOrEmpty(ModuleDescription))
                  {
                      CourseService.Current.AddModuleToCourse(SelectedCourse.Code, ModuleName, ModuleDescription);
                      ModuleName = string.Empty; // Reset the module name
                      ModuleDescription = string.Empty; // Reset the module description
                 LoadModules();
                 NotifyPropertyChanged(nameof(Modules));
                  }
             }
        /*
         private void GradeAssignment()
         {
             // Implementation for grading an assignment
             Console.WriteLine("Enter assignment ID:");
             int id = int.Parse(Console.ReadLine());
             var assignment = course.Assignments.FirstOrDefault(a => a.Id == id);
             if (assignment != null)
             {
                 Console.WriteLine("Enter grade:");
                 decimal grade = decimal.Parse(Console.ReadLine());
                 assignment.Grade = grade;
                 NotifyPropertyChanged(nameof(Assignments));
             }
         }
         */

        public string AssignmentName { get; set; }  // For binding the assignment name from the UI
        public string AssignmentDescription { get; set; }  // For binding the description from the UI
        public int AssignmentPoints { get; set; }  // For binding the point value from the UI

        public int AssignmentId { get; set; }   
        public DateTime AssignmentDueDate { get; set; }

        public string GroupName {  get; set; }


        //Same here add back if breaks MAUI

        
        public void AddAssignmentToSelectedCourse()
        {
            if (SelectedCourse != null && !string.IsNullOrWhiteSpace(AssignmentName))
            {
                Assignment newAssignment = new Assignment
                {
                  
                    Name = AssignmentName,
                    Description = AssignmentDescription,
                    TotalPointsAvailable = AssignmentPoints,
                    DueDate = AssignmentDueDate
                };

                if (SelectedCourse.AssignmentGroups.Any())
                {
                    // Assuming there's a default group for simplification
                    SelectedCourse.AssignmentGroups.First().Assignments.Add(newAssignment);
                }
                else
                {
                    // Create a new group if none exists
                    AssignmentGroup newGroup = new AssignmentGroup { Name = "General" };
                    newGroup.Assignments.Add(newAssignment);
                    SelectedCourse.AssignmentGroups.Add(newGroup);
                }

                // Clear the fields after adding
                AssignmentName = string.Empty;
                AssignmentDescription = string.Empty;
                AssignmentPoints = 0;
                AssignmentDueDate = DateTime.Now;


                NotifyPropertyChanged(nameof(SelectedCourse));  // To update UI if needed
            }
        }

      


        private string moduleName;
        public string ModuleName
        {
            get => moduleName;
            set
            {
                moduleName = value;
                NotifyPropertyChanged(nameof(ModuleName));
            }
        }

        private string moduleDescription;
        public string ModuleDescription
        {
            get => moduleDescription;
            set
            {
                moduleDescription = value;
                NotifyPropertyChanged(nameof(ModuleDescription));
            }
        }
        private void AddAssignment()
        {
            if (SelectedCourse == null || string.IsNullOrEmpty(ModuleName) || string.IsNullOrEmpty(AssignmentName) || string.IsNullOrEmpty(AssignmentDescription))
                return;

            CourseService.Current.AddAssignmentToCourse(SelectedCourse.Code,GroupName, AssignmentName, AssignmentDescription, AssignmentPoints, AssignmentDueDate);
            NotifyPropertyChanged("Assignments");
        }

      /*  public void AddAssignmentToSelectedModule()
        {
            if (SelectedCourse != null && !string.IsNullOrWhiteSpace(AssignmentName))
            {
                bool added = CourseService.Current.AddAssignmentToModule(SelectedCourse.Code, ModuleName, AssignmentName, AssignmentDescription, AssignmentPoints, DateTime.Now);
                if (added)
                {
                    AssignmentName = string.Empty;
                    AssignmentDescription = string.Empty;
                    AssignmentPoints = 0;
                    NotifyPropertyChanged("Modules");  // Assuming you also expose Modules in the ViewModel
                }
            }
        }*/
        
        private void LoadModules()
        {
            Modules.Clear();
            if (SelectedCourse != null)
            {
                foreach (var module in SelectedCourse.Modules)
                {
                    Modules.Add(module);
                }
            }
        }

        public void AddContentToSelectedModule()
        {

            /* if (SelectedModule != null && !string.IsNullOrEmpty(AssignmentName))
             {
                 Assignment newAssignment = new Assignment
                 {
                     Name = AssignmentName,
                     Description = AssignmentDescription,
                     TotalPointsAvailable = AssignmentPoints
                 };

                 AssignmentItem newAssignmentItem = new AssignmentItem
                 {
                     Assignment = newAssignment
                 };

                 SelectedModule.Content.Add(newAssignmentItem);
                 NotifyPropertyChanged(nameof(Modules));  // Refresh the modules list if necessary
             }*/
            if (SelectedModule != null )
            {
                // Call the service method to add the assignment to the module
                bool result = CourseService.Current.AddContentToModule(SelectedCourse.Code, SelectedModule.Name, ContentName, ContentDescrip, ContentPath);

                if (result)
                {
                    // Notify that the Modules list may need refreshing in the UI
                    NotifyPropertyChanged(nameof(Modules));

                    // Optional: Reset fields after successful operation
                    AssignmentName = string.Empty;
                    AssignmentDescription = string.Empty;
                    AssignmentPoints = 0;
                    AssignmentDueDate = DateTime.Now;  // Reset to current date or a default value

                    
                }
                
            }

        }
        public void SaveCourse()
        { 
           CourseService.Current.AddOrUpdateCourse(Model);
           
        }










    }
}
