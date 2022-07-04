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

namespace ST1.Controllers
{
    public class CoachesController : Controller
    {
        private testEntities db = new testEntities();
        public async Task<ActionResult> Index()
        {
            return View(await db.Coach.ToListAsync());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = await db.Coach.FindAsync(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CoachID,Firstname,Lastname")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Coach.Add(coach);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Coaches");
            }
            return RedirectToAction("Create");

        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = await db.Coach.FindAsync(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CoachID,Firstname,Lastname")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coach);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = await db.Coach.FindAsync(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            db.Coach.Remove(coach);
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
