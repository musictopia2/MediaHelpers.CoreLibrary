namespace MediaHelpers.CoreLibrary.Video.Helpers;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterMockTelevisionRerunRemoteControls(this IServiceCollection services)
    {
        services.AddSingleton<IRerunTelevisionRemoteControlHostService, MockRerunTelevisionRemoteControlHostService>();
        return services;
    }
    public static IServiceCollection RegisterMockMovieRerunRemoteControls(this IServiceCollection services)
    {
        services.AddSingleton<IRerunMoviesRemoveControlHostService, MockRerunMoviesRemoteControlHostService>();
        return services;
    }
    public static IServiceCollection RegisterTelevisionContainer<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        //this is all the stuff that does not require the video players or displays.
        services.AddSingleton<TelevisionContainerClass<E>>();
        return services;
    }
    public static IServiceCollection RegisterCoreRerunListMovieServices<L, M>(this IServiceCollection services)
        where M: class, IMainMovieTable
        where L : class, IMovieVideoLoader<M>
    {
        services.AddSingleton<MovieContainerClass<M>>()
            .AddSingleton<MovieListViewModel<M>>()
            .AddSingleton<IMovieListLogic<M>, MovieRerunsListLogic<M>>()
            .AddSingleton<IMovieVideoLoader<M>, L>();
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
            .RegisterNextReRunTelevisionLogic<E>()
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
            .RegisterNextReRunTelevisionLogic<E>()
            .RegisterRerunTelevisionLoaderLogic<E>()
            .RegisterCoreHolidayTelevisionServices<E>();
        return services;
    }
    public static IServiceCollection RegisterCoreLocalRerunLoaderMovieServices<M>(this IServiceCollection services)
        where M : class, IMainMovieTable
    {
        services.AddSingleton<MovieContainerClass<M>>()
            .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
            .AddSingleton<IMovieLoaderViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
            .AddSingleton<IStartLoadingViewModel>(pp => pp.GetRequiredService<RerunLocalMovieLoaderViewModel<M>>())
            .AddSingleton<MovieRerunsLoaderLogic<M>>()
            .AddSingleton<IRerunMovieLoaderLogic<M>>(pp => pp.GetRequiredService<MovieRerunsLoaderLogic<M>>());
        return services;
    }
    public static IServiceCollection RegisterRerunTelevisionLoaderLogic<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<TelevisionRerunsLoaderLogic<E>>()
            .AddSingleton<IRerunTelevisionLoaderLogic<E>>(pp => pp.GetRequiredService<TelevisionRerunsLoaderLogic<E>>());
        return services;
    }
    //even these needs to be public so youtube can access them.
    

    public static IServiceCollection RegisterNextReRunTelevisionLogic<E>(this IServiceCollection services)
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