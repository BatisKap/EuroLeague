using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public interface IStandingsRepo
    {
        List<Standings> teamsInStandings();
        Standings populateStanding(Standings teamsInStand, Matches matchItem, bool isHomeTeam);

    }
}
