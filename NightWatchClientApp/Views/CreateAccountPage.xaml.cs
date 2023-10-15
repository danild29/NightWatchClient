

namespace NightWatchClientApp.Views;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage()
	{
		InitializeComponent();

        MainBackground.Source = ImageSource.FromResource("NightWatchClientApp.Resources.Background.background.jpg");
        MainBackground.Aspect = Aspect.Fill;

        BindingContext = ServiceHelper.GetService<CreateAccountViewModel>();
    }
}