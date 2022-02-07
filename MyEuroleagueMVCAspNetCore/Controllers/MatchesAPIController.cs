using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEuroleagueMVCAspNetCore.Models;
using ReflectionIT.Mvc.Paging;

namespace MyEuroleagueMVCAspNetCore.Controllers
{
    public class MatchesAPIController : Controller
    {
        private readonly Euroleague2020_21ASPDBContext _context;

        public MatchesAPIController(Euroleague2020_21ASPDBContext context)
        {
            _context = context;
        }

        // GET: MatchesAPI
        public async Task<IActionResult> Index(int searchRound, int page)
        {
            var pageIndex = (page == 0) ? 1 : page;
            var pageQuery = _context.Match.AsNoTracking().OrderByDescending(x => x.RoundNo);
            if (searchRound >0)
            {
                pageQuery = pageQuery.Where(c => c.RoundNo == searchRound).OrderBy(x => x.RoundNo);
            }
            var modelIndex = await PagingList.CreateAsync(pageQuery, _context.Team.Count()/2, pageIndex);
            return View(modelIndex);
        }

        // GET: MatchesAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // GET: MatchesAPI/Create
        public IActionResult Create()
        {
            var list = new List<TeamUser>();
            var teamNames = _context.Team.OrderBy(y => y.Name).Select(x => x.Name).ToList();
            foreach (var item in teamNames)
            {
                list.Add(new TeamUser
                {
                    Key = item,
                    Display = item
                });
            }
            var model = new Matches();
            var lastRoundNo = _context.Match.OrderByDescending(y => y.RoundNo).First().RoundNo;
            var matchesLastRound = _context.Match.Where(x => x.RoundNo == lastRoundNo).Count();
            if (matchesLastRound<_context.Team.Count() / 2)
            {
                model.RoundNo = lastRoundNo;
            }
            else
            {
                model.RoundNo = lastRoundNo + 1;
            }           
            model.TeamsList = new SelectList(list, "Key", "Display");
            return View(model);
        }

        // POST: MatchesAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchID,RoundNo,Home_Team,Away_Team,HomePointsScored,AwayPointsScored,HadExtraTime,EndOfFourthPeriodPoints")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matches);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception  ex)
                {
                    if (ex.InnerException.Message.Contains("IndexMatchRound")) {
                        return View("UniqueMatchRound", matches);
                    }
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(matches);
        }

        // GET: MatchesAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match.FindAsync(id);
            if (matches == null)
            {
                return NotFound();
            }
            var list = new List<TeamUser>();
            var teamNamesEdit = _context.Team.OrderBy(y => y.Name).Select(x => x.Name).ToList();
            foreach (var item in teamNamesEdit)
            {
                list.Add(new TeamUser
                {
                    Key = item,
                    Display = item
                });
            }
            var modelEdit = new Matches();
            modelEdit.TeamsList = new SelectList(list, "Key", "Display");
            modelEdit.MatchID = matches.MatchID;
            modelEdit.RoundNo = matches.RoundNo;
            modelEdit.Home_Team = matches.Home_Team;
            modelEdit.Away_Team = matches.Away_Team;
            modelEdit.HomePointsScored = matches.HomePointsScored;
            modelEdit.AwayPointsScored = matches.AwayPointsScored;
            modelEdit.HadExtraTime = matches.HadExtraTime;
            modelEdit.EndOfFourthPeriodPoints = matches.EndOfFourthPeriodPoints;
            return View(modelEdit);
        }

        // POST: MatchesAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchID,RoundNo,Home_Team,Away_Team,HomePointsScored,AwayPointsScored,HadExtraTime,EndOfFourthPeriodPoints")] Matches matches)
        {
            if (id != matches.MatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matches);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("IndexMatchRound"))
                        {
                            return View("UniqueMatchRound", matches);
                        }
                    }
                   
                }
                catch (DbUpdateConcurrencyException )
                {             
                    if (!MatchesExists(matches.MatchID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(matches);
        }

        // GET: MatchesAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Match
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // POST: MatchesAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matches = await _context.Match.FindAsync(id);
            _context.Match.Remove(matches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchesExists(int id)
        {
            return _context.Match.Any(e => e.MatchID == id);
        }
    }
}
