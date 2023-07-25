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

		if(DevMain.IsFastLogin)
		{
			ServiceHelper.GetService<LoginViewModel>().GetDataFromPrefernces().Await();
		}

		BindingContext = ServiceHelper.GetService<LoginViewModel>();


    }

}