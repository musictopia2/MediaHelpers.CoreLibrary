namespace MediaHelpers.CoreLibrary.Video.Components;
public partial class FirstVideoProgressComponent
{
    [Inject]
    private FirstContainerClass? Container { get; set; }
    protected override void OnInitialized()
    {
        RunTask();
    }
    private async void RunTask()
    {
        do
        {
            await Task.Delay(1000);
            Container!.DateProgress = DateTime.Now;
            await InvokeAsync(StateHasChanged);
        } while (true);
    }
}