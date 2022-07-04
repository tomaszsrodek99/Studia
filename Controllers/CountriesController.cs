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
    public class CountriesController : Controller
    {
        private readonly testEntities db = new testEntities();
        public List<Country> countriesList = new List<Country>();
        public List<Coach> coachesList = new List<Coach>();
        public List<Player> playersList = new List<Player>();
        public async Task<ActionResult> Index()
        {
            countriesList = await db.Country.ToListAsync();
            var list = countriesList.OrderBy(x => x.CountryName);
            return View(list);
        }
        public async Task<ActionResult> AdminIndex()
        {
            countriesList = await db.Country.ToListAsync();
            var list = countriesList.OrderBy(x => x.CountryName);
            return View("AdminIndex", list);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = new Country();
            playersList = new List<Player>();
            playersList.Sort();
            try
            {
                country = await db.Country.FindAsync(id);
                playersList = await db.Player.Where(x => x.CountryID == id).OrderBy(x => x.Position).ToListAsync();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            if (country == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = country;
            return View("Details", playersList);
        }
        public async Task<ActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = new Country();
            playersList = new List<Player>();
            playersList.Sort();
            try
            {
                country = await db.Country.FindAsync(id);
                playersList = await db.Player.Where(x => x.CountryID == id).OrderBy(x => x.Position).ToListAsync();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            if (country == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = country;
            return View("AdminDetails", playersList);
        }
        public ActionResult Create()
        {
            try
            {
                countriesList = db.Country.ToList();
                coachesList = db.Coach.ToList();
            }
            catch (Exception create)
            {
                ViewBag.Error = create.Message;
            }
            var list = coachesList.Where(p => countriesList.All(p2 => p2.CoachID != p.CoachID));
            ViewBag.Coach = list;
            ViewBag.Group = new SelectList(Enum.GetValues(typeof(CountryModel.Group)));
            return View("CountryCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountryID,CoachID,CountryName,Grupa")] CountryModel country)
        {
            var countriesName = db.Country.Select(x => x.CountryName).ToList();
            if (countriesName.Contains(country.CountryName))
            {
                ModelState.AddModelError("CountryName", "This team already exist.");
                return RedirectToAction("Create");
            }
            Country model = new Country
            {
                CoachID = country.CoachID,
                CountryName = country.CountryName,
                Grupa = country.Grupa.ToString()
            };
            if (ModelState.IsValid)
            {
                db.Country.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminIndex");
            }
            return RedirectToAction("Create");
        }
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                countriesList = db.Country.ToList();
                coachesList = db.Coach.ToList();
            }
            catch (Exception create)
            {
                ViewBag.Error = create.Message;
            }
            var list = coachesList.Where(p => countriesList.All(p2 => p2.CoachID != p.CoachID)).ToList();
            ViewBag.Coach = list;
            ViewBag.Group = new SelectList(Enum.GetValues(typeof(CountryModel.Group)));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await db.Country.FindAsync(id);
            list.Add(country.Coach);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View("CountryEdit", country);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CountryID,CoachID,CountryName,Grupa")] Country country)
        {
            var countriesName = db.Country.Where(x => x.CountryName != country.CountryName).Select(x => x.CountryName).ToList();
            if (countriesName.Contains(country.CountryName))
            {
                ModelState.AddModelError("CountryName", "This team already exist.");
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminIndex");
            }
            return View(country);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            playersList = await db.Player.ToListAsync();
            foreach (var item in playersList)
            {
                Player model = item;
                if (item.CountryID == id)
                {
                    db.Player.Remove(model);
                    db.SaveChanges();
                }
            }
            Country country = await db.Country.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            db.Country.Remove(country);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminIndex");
        }
    }
}
