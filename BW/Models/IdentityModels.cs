using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime? LoginTime { get; set; }
        public DateTime? LastPing { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime? BD { get; set; }
        public virtual ICollection<Clubs> Clubs { get; set; }
        public virtual ICollection<Books> Book { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Friends> Friend { get; set; }
        public virtual ICollection<site> Sites { get; set; }
        public virtual ICollection<Chat> Chats { get; set; } 
        public virtual ICollection<ChatMessage> Messages { get; set; } 
        public ApplicationUser()
        {
            Clubs = new List<Clubs>();
            Book = new List<Books>();
            Posts = new List<Post>();
            Friend = new List<Friends>();
            Sites = new List<site>();
            Chats = new List<Chat>();
            Messages = new List<ChatMessage>();
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
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }

        public Books()
        {

            Clubs = new List<Clubs>();
            Tags = new List<Tags>();
            ApplicationUser = new List<ApplicationUser>();
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
    public class Friends
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<ApplicationUser> User { get; set; }

        public Friends()
        {
            User = new List<ApplicationUser>();
        }
    }

    public class Clubs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string Admin { get; set; }
        public double Rating { get; set; }
        public int Readingbook { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<Books> Books { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public virtual ICollection<Post> Posts { get; set; }



        public Clubs()
        {
            Posts = new List<Post>();

            Tags = new List<Tags>();
            ApplicationUser = new List<ApplicationUser>();
            Books = new List<Books>();
        }

    }
}

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ApplicationUser> ChatUser { get; set; }
    public virtual ICollection<ChatMessage> Messages { get; set; } // все сообщения
    
    public Chat()
    {
        Messages = new List<ChatMessage>();
        ChatUser = new List<ApplicationUser>();
    }
}

public class ChatMessage
{
    public int Id { get; set; }
    
    public DateTime? Date { get; set; }

    public string Text { get; set; }
    public ApplicationUser User { get; set; }
    public Chat Chat { get; set; }
}

public class Post
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }

    public ApplicationUser User { get; set; } 
    public Clubs Clubs { get; set; }
}


public class site
{
    public int id { get; set; }
    public string url { get; set; }
    public ApplicationUser User { get; set; }
    public networkicon networkicon { get; set; }
}

public class networkicon
{
    public int id { get; set; }
    public string name { get; set; }
    public string ico { get; set; }
    public virtual ICollection<site> Sites { get; set; }
    public networkicon()
    {
        Sites = new List<site>();
    }
}
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Chat> Chat { get; set; }
    public DbSet<ChatMessage> ChatMessage { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<Clubs> Clubs { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Friends> Friends { get; set; }
    public DbSet<site> Sites { get; set; }
    public DbSet<networkicon> Networkicons { get; set; }
    public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }

}