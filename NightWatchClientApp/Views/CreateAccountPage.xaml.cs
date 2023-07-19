

namespace NightWatchClientApp.Views;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<CreateAccountViewModel>();
    }
}