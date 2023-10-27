namespace NightWatchClientApp.Views;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
        BindingContext = ServiceHelper.GetService<PlayViewModel>();
        InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}