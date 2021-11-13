namespace MediaHelpers.CoreLibrary.Music.BasicRandomizer;
public interface IMusicShuffleProcesses
{
    Task AddSectionAsync(BasicSection thisSection);
    void Clear();
    void Dispose();
    Task<BasicList<IBaseSong>> GetRandomListAsync();
    BasicList<IBaseSong> SongsChosenForPastSection();
    int SongsSoFar();
}