namespace MediaHelpers.CoreLibrary.Video.Helpers;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterMockRerunRemoteControls(this IServiceCollection services)
    {
        services.AddSingleton<IRerunTelevisionRemoteControlHostService, MockRerunTelevisionRemoteControlHostService>();
        return services;
    }
    public static IServiceCollection RegisterTelevisionContainer<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        //this is all the stuff that does not require the video players or displays.
        services.AddSingleton<TelevisionContainerClass<E>>();
        return services;
    }
    public static IServiceCollection RegisterCoreRerunListTelevisionServices<L, E>(this IServiceCollection services)
        where E : class, IEpisodeTable
        where L : class, ITelevisionVideoLoader<E>
    {
        services.RegisterTelevisionContainer<E>()
            .AddSingleton<ITelevisionShellViewModel<E>, TelevisionRerunsShellViewModel<E>>()
            .AddSingleton<ITelevisionShellLogic<E>, TelevisionCompleteShellLogic<E>>()
            .RegisterCoreHolidayTelevisionServices<E>()
            .AddSingleton<TelevisionListViewModel<E>>()
            .RegisterNextReRunLogic<E>()
            .AddSingleton<ITelevisionListLogic, TelevisionListRerunLogic<E>>()
            .AddSingleton<ITelevisionVideoLoader<E>, L>();
        return services;
    }
    public static IServiceCollection RegisterCoreLocalRerunLoaderTelevisionServices<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<RerunLocalTelevisionLoaderViewModel<E>>()
            .RegisterTelevisionContainer<E>()
            .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
            .AddSingleton<ITelevisionLoaderViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
            .AddSingleton<IStartLoadingViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel<E>>())
            .RegisterNextReRunLogic<E>()
            .RegisterRerunLoaderLogic<E>()
            .RegisterCoreHolidayTelevisionServices<E>();
        return services;
    }
    public static IServiceCollection RegisterRerunLoaderLogic<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<TelevisionRerunsLoaderLogic<E>>()
            .AddSingleton<IRerunTelevisionLoaderLogic<E>>(pp => pp.GetRequiredService<TelevisionRerunsLoaderLogic<E>>());
        return services;
    }
    //even these needs to be public so youtube can access them.
    

    public static IServiceCollection RegisterNextReRunLogic<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<INextEpisodeLogic<E>, TelevisionBasicRerunNextEpisodeLogic<E>>();
        return services;
    }
    public static IServiceCollection RegisterCoreHolidayTelevisionServices<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<TelevisionHolidayLogic<E>>()
            .AddSingleton<ITelevisionHolidayLogic<E>>(pp => pp.GetRequiredService<TelevisionHolidayLogic<E>>())
            .AddSingleton<TelevisionHolidayViewModel<E>>();
        return services;
    }
}