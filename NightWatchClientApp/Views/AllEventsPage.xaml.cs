namespace NightWatchClientApp.Views;

public partial class AllEventsPage : ContentPage
{
	public AllEventsPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<AllEventsViewModel>();
    }
}