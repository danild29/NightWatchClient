namespace NightWatchClientApp.Views;

public partial class TeamsInEventPage : ContentPage
{
	public TeamsInEventPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<TeamsInEventViewModel>();
    }
}