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
        int currentWeight;
        await Task.CompletedTask;
        if (currentHoliday == EnumTelevisionHoliday.Christmas)
        {
            currentWeight = 6;
        }
        else
        {
            currentWeight = 4;
        }
        return _dats.GetHolidayList(currentHoliday, currentWeight); //something else has the try catch statement.
    }
}