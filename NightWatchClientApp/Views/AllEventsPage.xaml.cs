namespace NightWatchClientApp.Views;

public partial class AllEventsPage : ContentPage
{
	public AllEventsPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<AllEventsViewModel>();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ServiceHelper.GetService<AllEventsViewModel>().TextChangedCommand(e.NewTextValue);
    }
}