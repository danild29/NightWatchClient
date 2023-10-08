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
		Routing.RegisterRoute(nameof(PlayPage), typeof(PlayPage));
		Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));

		Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));
		Routing.RegisterRoute(nameof(ManageEventPage), typeof(ManageEventPage));
		Routing.RegisterRoute(nameof(TeamsInEventPage), typeof(TeamsInEventPage));

        IsVisibleEvents = true;
        BindingContext = this;
	}
}
