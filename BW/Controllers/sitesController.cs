using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BW.Controllers
{
    public class sitesController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: sites
        public async Task<ActionResult> Index()
        {
            return View(await db.Sites.ToListAsync());
        }

        // GET: sites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.Sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sites/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,url,icon")] site site)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                //site.networkicon = db.Networkicons.Find();
                site.User = db.Users.Find(userId);
                db.Sites.Add(site);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(site);
        }

        // GET: sites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.Sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: sites/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,url")] site site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: sites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.Sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();

            site site = await db.Sites.FindAsync(id);
            db.Sites.Remove(site);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Account", new { id = userId});
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
