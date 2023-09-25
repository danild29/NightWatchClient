namespace NightWatchClientApp.ViewModels;


public partial class ManageEventPage : ContentPage
{
    public ManageEventPage()
	{
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<ManageEventViewModel>();

	
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
    
