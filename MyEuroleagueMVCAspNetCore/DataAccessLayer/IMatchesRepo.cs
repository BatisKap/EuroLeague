using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public interface IMatchesRepo
    {
        IEnumerable<Matches> GetAppMatches();
        Matches GetMatchesByRoundId(int RoundId);
        IEnumerable<Matches> GetMatchesBeUntilRoundId(int RoundId);
    }
}
