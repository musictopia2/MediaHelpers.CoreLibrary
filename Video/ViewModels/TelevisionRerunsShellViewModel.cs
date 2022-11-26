namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public class TelevisionRerunsShellViewModel : ITelevisionShellViewModel
{
    private readonly ITelevisionShellLogic _logic;
    private readonly IDateOnlyPicker _datePicker;
    public TelevisionRerunsShellViewModel(ITelevisionShellLogic logic, IDateOnlyPicker datePicker)
    {
        _logic = logic;
        _datePicker = datePicker;
    }
    public EnumTelevisionHoliday CurrentHoliday { get; private set; } = EnumTelevisionHoliday.None;
    public IEpisodeTable? PreviousEpisode { get; private set; }
    public bool IsLoaded { get; private set; }
    public bool DidReset { get; private set; }

    public async Task InitAsync()
    {
        PreviousEpisode = await _logic.GetPreviousShowAsync();
        if (PreviousEpisode is null)
        {
            CurrentHoliday = _datePicker.GetCurrentDate.WhichHoliday();
            //if there are no episodes then needs to somehow show the normal list.

        }
        IsLoaded = true;
    }
    //i can reset holiday via programming if needed as well.
    public void ResetHoliday()
    {
        CurrentHoliday = EnumTelevisionHoliday.None;
        DidReset = true; //this means will not be holiday because you chose no holiday.
    }
}