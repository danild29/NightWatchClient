namespace NightWatchClientApp.Views;

public partial class MyTeamPage : ContentPage
{
	public MyTeamPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<MyTeamViewModel>();
    }
}