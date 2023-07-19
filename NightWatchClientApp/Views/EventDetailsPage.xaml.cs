namespace NightWatchClientApp.Views;

public partial class EventDetailsPage : ContentPage
{
	public EventDetailsPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<EventDetailsViewModel>();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}