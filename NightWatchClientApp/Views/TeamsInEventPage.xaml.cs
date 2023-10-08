namespace NightWatchClientApp.Views;

public partial class TeamsInEventPage : ContentPage
{
	public TeamsInEventPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<TeamsInEventViewModel>();
    }
}