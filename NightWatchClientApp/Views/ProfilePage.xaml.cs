namespace NightWatchClientApp.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<ProfileViewModel>();
    }
}