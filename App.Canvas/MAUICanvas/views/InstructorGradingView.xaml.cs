using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using MAUICanvas.ViewModels;
using Library.LearningManagement.DTO;


namespace MAUICanvas.views;

[QueryProperty(nameof(StudentId), "studentId")]

public partial class InstructorGradingView : ContentPage
{
    public ObservableCollection<Course> StudentCourses { get; set; } = new ObservableCollection<Course>();

    public InstructorGradingView()
    {
        InitializeComponent();
        BindingContext = new InstructorViewViewModel();
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        base.OnAppearing();
        (BindingContext as InstructorViewViewModel).LoadData(_studentId);
    }
    private int _studentId;

    // This property is meant to receive the navigation parameter value.
    public int StudentId
    {
        set
        {
            _studentId = value;
            (BindingContext as InstructorViewViewModel).LoadData(_studentId); // Call a method to load data based on the new _studentId
        }
    }

    /* private void OnSubmitAssignmentClicked(object sender, EventArgs e)
     {
         var viewModel = BindingContext as StudentViewViewModel;
         if (viewModel != null)
         {
             viewModel.SubmitAssignment();
             DisplayAlert("Sucesss!", "Assignment Submited!", "OK");
         }
     }
    */
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InstructorView");
    }
    private void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            (BindingContext as InstructorViewViewModel).SelectedCourse = e.SelectedItem as CoursesDTO;
        }
    }
}