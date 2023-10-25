namespace NightWatchClientApp.Views;

public partial class CreateEventPage : ContentPage
{
	public CreateEventPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<CreateEventViewModel>();
    }
}