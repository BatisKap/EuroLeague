using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public class TeamsRepo : ITeamsRepo
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public TeamsRepo(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }

        public string GetImageTeamName(string teamName)
        {
            return _context.Team.Where(x => x.Name == teamName).First().TeamLogoImageName;
        }
    }
}
