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
    public class PlayersController : Controller
    {
        private testEntities db = new testEntities();
        /*public async Task<ActionResult> Scorer()
        {
            List<GoalDetails> playerList = await db.GoalDetails.ToListAsync();
            List<Scorer> scorerList = new List<Scorer>();
            foreach (var item in playerList)
            {
                Scorer model = new Scorer()
                {
                    model.Age = item.
                };
                if(scorerList.Contains(model))
            }
            return View(scorerList);
        }*/
        public ActionResult CreatePlayer()
        {
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlayerID,CountryID,PlayerFullName,Position,Age,Club")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Player.Add(player);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminDetails", "Countries", new { id = player.CountryID });
            }

            ViewBag.TeamID = new SelectList(db.Country, "CountryID", "CountryName", player.CountryID);
            return RedirectToAction("AdminDetails", "Countries", player.CountryID);
        }
        public async Task<ActionResult> EditPlayer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await db.Player.FindAsync(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Country, "CountryID", "CountryName", new { id = player.CountryID });
            return View("Edit", player);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlayerID,TeamID,PlayerFullName,Position,Age,Club")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminDetails", "Countries", new { id = player.CountryID });
            }
            ViewBag.TeamID = new SelectList(db.Country, "CountryID", "CountryName", new { id = player.CountryID });
            ModelState.AddModelError("PlayerFullName", "Something gone wrong.");
            return RedirectToAction("EditPlayer", "Players", new { id = player.CountryID });
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await db.Player.FindAsync(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            db.Player.Remove(player);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminDetails", "Countries", new { id = player.CountryID });
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
