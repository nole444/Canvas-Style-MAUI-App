using MAUICanvas.ViewModels;

namespace MAUICanvas.views;

public partial class EditCourseAndStudentView : ContentPage
{
	public EditCourseAndStudentView()
	{
		InitializeComponent();
        BindingContext = new EditCourseAndStudentViewModel();

    }

    private void EditStudentClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EditCourseAndStudentViewModel;
        if (viewModel == null) return;

        // Execute my ICommand for update student
        if (viewModel.UpdateStudentCommand.CanExecute(null))
        {
            viewModel.UpdateStudentCommand.Execute(null);
        }

        //  Display a confirmation message
        DisplayAlert("Update Successful", "Student successfully updated.", "OK");
    }
    private void EditCourseClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EditCourseAndStudentViewModel;
        if (viewModel == null) return;

        // Execute the ICommand
        if (viewModel.UpdateCourseCommand.CanExecute(null))
        {
            viewModel.UpdateCourseCommand.Execute(null);
        }

        // Display a confirmation message
        DisplayAlert("Update Successful", "Course successfully updated.", "OK");
    }

    private async void DeleteStudentClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EditCourseAndStudentViewModel;
        if (viewModel == null) return;

        // Show confirmation dialog before deleting
        //I am using displayAlert method to display a message to the user
        bool isUserSure = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this student?","Yes", "No");

        // Check if the user confirmed the deletion
        if (isUserSure)
        {
            if (viewModel.DeleteStudentCommand.CanExecute(null))
            {
                viewModel.DeleteStudentCommand.Execute(null);
                // Display a confirmation message if the deletion was executed
                await DisplayAlert("Deletion Successful", "Student successfully deleted.", "OK");
            }
        }
        else
        {
            // Optionally handle the case where the user cancels the deletion
            await DisplayAlert("Deletion Cancelled", "Student deletion was cancelled.", "OK");
        }
    }

    private async void DeleteCourseClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as EditCourseAndStudentViewModel;
        if (viewModel == null) return;

        // Show confirmation dialog before deleting
        //I am using displayAlert method to display a message to the user
        bool isUserSure = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this course?", "Yes", "No");

        // Check if the user confirmed the deletion
        if (isUserSure)
        {
            if (viewModel.DeleteCourseCommand.CanExecute(null))
            {
                viewModel.DeleteCourseCommand.Execute(null);
                // Display a confirmation message if the deletion was executed
                await DisplayAlert("Deletion Successful", "Course successfully deleted.", "OK");
            }
        }
        else
        {
            // Optionally handle the case where the user cancels the deletion
            await DisplayAlert("Deletion Cancelled", "Course deletion was cancelled.", "OK");
        }
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InstructorView");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EditCourseAndStudentViewModel();
    }
}
