

namespace NightWatchClientApp.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<ProfileViewModel>();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        ServiceHelper.GetService<ProfileViewModel>().Name = UserAppInfo.UserData.Name;
        base.OnNavigatedTo(args);
    }
}