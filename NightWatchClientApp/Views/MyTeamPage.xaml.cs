namespace NightWatchClientApp.Views;

public partial class MyTeamPage : ContentPage
{
	public MyTeamPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<MyTeamViewModel>();
    }
}