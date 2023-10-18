namespace NightWatchClientApp.Views;

public partial class TeamDetailsPage : ContentPage
{
	public TeamDetailsPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<TeamDetailsViewModel>();
    }
}