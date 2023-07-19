using NightWatchClientApp.Data;
using NightWatchClientApp.Helpers;
using NightWatchClientApp.ViewModels;

namespace NightWatchClientApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<LoginViewModel>();
	}
}