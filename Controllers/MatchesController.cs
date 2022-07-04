using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ST1.Context;
using ST1.Models;

namespace ST1.Controllers
{
    public class MatchesController : Controller
    {
        private readonly testEntities db = new testEntities();
        public List<Match> matchesList = new List<Match>();
        public List<Country> countriesList = new List<Country>();
        public async Task<ActionResult> Index()
        {
            matchesList = await db.Match.ToListAsync();
            countriesList = await db.Country.ToListAsync();

            List<MatchModel> matchmodels = new List<MatchModel>();

            foreach (var item in matchesList)
            {
                var model = new MatchModel()
                {
                    MatchID = item.MatchID,
                    MatchDate = (DateTime)item.MatchDate,
                    Home = countriesList.Where(x => x.CountryID == item.Home).Select(x => x.CountryName).Single(),
                    HomeScore = (int)item.HomeScore,
                    Visitor = countriesList.Where(x => x.CountryID == item.Visitor).Select(x => x.CountryName).Single(),
                    VisitorScore = (int)item.VisitorScore,
                    PlayStage = item.PlayStage,
                    PenaltyScore = item.PenaltyScore,
                    HomeScoreP = item.HomeScoreP,
                    VisitorScoreP = item.VisitorScoreP,
                };
                matchmodels.Add(model);
            }

            var list = matchmodels.OrderBy(x => x.PlayStage).ThenBy(x => x.MatchID);
            return View(list);
        }
        public async Task<ActionResult> UserIndex()
        {
            matchesList = await db.Match.ToListAsync();
            countriesList = await db.Country.ToListAsync();

            List<MatchModel> matchmodels = new List<MatchModel>();

            foreach (var item in matchesList)
            {
                var model = new MatchModel()
                {
                    MatchID = item.MatchID,
                    MatchDate = (DateTime)item.MatchDate,
                    Home = countriesList.Where(x => x.CountryID == item.Home).Select(x => x.CountryName).Single(),
                    HomeScore = (int)item.HomeScore,
                    Visitor = countriesList.Where(x => x.CountryID == item.Visitor).Select(x => x.CountryName).Single(),
                    VisitorScore = (int)item.VisitorScore,
                    PlayStage = item.PlayStage,
                    PenaltyScore = item.PenaltyScore,
                    HomeScoreP = item.HomeScoreP,
                    VisitorScoreP = item.VisitorScoreP,
                };
                matchmodels.Add(model);
            }

            var list = matchmodels.OrderBy(x => x.PlayStage).ThenBy(x => x.MatchID);
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Home = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.Visitor = new SelectList(db.Country, "CountryID", "CountryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MatchID,MatchDate,Home,HomeScore,Visitor,VisitorScore,PlayStage,PenaltyScore,HomeScoreP,VisitorScoreP")] Match match)
        {
            ViewBag.Home = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.Visitor = new SelectList(db.Country, "CountryID", "CountryName");
            if (match.Home == match.Visitor)
            {
                ModelState.AddModelError("Home", "Teams cannot play against each other.");
                ModelState.AddModelError("Visitor", "Teams cannot play against each other.");
                return View("Create");
            }
            if (match.PlayStage != "G")
            {
                var matchList = await db.Match.Where(x => x.PlayStage == match.PlayStage).ToListAsync();
                foreach (var item in matchList)
                {
                    if ((item.Home == match.Home && item.Visitor == match.Visitor) || (item.Visitor == match.Home && item.Home == match.Visitor))
                    {
                        ModelState.AddModelError("PlayStage", "Such a match has already been played in this phase of the tournament.");
                        return View("Create");
                    }
                    if (item.Home == match.Home || item.Visitor == match.Visitor || item.Visitor == match.Home || item.Home == match.Home)
                    {
                        ModelState.AddModelError("PlayStage", "This team has already participated in the match in this phase of the tournament.");
                        return View("Create");
                    }
                }
                if ((match.PlayStage == "F" && matchList.Count() == 1) || (match.PlayStage == "S" && matchList.Count() == 2) || (match.PlayStage == "Q" && matchList.Count() == 4) || (match.PlayStage == "R" && matchList.Count() == 8))
                {
                    ModelState.AddModelError("PlayStage", "The maximum number of matches in this phase of the competition has been reached.");
                    return View("Create");
                }
                if (match.PenaltyScore == "N" && match.HomeScore == match.VisitorScore)
                {
                    ModelState.AddModelError("Home", "In this phase of the competition, the match cannot end in a goal draw.");
                    ModelState.AddModelError("PlayStage", "In this phase of the competition, the match cannot end in a goal draw.");
                    ModelState.AddModelError("HomeScoreP", "In this phase of the competition, the match cannot end in a goal draw.");
                    ModelState.AddModelError("VisitorScoreP", "In this phase of the competition, the match cannot end in a goal draw.");
                    ModelState.AddModelError("Visitor", "In this phase of the competition, the match cannot end in a goal draw.");
                    return View("Create");
                }

            }
            else
            {
                var matchList = await db.Match.Where(x => x.PlayStage == match.PlayStage).ToListAsync();
                foreach (var item in matchList)
                {
                    if ((item.Home == match.Home && item.Visitor == match.Visitor) || (item.Visitor == match.Home && item.Home == match.Visitor))
                    {
                        ModelState.AddModelError("PlayStage", "Such a match has already been played in this phase of the tournament.");
                        return View("Create");
                    }
                    Country homeTeam = await db.Country.FindAsync(match.Home);
                    Country visitorTeam = await db.Country.FindAsync(match.Visitor);
                    if (homeTeam.Grupa != visitorTeam.Grupa)
                    {
                        ModelState.AddModelError("Home", "Teams playing group matches must be in the same group.");
                        ModelState.AddModelError("Visitor", "Teams playing group matches must be in the same group.");
                        return View("Create");
                    }
                }
            }


            if (ModelState.IsValid)
            {
                db.Match.Add(match);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(match);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = await db.Match.FindAsync(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MatchID,MatchDate,Home,HomeScore,Visitor,VisitorScore,PLayStage,PenaltyScore,HomeScoreP,VisitorScoreP")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(match);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = await db.Match.FindAsync(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            db.Match.Remove(match);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
