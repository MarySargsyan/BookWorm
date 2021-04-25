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
using Microsoft.AspNet.Identity;

namespace BW.Controllers
{
    public class PostsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public async Task<ActionResult> Index()
        {
            return View(await db.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,Text,Image")] Post post)
        {
            var userId = User.Identity.GetUserId();


            ApplicationUser user = db.Users.Find(userId);
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                post.User = user;
                db.Posts.Add(post);

                await db.SaveChangesAsync();
                return RedirectToAction("MyProfil", "Account");
            }

            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewClubPost([Bind(Include = "Id,Date,Text,Image")] Post post, int? Club_ID)
        {
            Clubs clubs = db.Clubs.Find(Club_ID);
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                post.Clubs = clubs;
                db.Posts.Add(post);

                await db.SaveChangesAsync();
                return RedirectToAction("ClubPage", "Home", new { id = Club_ID });
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clubs = db.Clubs.ToList();
            return View(post);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Text,Image")] Post post, int? Club_ID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();

            }
            return RedirectToAction("ClubPage", "Home",  new { id = Club_ID }); 
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clubs = db.Clubs.ToList();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int? Club_ID)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
           return RedirectToAction("ClubPage", "Home", new {id= Club_ID }); 
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
