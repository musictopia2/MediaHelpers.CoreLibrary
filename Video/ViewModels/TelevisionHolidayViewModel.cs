namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionHolidayViewModel(ITelevisionHolidayLogic logic, ISystemError error, IExit exit)
{
    public string NonHolidayText { get; set; } = "";
    public bool HolidayFullVisible { get; set; }
    public string HolidayFullText { get; set; } = "Full Hour";
    public bool HolidayHalfVisible { get; set; }
    public string HolidayHalfText { get; set; } = "Half Hour";
    //internal bool ManuallyChoseHoliday { get; set; }
    public bool IsLoaded { get; private set; }
    //go ahead and remove because the next episode may be the same program (but no guarantees)

    internal void RemoveHolidayEpisode(IEpisodeTable episode)
    {
        var lookup = _holidayList.Single(xx => xx.ID == episode.ID);
        _holidayList.RemoveSpecificItem(lookup); //to guarantee the proper one gets removed no matter what.
    }
    public IEpisodeTable? GetHolidayEpisode(EnumTelevisionLengthType lengthType)
    {
        if (IsLoaded == false)
        {
            exit.ExitApp();
            return null;
        }
        var episodeList = _holidayList.GetConditionalItems(xx => xx.ShowTable.LengthType == lengthType);
        if (episodeList.Count == 0)
        {
            exit.ExitApp(); //just exit period.
            return null;
        }
        //ManuallyChoseHoliday = true;
        IEpisodeTable episode = episodeList.GetRandomItem();
        return episode;
    }
    private BasicList<IEpisodeTable> _holidayList = [];
    public async Task InitAsync(EnumTelevisionHoliday holiday) //has to send in.  so i can mock a holiday if needed to make sure holidays work before they happen.
    {
        IsLoaded = false;
        if (holiday == EnumTelevisionHoliday.None)
        {
            error.ShowSystemError("Should have never shown the holiday view model because no holiday was chosene");
            return;
        }
        //WasHoliday = true;
        try
        {
            _holidayList = await logic.GetHolidayEpisodeListAsync(holiday);
            string p;
            NonHolidayText = $"Choose Shows With Non {holiday} Episodes";
            if (_holidayList.Exists(items => items.ShowTable.LengthType == EnumTelevisionLengthType.FullHour) == false)
            {
                HolidayFullVisible = false;
            }
            else
            {
                p = HolidayFullText;
                HolidayFullText = $"{p} For {holiday}";
                HolidayFullVisible = true;
            }
            if (_holidayList.Exists(items => items.ShowTable.LengthType == EnumTelevisionLengthType.HalfHour) == false)
            {
                HolidayHalfVisible = false;
            }
            else
            {
                p = HolidayHalfText;
                HolidayHalfText = $"{p} For {holiday}";
                HolidayHalfVisible = true;
            }
            if (HolidayFullVisible == false && HolidayHalfVisible == false)
            {
                return; //can't show as loaded.
            }
            IsLoaded = true;
        }
        catch (Exception ex)
        {
            error.ShowSystemError(ex.Message);
            throw;
        }
    }
}