using NightWatchClientApp.Views;
using System.Security.Cryptography.X509Certificates;

namespace NightWatchClientApp;

public partial class AppShell : Shell
{

    public bool IsVisibleEvents { get; set; }

    public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));

		Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));

        IsVisibleEvents = true;
        BindingContext = this;
	}
}
