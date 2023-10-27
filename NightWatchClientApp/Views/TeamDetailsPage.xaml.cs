namespace NightWatchClientApp.Views;

public partial class TeamDetailsPage : ContentPage
{
	public TeamDetailsPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<TeamDetailsViewModel>();
    }
}