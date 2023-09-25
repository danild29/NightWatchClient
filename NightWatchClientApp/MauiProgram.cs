﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NightWatchClientApp.Data.Services;
using NightWatchClientApp.ViewModels;

namespace NightWatchClientApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.RegisterData()
			.RegisterViewModels();




#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}


	public static MauiAppBuilder RegisterData(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<IUserData, UserData>();
		mauiAppBuilder.Services.AddSingleton<ITeamData, TeamData>();
		mauiAppBuilder.Services.AddSingleton<IEventData, EventData>();

		return mauiAppBuilder;
	}
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddSingleton<CreateAccountViewModel>();
        mauiAppBuilder.Services.AddSingleton<ProfileViewModel>();
        mauiAppBuilder.Services.AddSingleton<MyTeamViewModel>();
        mauiAppBuilder.Services.AddSingleton<AllEventsViewModel>();
        mauiAppBuilder.Services.AddSingleton<EventDetailsViewModel>();
        mauiAppBuilder.Services.AddSingleton<CreateEventViewModel>();
        mauiAppBuilder.Services.AddSingleton<ManageEventViewModel>();

        return mauiAppBuilder;
    }
}
