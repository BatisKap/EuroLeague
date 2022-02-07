using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public class MatchesRepo : IMatchesRepo
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public MatchesRepo(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Matches> GetAppMatches()
        {
            return _context.Match.ToList();
        }

        public Matches GetMatchesByRoundId(int RoundId)
        {
            return _context.Match.FirstOrDefault(p => p.RoundNo == RoundId);
        }
        public IEnumerable<Matches> GetMatchesBeUntilRoundId(int RoundId)
        {
            return _context.Match.Where(e => e.RoundNo <= RoundId);
        }
    }
}
