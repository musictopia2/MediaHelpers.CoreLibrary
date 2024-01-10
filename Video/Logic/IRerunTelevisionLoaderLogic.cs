using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHelpers.CoreLibrary.Video.Logic;
public interface IRerunTelevisionLoaderLogic : IBasicTelevisionLoaderLogic
{
    Task TemporarilySKipEpisodeAsync(IEpisodeTable episode);
    Task<bool> CanGoToNextEpisodeAsync();
    //this means the firstrun can have other functions for that remote control.
}