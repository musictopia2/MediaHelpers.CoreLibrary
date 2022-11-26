namespace MediaHelpers.CoreLibrary.Video.ViewModels;
public abstract class MainVideoListViewModel<V>
    where V : class
{
    public V? SelectedItem { get; set; }
    public BasicList<V> VideoList { get; set; } = new();
    public bool CanChooseVideo => SelectedItem != null;
    public Action? FocusCombo { get; set; }
    public abstract Task InitAsync();
}