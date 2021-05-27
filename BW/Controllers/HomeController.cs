using BW.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BW.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index( int? page, string searchString)
        {
            var clubs = from m in context.Clubs
                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                clubs = clubs.Where(s => s.Name.Contains(searchString)|
                s.Tags.Where(c => c.Name.Contains(searchString)).Count() > 0 ? true : false |
                s.Books.Where(b => b.Name.Contains(searchString)).Count() > 0 ? true: false);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            List<Clubs> clublist = clubs.ToList();
            return View(clublist.ToPagedList(pageNumber, pageSize));
        }
       
        public ActionResult News(string searchString)
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = context.Users.Find(userId);
            if (userId == " ") return RedirectToAction("Index");
            if (user == null)
            {
                return HttpNotFound();
            }

            var post = from p in context.Posts
                       orderby p.Date
                       select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                post = (IOrderedQueryable<Post>)post.Where(s => s.Text.Contains(searchString) |
                s.User.UserName.Contains(searchString)| 
                s.Clubs.Name.Contains(searchString));
            }
            //ViewBag.Posts = context.Posts.ToList().OrderByDescending(s => s.Date);
            ViewBag.Posts = post.ToList().OrderByDescending(s => s.Date);

            return View(user);
        }
        [HttpPost]
        public ActionResult News(ApplicationUser user)
        {
            ApplicationUser newuser = context.Users.Find(user.Id);
            newuser.Posts.Clear();
            context.Entry(newuser).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Clubpage(int? id)
        {
            var userId = User.Identity.GetUserId();
            Clubs clubs = context.Clubs.Find(id);

            if (id == null) return RedirectToAction("Index");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            if(clubs.Admin == userId)
            {
                return RedirectToAction("MyClub", new { id = id});
            }
            ViewBag.Posts = context.Posts.ToList().OrderByDescending(s => s.Date);

            ViewBag.Books = context.Books.ToList();
            ViewBag.Users = context.Users.ToList();
            ViewBag.Chats = context.Chat.ToList();
            return View(clubs);
        }
        public ActionResult MyClub(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);
            if (id == null) return RedirectToAction("Index");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            ViewBag.Posts = context.Posts.ToList().OrderByDescending(s => s.Date);

            ViewBag.Books = context.Books.ToList();
            ViewBag.Users = context.Users.ToList();
            ViewBag.Chats = context.Chat.ToList();
            return View(clubs);
        }

        
        [HttpGet]
        public ActionResult DeleteClub(int? id)
        {
            if (id == null)
            { return HttpNotFound(); }
            Clubs b = context.Clubs.Find(id);
            if (b == null) 
            { return HttpNotFound();}
            return View(b);
        }
        [HttpPost, ActionName("DeleteClub")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            { return HttpNotFound(); }
            Clubs b = context.Clubs.Find(id);
            if (b == null)
            { return HttpNotFound(); }

            context.Clubs.Remove(b);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
       

        public ActionResult Edit(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);
            if (id == null) return RedirectToAction("Clubpage");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = context.Tags.ToList();
            return View(clubs);
        } 
        

        [HttpPost]
        public ActionResult Edit(Clubs clubs, int[] selectedTags, int[] selectedBooks)
        {
            Clubs newclub = context.Clubs.Find(clubs.Id);
            newclub.Name = clubs.Name;
            newclub.Discription = clubs.Discription;
            //менять фотрграфия и иконку

            newclub.Tags.Clear();
            if (selectedTags != null)
            {
                foreach (var c in context.Tags.Where(co => selectedTags.Contains(co.Id)))
                {
                    newclub.Tags.Add(c);
                }
            }

            context.Entry(newclub).State = EntityState.Modified;
            context.SaveChanges();
       
                return RedirectToAction("Clubpage", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Clubs clubs)
        {
            var userr = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {

                clubs.Name = " ";
                clubs.Discription = " ";
                clubs.Image = "/Images/Clubimg.jpg";
                clubs.Admin = userr;
                context.Clubs.Add(clubs);
                await context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = clubs.Id });

            }

            return View(clubs);
        }



        public ActionResult Bookadd(string searchString, Clubs clubs)
        {
            Clubs clubs1 = context.Clubs.Find(clubs.Id);
            ViewBag.Books = context.Books.Where(s => s.Name.Contains(searchString)).ToList();
            return View(clubs1);
        }


        [HttpPost]
        public ActionResult Bookadd(Clubs clubs, Books books)
        {
            Clubs newclub = context.Clubs.Find(clubs.Id);
            Books books1 = context.Books.Find(books.Id);

            newclub.Books.Add(books1);
            newclub.Readingbook = 0;
            newclub.Readingbook = books1.Id;
           
            context.SaveChanges();
            return RedirectToAction("ClubPage");
        }

        public ActionResult Createtag()
        {
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Createtag([Bind(Include = "Id,Name")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                context.Tags.Add(tags);
                await context.SaveChangesAsync();
                return RedirectToAction("Edit");

            }

            return View(tags);
        }

        public ActionResult bookcheck (int? id)
        {
            Clubs clubs1 = context.Clubs.Find(id);
            clubs1.Readingbook = 0;
            context.SaveChanges();
            return RedirectToAction("Edit");
        }

        public ActionResult ClubChat(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);
            if (id == null) return RedirectToAction("Index");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return PartialView();

        } 
        public ActionResult ClubNews(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);
            if (id == null) return RedirectToAction("Index");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return PartialView();

        } 
        public ActionResult MyClubNews(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);
            if (id == null) return RedirectToAction("Index");
            if (clubs == null)
            {
                return HttpNotFound();
            }
            return PartialView();

        }

        public ActionResult Join(int? id)
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = context.Users.Find(userId);
            Clubs clubs = context.Clubs.Find(id);
            clubs.ApplicationUser.Add(user);
            context.SaveChanges();

            return RedirectToAction("ClubPage", new { id = id });


        }
        public ActionResult left(int? id)
        {
            Clubs clubs = context.Clubs.Find(id);

            var userId = User.Identity.GetUserId();
            ApplicationUser user = context.Users.Find(userId);
            clubs.ApplicationUser.Remove(user);
            context.SaveChanges();

            return RedirectToAction("ClubPage", new { id = id});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostInNews([Bind(Include = "Id,Date,Text,Image")] Post post)
        {
            var userId = User.Identity.GetUserId();


            ApplicationUser user = context.Users.Find(userId);
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                post.User = user;
                context.Posts.Add(post);

                await context.SaveChangesAsync();
                return RedirectToAction("News");
            }

            return View(post);
        }
    }
}