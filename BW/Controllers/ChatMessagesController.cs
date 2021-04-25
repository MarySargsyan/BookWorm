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
    public class ChatMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChatMessages
        public async Task<ActionResult> Index()
        {
            return View(await db.ChatMessage.ToListAsync());
        }

        // GET: ChatMessages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatMessage chatMessage = await db.ChatMessage.FindAsync(id);
            if (chatMessage == null)
            {
                return HttpNotFound();
            }
            return View(chatMessage);
        }

        // GET: ChatMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatMessages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Text")] ChatMessage chatMessage, int? Club_id, int? Chat_id)
        {
            var userId = User.Identity.GetUserId();
            Chat chat = db.Chat.Find(Chat_id);
            ApplicationUser user = db.Users.Find(userId);
            if (ModelState.IsValid)
            {
                chatMessage.Chat = chat;
                chatMessage.Date = DateTime.Now;
                chatMessage.User = user;
                db.ChatMessage.Add(chatMessage);

                await db.SaveChangesAsync();
                return RedirectToAction("ClubPage", "Home", new { id = Club_id});
            }

            return View(chatMessage);
        }

  

        // GET: ChatMessages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatMessage chatMessage = await db.ChatMessage.FindAsync(id);
            if (chatMessage == null)
            {
                return HttpNotFound();
            }
            return View(chatMessage);
        }

        // POST: ChatMessages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Text,ApplicationUserId")] ChatMessage chatMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatMessage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chatMessage);
        }

        // GET: ChatMessages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatMessage chatMessage = await db.ChatMessage.FindAsync(id);
            if (chatMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clubs = db.Clubs.ToList();
            ViewBag.Chats = db.Chat.ToList();
            return View(chatMessage);

        }

        // POST: ChatMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int? Clubs_ID)
        {
            ChatMessage chatMessage = await db.ChatMessage.FindAsync(id);
            db.ChatMessage.Remove(chatMessage);
            await db.SaveChangesAsync();
            return RedirectToAction("ClubPage", "Home", new {id = Clubs_ID });
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
