namespace MediaHelpers.CoreLibrary.Music.Components.Jukebox;
public partial class JukeboxShellComponent
{
    private bool _updating;
    private async Task UpdateJukebox()
    {
        _updating = true;
        await InvokeAsync(StateHasChanged);
        await JukeboxViewModel.UpdateJukebox.InvokeAsync();
        _updating = false;
    }
}