﻿namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionRerunsShellViewModel<E>(ITelevisionShellLogic<E> logic, IDateOnlyPicker datePicker) : ITelevisionShellViewModel<E>
    where E: class, IEpisodeTable
{
    public EnumTelevisionHoliday CurrentHoliday { get; private set; } = EnumTelevisionHoliday.None;
    public E? PreviousEpisode { get; private set; }
    public bool IsLoaded { get; private set; }

    //public bool DidReset { get; private set; }

    public async Task InitAsync()
    {
        PreviousEpisode = await logic.GetPreviousEpisodeAsync();
        if (PreviousEpisode is null)
        {
            CurrentHoliday = datePicker.GetCurrentDate.WhichHoliday();
            //if there are no episodes then needs to somehow show the normal list.

        }
        IsLoaded = true;
    }
    //i can reset holiday via programming if needed as well.
    public void ResetHoliday()
    {
        CurrentHoliday = EnumTelevisionHoliday.None;
        //DidReset = true; //this means will not be holiday because you chose no holiday.
    }
}