namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic(IRerunListTelevisionContext dats) : ITelevisionHolidayLogic
{
    async Task<BasicList<IEpisodeTable>> ITelevisionHolidayLogic.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        return await dats.GetHolidayListAsync(currentHoliday);
    }
}