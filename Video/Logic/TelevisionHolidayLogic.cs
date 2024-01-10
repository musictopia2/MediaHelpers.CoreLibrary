namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic(ITelevisionContext dats) : ITelevisionHolidayLogic
{
    async Task<BasicList<IEpisodeTable>> ITelevisionHolidayLogic.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        //await Task.CompletedTask;
        
        //return dats.GetHolidayList(currentHoliday);
        return await dats.GetHolidayListAsync(currentHoliday);
    }
}