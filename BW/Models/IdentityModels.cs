using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using BW.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BW.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        //public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string about { get; set; }
        public string Image { get; set; }
        //public DateTime LoginTime { get; set; }
        //public DateTime LastPing { get; set; }
        public virtual ICollection<Clubs> Clubs { get; set; }
        public virtual ICollection<Books> Book { get; set; }

        public ApplicationUser()
        {
            Clubs = new List<Clubs>();
            Book = new List<Books>();

        }

    }

    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Discription { get; set; }
        public double Raiting { get; set; }
        public string Link { get; set; }
        
        public virtual ICollection<Clubs> Clubs { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Books()
        {

            Clubs = new List<Clubs>();
            Tags = new List<Tags>();
            Users = new List<ApplicationUser>();
        }
    }

    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Clubs> Clubs { get; set; }
        public virtual ICollection<Books> Books { get; set; }

        public Tags()
        {
            Clubs = new List<Clubs>();
            Books = new List<Books>();

        }
    }

    public class Clubs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public int Readingbook { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<Books> Books { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        //public int ChatsId { get; set; }
        //public Chat Chat { get; set; }

        //public int ReadBookId { get; set; }
        //public Books ReadBook { get; set; }


        public Clubs()
        {
            Tags = new List<Tags>();
            Users = new List<ApplicationUser>();
            Books = new List<Books>();
        }

    }
}

public class Chat
{
    public int Id { get; set; }
    public String Name { get; set; }

    public List<ChatMessage> Messages { get; set; } // все сообщения

    public Chat()
    {
        Messages = new List<ChatMessage>();

    }
}

public class ChatMessage
{
    public int Id { get; set; }
    
    public DateTime Date = DateTime.Now;

    public string Text = "";
    public int ApplicationUserId { get; set; }
    public ApplicationUser User { get; set; }
}




public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Chat> Chat { get; set; }
    public DbSet<ChatMessage> ChatMessage { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<Clubs> Clubs { get; set; }
    public DbSet<Tags> Tags { get; set; }

    public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }


}