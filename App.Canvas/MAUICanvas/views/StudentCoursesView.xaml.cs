using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using MAUICanvas.ViewModels;
using Library.LearningManagement.DTO;


namespace MAUICanvas.views;

[QueryProperty(nameof(StudentId), "studentId")]

public partial class StudentCoursesView : ContentPage
{
    public ObservableCollection<CoursesDTO> StudentCourses { get; set; } = new ObservableCollection<CoursesDTO>();

    public StudentCoursesView()
    {
        InitializeComponent();
        BindingContext = new StudentViewViewModel();
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        base.OnAppearing();
        (BindingContext as StudentViewViewModel).LoadStudentData(_studentId);
    }
    private int _studentId;

    // This property is meant to receive the navigation parameter value.
    public int StudentId
    {
        set
        {
            _studentId = value;
            (BindingContext as StudentViewViewModel).LoadStudentData(_studentId); // Call a method to load data based on the new _studentId
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
        Shell.Current.GoToAsync("//StudentView");
    }
    private async void PickFileAndSubmit(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync();
        if (result != null)
        {
            var viewModel = BindingContext as StudentViewViewModel;
            if (viewModel != null)
            {
                // Assuming there is a property SelectedContentItem in your ViewModel
                viewModel.SelectedContentItem.Path = result.FullPath;
                bool submissionSuccess = viewModel.SubmitAssignment();

                if (submissionSuccess)
                {
                    await DisplayAlert("Success", "Assignment submitted successfully!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to submit the assignment. Please check all fields.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "No file selected.", "OK");
        }
    }
    private void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            (BindingContext as StudentViewViewModel).SelectedCourse = e.SelectedItem as CoursesDTO;
        }
    }


}