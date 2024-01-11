namespace MediaHelpers.CoreLibrary.Video.Helpers;
public static class ServiceExtensions
{
    //this is all the stuff that does not rely on wpf stuff  or custom stuff like databases..
    public static IServiceCollection RegisterMockFirstRunRemoteControls(this IServiceCollection services)
    {
        services.AddSingleton<IFirstRunTelevisionRemoteControlHostService, MockFirstRunTelevisionRemoteControlHostService>();
        return services;
    }
    public static IServiceCollection RegisterMockRerunRemoteControls(this IServiceCollection services)
    {
        services.AddSingleton<IRerunTelevisionRemoteControlHostService, MockRerunTelevisionRemoteControlHostService>();
        return services;
    }
    private static IServiceCollection RegisterTelevisionContainer(this IServiceCollection services)
    {
        //this is all the stuff that does not require the video players or displays.
        services.AddSingleton<TelevisionContainerClass>();
        return services;
    }
    public static IServiceCollection RegisterCoreLocalFirstRunLoaderTelevisionServices(this IServiceCollection services)
    {
        services.AddSingleton<FirstRunLocalTelevisionLoaderViewModel>()
             .RegisterTelevisionContainer()
             .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<FirstRunLocalTelevisionLoaderViewModel>())
             .AddSingleton<ITelevisionLoaderViewModel>(pp => pp.GetRequiredService<FirstRunLocalTelevisionLoaderViewModel>())
             .RegisterNextFirstRunLogic()
             .AddSingleton<IFirstRunTelevisionLoaderLogic, TelevisionFirstrunLoaderLogic>();
        return services;
    }
    public static IServiceCollection RegisterCoreLocalRerunLoaderTelevisionServices(this IServiceCollection services)
    {
        services.AddSingleton<RerunLocalTelevisionLoaderViewModel>()
            .RegisterTelevisionContainer()
            .AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel>())
            .AddSingleton<ITelevisionLoaderViewModel>(pp => pp.GetRequiredService<RerunLocalTelevisionLoaderViewModel>())
            .RegisterNextReRunLogic()
            .AddSingleton<IRerunTelevisionLoaderLogic, TelevisionRerunsLoaderLogic>()
            .RegisterCoreHolidayTelevisionServices();
        return services;
    }
    private static IServiceCollection RegisterNextFirstRunLogic(this IServiceCollection services)
    {
        services.AddSingleton<INextEpisodeLogic, TelevisionBasicFirstRunNextEpisodeLogic>();
        return services;
    }
    private static IServiceCollection RegisterNextReRunLogic(this IServiceCollection services)
    {
        services.AddSingleton<INextEpisodeLogic, TelevisionBasicRerunNextEpisodeLogic>();
        return services;
    }
    public static IServiceCollection RegisterCoreHolidayTelevisionServices(this IServiceCollection services)
    {
        services.AddSingleton<ITelevisionHolidayLogic, TelevisionHolidayLogic>()
            .AddSingleton<TelevisionHolidayViewModel>();
        return services;
    }
}