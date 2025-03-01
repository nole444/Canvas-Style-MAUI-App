using MAUICanvas.ViewModels;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
namespace MAUICanvas.views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
    {
		InitializeComponent();
        BindingContext = new InstructorViewViewModel();
        
    }

	//Here is my return to main menu method
	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddEnrollmentClick(object sender, EventArgs e)
	{
		(BindingContext as InstructorViewViewModel).AddEnrollmentClick(Shell.Current);
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as InstructorViewViewModel).RefreshView();
	}
    private void AddCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }
	//Handlers for toolbar 
    private void Toolbar_EnrollmentsClicked(object sender, EventArgs e)
    {
		(BindingContext as InstructorViewViewModel).ShowEnrollments();
    }

	private void Toolbar_CoursesClicked(object sender, EventArgs e)
	{
        (BindingContext as InstructorViewViewModel).ShowCourses();
    }
  
    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as InstructorViewViewModel;
        if (viewModel == null) return;

        
        var selectedStudent = viewModel.SelectedStudent;
        var selectedCourse = viewModel.SelectedCourse;

        if (selectedStudent != null && selectedCourse != null)
        {
           viewModel.AddStudentToCourse(selectedCourse.Code, selectedStudent.Id);

            // Optionally display a confirmation or result message
            DisplayAlert("Student Added!","Course Roster Updated!", "OK");
        }
        else
        {
            DisplayAlert("Selection Error", "Please select both a student and a course.", "OK");
        }
    }

    private void OnStudentChosen(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedStudent = e.SelectedItem as Student; // Ensure this matches your model
        if (selectedStudent != null)
        {
            // Assuming NavigateToStudentCourses is a public method in your ViewModel that can be called here
            NavigateToStudentsCourses(selectedStudent);
        }

     // Optionally, deselect the item
     ((ListView)sender).SelectedItem = null;
    }

    public void NavigateToStudentsCourses(Student student)
    {
        if (student != null)
        {
            // Use Shell navigation to go to the student courses page, passing the student ID as a parameter
            Shell.Current.GoToAsync($"//InstructorGradingView?studentId={student.Id}");
        }
        else {
            DisplayAlert("Error!!", "Student is null", "OK");

        }
    }

    //EditCourseAndStudent
    private async void EditCoursesAndStudentsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//EditCourseAndStudentView");
    }



}