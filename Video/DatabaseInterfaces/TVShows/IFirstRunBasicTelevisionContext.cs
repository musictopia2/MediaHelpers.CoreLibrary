using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHelpers.CoreLibrary.Video.DatabaseInterfaces.TVShows;
public interface IFirstRunBasicTelevisionContext : IStartBasicTelevisionContext
{
    Task FinishVideoFirstRunAsync();
    Task FinishVideoFirstRunAsync(int showID);
}