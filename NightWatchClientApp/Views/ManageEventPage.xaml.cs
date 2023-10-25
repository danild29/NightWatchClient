namespace NightWatchClientApp.ViewModels;


public partial class ManageEventPage : ContentPage
{
    public ManageEventPage()
	{
        InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<ManageEventViewModel>();

	
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
    
