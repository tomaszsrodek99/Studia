using ST1.Context;
using ST1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ST1.Controllers
{
    public class TournamentViewController : Controller
    {
        private testEntities db = new testEntities();
        public List<Match> matchList = new List<Match>();
        public List<Country> countryList = new List<Country>();
        public ActionResult IndexGroup()
        {
            matchList = db.Match.Where(x => x.PlayStage == "G").ToList();
            countryList = db.Country.Where(x=>x.Grupa != null).ToList();
            var matchForView = new List<GroupModel>();
            foreach (var country in countryList)
            {
                GroupModel model = new GroupModel()
                {
                    Name = country.CountryName,
                    Group = country.Grupa,
                    Matches = 0,
                    GoalsAgainst = 0,
                    GoalsFor = 0,
                    Points = 0
                };
                if(matchList.Count() == 0)
                    matchForView.Add(model);
                foreach (var match in matchList)
                {
                    if(country.CountryID == match.Home)
                    {
                        model.GoalsFor += (int)match.HomeScore;
                        model.GoalsAgainst += (int)match.VisitorScore;
                        model.Matches++;
                        if (match.HomeScore > match.VisitorScore)
                            model.Points += 3;
                        if (match.HomeScore == match.VisitorScore)
                            model.Points += 1;
                        if(matchForView.Where(x=>x.Name == model.Name).Any()) { }
                        else matchForView.Add(model);

                    }
                    if (country.CountryID == match.Visitor)
                    {
                        model.GoalsFor += (int)match.VisitorScore;
                        model.GoalsAgainst += (int)match.HomeScore;
                        model.Matches++;
                        if (match.VisitorScore > match.HomeScore)
                            model.Points += 3;
                        if (match.VisitorScore == match.HomeScore)
                            model.Points += 1;
                        if (matchForView.Where(x => x.Name == model.Name).Any()) { }
                        else matchForView.Add(model);
                    }
                }
            }
            string[] tab = new string[] { "A", "B", "C", "D", "E", "F" };
            ViewBag.Group = tab;
            var list = matchForView.OrderByDescending(x => x.Points).ThenBy(x=>x.Matches).ThenBy(x=>x.GoalsFor);
            return View("Index", list);
        }
    }
}