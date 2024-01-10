namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic : ITelevisionHolidayLogic
{
    private readonly ITelevisionContext _dats;
    public TelevisionHolidayLogic(ITelevisionContext dats)
    {
        _dats = dats;
    }
    async Task<BasicList<IEpisodeTable>> ITelevisionHolidayLogic.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        await Task.CompletedTask;
        
        return _dats.GetHolidayList(currentHoliday);
    }
}