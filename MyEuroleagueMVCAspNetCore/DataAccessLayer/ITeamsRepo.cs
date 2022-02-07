using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.DataAccessLayer
{
    public interface ITeamsRepo
    {
        String GetImageTeamName(string teamName);
    }
}
