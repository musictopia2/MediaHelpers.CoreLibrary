namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic(IRerunStartTelevisionContext dats) : ITelevisionHolidayLogic
{
    //now needs holiday logic
    async Task<BasicList<IEpisodeTable>> ITelevisionHolidayLogic.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        return await dats.GetHolidayListAsync(currentHoliday);
    }
}