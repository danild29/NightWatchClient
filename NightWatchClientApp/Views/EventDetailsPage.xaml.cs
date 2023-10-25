namespace NightWatchClientApp.Views;

public partial class EventDetailsPage : ContentPage
{
	public EventDetailsPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<EventDetailsViewModel>();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}