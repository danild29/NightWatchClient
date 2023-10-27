

namespace NightWatchClientApp.Views;

public partial class AllEventsPage : ContentPage
{
	public AllEventsPage()
	{
		InitializeComponent();

        
        BindingContext = ServiceHelper.GetService<AllEventsViewModel>();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ServiceHelper.GetService<AllEventsViewModel>().TextChangedCommand(e.NewTextValue);
    }
}