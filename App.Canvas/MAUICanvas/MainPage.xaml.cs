using MAUICanvas.ViewModels;

namespace MAUICanvas
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void StudentClicked(object sender, EventArgs e)
        {
            //Console.WriteLine("Student View button clicked");
            Shell.Current.GoToAsync("//StudentView");
        }
        private void InstructorClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InstructorView");
        }
/*
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/
    }

}
