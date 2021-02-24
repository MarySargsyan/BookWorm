using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW.Models;

namespace BW.Controllers
{
    public class ClubsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Clubs.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clubs clubs = await db.Clubs.FindAsync(id);
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return View(clubs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Discription,Icon,Image,Rating,Readingbook")] Clubs clubs)
        {
            if (ModelState.IsValid)
            {
                db.Clubs.Add(clubs);
                await db.SaveChangesAsync();
                return RedirectToAction("ClubPage", "Home", new { id = clubs.Id });

            }

            return View(clubs);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clubs clubs = await db.Clubs.FindAsync(id);
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return View(clubs);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Discription,Icon,Image,Rating,Readingbook")] Clubs clubs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clubs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clubs);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clubs clubs = await db.Clubs.FindAsync(id);
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return View(clubs);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clubs clubs = await db.Clubs.FindAsync(id);
            db.Clubs.Remove(clubs);
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
