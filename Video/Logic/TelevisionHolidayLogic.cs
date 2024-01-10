namespace MediaHelpers.CoreLibrary.Video.Logic;
public class TelevisionHolidayLogic(IRerunTelevisionContext dats) : ITelevisionHolidayLogic
{
    async Task<BasicList<IEpisodeTable>> ITelevisionHolidayLogic.GetHolidayEpisodeListAsync(EnumTelevisionHoliday currentHoliday)
    {
        return await dats.GetHolidayListAsync(currentHoliday);
    }
}