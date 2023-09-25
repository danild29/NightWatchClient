namespace NightWatchClientApp.Views;

public partial class CreateEventPage : ContentPage
{
	public CreateEventPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<CreateEventViewModel>();
    }
}