using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.Views;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage()
	{
		InitializeComponent();

        BindingContext = ServiceHelper.GetService<ResetPasswordViewModel>();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;
    }
}
