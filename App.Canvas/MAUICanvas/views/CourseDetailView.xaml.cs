using Library.LearningManagement.Models;
using MAUICanvas.ViewModels;

namespace MAUICanvas.views;

public partial class CourseDetailView : ContentPage
{
	public CourseDetailView()
	{
		InitializeComponent();
        BindingContext = new CourseDetailViewModel();
    }
	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//InstructorView");
	}
    //private void EnrollButtonClicked(object sender, EventArgs e)
    //{
        
    //    // when a student is selected from the 'NotEnrolledStudentsList' ListView.
    //    var selectedStudent = (this.BindingContext as CourseDetailViewModel)?.SelectedStudent;

    //    // Assuming 'CourseCode' is a property of your ViewModel that holds the current course's code.
    //    // This could also be retrieved from a UI element like a Label if necessary.
    //    var courseCode = (this.BindingContext as CourseDetailViewModel)?.CourseCode;

    //    if (selectedStudent != null && !string.IsNullOrWhiteSpace(courseCode))
    //    {
    //        // Pass the student's ID and course code to the EnrollStudent method
    //        (this.BindingContext as CourseDetailViewModel)?.EnrollStudent(courseCode, selectedStudent.Id);

    //        Console.WriteLine("Student Added to Course!");
    //        // Optionally, refresh lists or perform other UI updates as needed
    //    }
    //    else
    //    {
    //        // Handle case where no student is selected or course is not identified
    //        // This might involve displaying an error message to the user
    //    }
    //}
    //New additions. Delete if it causes major error:
    //********************************************
    ////********************************************
    //
    //


    private void OkClicked(object sender, EventArgs e)
	{
		(BindingContext as CourseDetailViewModel).AddCourse(Shell.Current);
	}

    private void AddModuleClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        if (viewModel == null) return;

        viewModel.AddModuleToSelectedCourse();
        DisplayAlert("Module Added", "Module successfully added to the course", "OK");
    }

    private void AddContentToModuleClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        if (viewModel != null && viewModel.SelectedCourse != null)
        {
            viewModel.AddContentToSelectedModule();
            DisplayAlert("Success", "Assignment added successfully to the course.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Please select a course and fill all fields correctly.", "OK");
        }

    }

    private void AddAssignmentToCourseClicked(object sender, EventArgs e)
    {
        var viewM = BindingContext as CourseDetailViewModel;
        if(viewM == null)
        {
            DisplayAlert("Error", "Please select a module and fill all fields correctly.", "OK");
            return;
        }
        viewM.AddAssignmentToSelectedCourse();
        DisplayAlert("Success", "The assignment was added to the module.", "OK");


    }

    private void GradeAssignmentClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as CourseDetailViewModel;
        if (vm != null)
        {
            vm.GradeAssignmentCommand.Execute(null);
        }
    }

}