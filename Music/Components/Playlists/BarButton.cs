namespace MediaHelpers.CoreLibrary.Music.Components.Playlists;
public class BarButton : ButtonComponentBase
{
    public override string BackColor => cc1.Aqua.ToWebColor();
    public override string TextColor => cc1.Navy.ToWebColor();
    public override string DisabledColor => cc1.Gray.ToWebColor();
    public override string FontSize => "40px"; //try 40 to start with.
}