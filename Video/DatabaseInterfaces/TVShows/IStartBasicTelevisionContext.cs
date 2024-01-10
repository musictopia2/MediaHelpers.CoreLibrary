using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IStartBasicTelevisionContext
{
    IEpisodeTable CurrentEpisode { get; set; } //this is needed so if there is a previous show, can set it to use later.
    Task<IEpisodeTable?> GetNextEpisodeAsync(int showID);
}