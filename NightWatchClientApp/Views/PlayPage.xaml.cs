namespace NightWatchClientApp.Views;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
        BindingContext = ServiceHelper.GetService<PlayViewModel>();
        InitializeComponent();
	}


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}