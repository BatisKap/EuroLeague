using Microsoft.AspNetCore.Mvc;
using MyEuroleagueMVCAspNetCore.DataAccessLayer;
using MyEuroleagueMVCAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEuroleagueMVCAspNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StandingsAPIController : Controller
    {
        private readonly IStandingsRepo _repository;
        private readonly IMatchesRepo _MatchesRepository;
        private readonly ITeamsRepo _TeamsRepository;
        public StandingsAPIController(IStandingsRepo repository, IMatchesRepo MatchesRepository, ITeamsRepo TeamsRepo)
        {
            _repository = repository;
            _MatchesRepository = MatchesRepository;
            _TeamsRepository = TeamsRepo;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Match.ToListAsync());
        //}


        // GET: StandingsAPI
 
        public ActionResult<IEnumerable<Standings>> GetOverallStandings()
        {
           
            List<Standings> standings = new List<Standings>();
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_repository.teamsInStandings());            
            var allMatches = _MatchesRepository.GetAppMatches();
            return View(populateStandings(standingsDynamically, allMatches));                      
        }

        [HttpGet]
        public ActionResult<IEnumerable<Standings>> GetOverallStandings(int searchByStandingRound)
        {
            if (searchByStandingRound>0)
            {
                ViewData["getStandings"] = searchByStandingRound;
            } else {
                ViewData["getStandings"] = "";
            }
           
            List<Standings> standings = new List<Standings>();
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_repository.teamsInStandings());
            if (searchByStandingRound>0) {

                var allMatchesUntilRoundID = _MatchesRepository.GetMatchesBeUntilRoundId(searchByStandingRound);
                return View(populateStandings(standingsDynamically, allMatchesUntilRoundID));
            } else {
                var allMatches = _MatchesRepository.GetAppMatches();
                return View(populateStandings(standingsDynamically, allMatches));
            }
        }
        // GET: StandingsAPI/5
        [HttpGet("{RoundId}")]
        public ActionResult<IEnumerable<Standings>> GetStandingsByRound(int RoundId)
        {
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_repository.teamsInStandings());
            var allMatchUntilRoundID = _MatchesRepository.GetMatchesBeUntilRoundId(RoundId);
            return View(populateStandings(standingsDynamically, allMatchUntilRoundID));
        }

        List<Standings> populateStandings(List<Standings> standingsDynamically,IEnumerable<Matches> allMatches) {
            List<Standings> standings = new List<Standings>();
            foreach (var matchItem in allMatches)
            {
                foreach (var teamsInStand in standingsDynamically)
                {
                    if (teamsInStand.TeamName.Trim() == matchItem.Home_Team.Trim())
                    {
                        var TeamNameExist = standings.Find(x => x.TeamName.Contains(teamsInStand.TeamName.Trim()));
                        Standings populateStandingsHome = _repository.populateStanding((TeamNameExist != null) ? TeamNameExist : teamsInStand, matchItem, true);
                        if (TeamNameExist != null)
                        {
                            var itemToRemove = standings.Single(r => r.TeamName == teamsInStand.TeamName.Trim());
                            standings.Remove(itemToRemove);
                        }
                        standings.Add(populateStandingsHome);
                    }
                    if (teamsInStand.TeamName.Trim() == matchItem.Away_Team.Trim())
                    {
                        var AwayTeamNameExist = standings.Find(x => x.TeamName.Contains(teamsInStand.TeamName.Trim()));
                        Standings populateStandingsAway = _repository.populateStanding((AwayTeamNameExist != null) ? AwayTeamNameExist : teamsInStand, matchItem, false);

                        if (AwayTeamNameExist != null)
                        {
                            var awayItemToRemove = standings.Single(r => r.TeamName == teamsInStand.TeamName.Trim());
                            standings.Remove(awayItemToRemove);
                        }
                        standings.Add(populateStandingsAway);
                    }
                    continue;
                }

            }
            int positionCnt = 0;
            var results = standings.OrderByDescending(p => p.PointsDif).OrderBy(m => m.MatchesNo).OrderByDescending(w => w.Wins).ToList();
            List<Standings> finalResults = new List<Standings>();
            foreach (var item in results)
            {
                item.TeamLogoImageName = _TeamsRepository.GetImageTeamName(item.TeamName);
                item.PositionNo = positionCnt + 1;
                positionCnt += 1;
            }
            return results;
        }
    }
}
