using NightWatchClientApp.Data;
using NightWatchClientApp.Helpers;
using NightWatchClientApp.ViewModels;
using System.Reflection;

namespace NightWatchClientApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

		MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
		MainBackground.Aspect = Aspect.Fill;

		if (DevMain.IsFastLogin)
		{
			ServiceHelper.GetService<LoginViewModel>().GetDataFromPrefernces().Await();
		}

		BindingContext = ServiceHelper.GetService<LoginViewModel>();


	}

}