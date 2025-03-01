using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using MAUICanvas.ViewModels;
namespace MAUICanvas.views;

public partial class StudentDetailView : ContentPage
{
	public StudentDetailView()
	{
        InitializeComponent();

		BindingContext = new StudentDetailViewModel();

	}
	public void OnLeaving (object sender, NavigatedFromEventArgs e) 
	{
		BindingContext = null;
	}
	private void OnArriving (object sender, NavigatedToEventArgs e)
	{
		BindingContext = new StudentDetailViewModel();
	}

	private void OkClick(object sender, EventArgs e)
	{ 
		(BindingContext as StudentDetailViewModel).AddStudent();
		/*var context = BindingContext as StudentDetailViewModel;

		StudentClassification classification;
		if(context.ClassificationString == "S")
		{
			classification = StudentClassification.Senior;
		}
        else if(context.ClassificationString == "J")
        {
             classification= StudentClassification.Junior;
        }
		else if(context.ClassificationString == "O")
		{
			classification = StudentClassification.Sophmore;
		}
		else if(context.ClassificationString == "F")
		{
			classification = StudentClassification.Freshman;
		}
		else
		{
			classification = StudentClassification.Freshman;
		}

		StudentService.Current.Add(new Student { Name = context.Name, Classification = classification });
		Shell.Current.GoToAsync("//InstructorView");
		*/
    }
}