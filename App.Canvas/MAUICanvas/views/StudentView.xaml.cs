using Library.LearningManagement.Models;
using MAUICanvas.ViewModels;
using Library.LearningManagement.DTO;

namespace MAUICanvas.views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
		BindingContext = new StudentViewViewModel();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedStudent = e.SelectedItem as StudentDTO; // Ensure this matches your model
        if (selectedStudent != null)
        {
            // Assuming NavigateToStudentCourses is a public method in your ViewModel that can be called here
            NavigateToStudentCourses(selectedStudent);
        }

        // Optionally, deselect the item
        ((ListView)sender).SelectedItem = null;
    }
    public void NavigateToStudentCourses(StudentDTO student)
    {
        if (student != null)
        {
            // Use Shell navigation to go to the student courses page, passing the student ID as a parameter
           Shell.Current.GoToAsync($"//StudentCoursesView?studentId={student.Id}");
            
        }
    }
    // Refresh or update the view when navigated to this page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Assuming RefreshView is a method in your ViewModel to refresh data
        (BindingContext as StudentViewViewModel)?.RefreshView();
    }
}