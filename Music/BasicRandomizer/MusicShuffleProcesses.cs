namespace MediaHelpers.CoreLibrary.Music.BasicRandomizer;
internal static class Globals
{
    internal static BasicList<int> SongsChosen { get; set; } = new();
}
public class MusicShuffleProcesses : IDisposable, IMusicShuffleProcesses
{
    private readonly BasicList<IBaseSong> _rList = new();
    private BasicList<IBaseSong> _pList = new();
    private bool _alreadyHad;
    private bool _disposedValue;
    public void Clear()
    {
        SongsChosen.Clear();
        _pList.Clear();
        _rList.Clear();
    }
    public int SongsSoFar()
    {
        return _rList.Count;
    }
    public MusicShuffleProcesses()
    {
        SongsChosen.Clear();
    }
    public BasicList<IBaseSong> SongsChosenForPastSection()
    {
        return _pList;
    }
    public async Task AddSectionAsync(BasicSection thisSection)
    {
        await Task.Run(() =>
        {
            _alreadyHad = true;
            var firstList = thisSection.SongList!.ToBasicList();
            if (firstList.Count == 0)
            {
                throw new CustomBasicException("When getting custom list, can't have 0 songs");
            }
            _pList.Clear();
            var howManySongs = PrivateHowManySongs(thisSection, firstList.Count);
            if (howManySongs == 0)
            {
                throw new CustomBasicException("PrivateHowManySongs Can't Return 0 Songs");
            }
            firstList.ShuffleList(howManySongs);
            if (firstList.Count == 0)
            {
                throw new CustomBasicException("Can't have 0 songs after shuffling.  Rethink");
            }
            firstList.ForEach(items =>
            {
                SongsChosen.Add(items.ID);
            });
            _pList = firstList;
            _rList.AddRange(firstList);
        });
    }
    private static int PrivateHowManySongs(BasicSection thisS, int songCount)
    {
        int temps = songCount.MultiplyPercentage(thisS.Percent);
        if (thisS.HowManySongs == 0)
        {
            thisS.ChooseAll = true;
        }
        if (thisS.ChooseAll == true)
        {
            if (thisS.Percent == 0)
            {
                return songCount;
            }
            return temps;
        }
        if (thisS.HowManySongs > temps)
        {
            return temps;
        }
        return thisS.HowManySongs;
    }
    public async Task<BasicList<IBaseSong>> GetRandomListAsync()
    {
        BasicList<IBaseSong>? tempList = null;
        await Task.Run(() =>
        {
            SongsChosen.Clear();
            if (_alreadyHad == true)
            {
                _alreadyHad = false;
                tempList = _rList.ToBasicList();
                return;
            }
            tempList = _rList.GetRandomList().ToCastedList<IBaseSong>();
            return;
        });
        return tempList!;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {

            }
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}