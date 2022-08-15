using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    [Authorize]
    public class MYController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MYController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            BlogContext db = new BlogContext();
            var item = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.UserId = userId;
                user.UserEmail=userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            /////////////////////////////////////////////////////////////////////////////////////////
           var item2= db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            return View(item2);
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
           
            BlogContext db = new BlogContext();
            var item = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            item.UserName = user.UserName;
            item.Description = user.Description;
            item.Photo = user.Photo;
            db.Users.Update(item);
            db.SaveChanges();
            //var posts = db.Posts.Where(x => x.UserId == userId).ToList();
            //foreach (var it in posts)
            //{
            //    it.UserName = item.UserName;
            //    db.Posts.Update(it);
            //    db.SaveChanges();
            //}
            //db.SaveChanges();
            //var list_comment = db.Commets.Where(x => x.UserId == userId).ToList();
            //foreach (var it in list_comment)
            //{
            //    it.UserName = item.UserName;
            //    db.Commets.Update(it);
            //    db.SaveChanges();
            //}
            return Redirect("/Home/Index");
        }

        public IActionResult create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult create(Post post)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            BlogContext db = new BlogContext();
            var item = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.UserId = userId;
                user.UserEmail = userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            var item2 = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            /////////////////////////////////////////////////////////
          //  BlogContext db = new BlogContext();
            var item3 = new Post();
            item3.Header = post.Header;
            item3.Description = post.Description;
            item3.CategoryName = post.CategoryName;
            item3.Date = DateTime.Now;
            item3.Photo = post.Photo;
            item3.Liks = 0;
            item3.Views = 0;
            item3.UserId = userId;
            var temp = db.Categories.Where(x => x.Name == post.CategoryName).FirstOrDefault();
            item3.CategoryId = temp.Id;
            db.Posts.Add(item3);
            db.SaveChanges();
            return View();

        }

        public IActionResult edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            BlogContext db = new BlogContext();
            var item = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.UserId = userId;
                user.UserEmail = userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            var item2 = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            /////////////////////////////////////////////////////////  

            var edit_post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
            return View(edit_post);
        }
        [HttpPost]
        public IActionResult edit(Post post)
        {
            BlogContext db = new BlogContext();
            var item = db.Posts.Where(x => x.Id == post.Id).FirstOrDefault();
            item.Description=post.Description;
            item.Header = post.Header;
            item.Photo=post.Photo;
            item.CategoryName = post.CategoryName;
            var temp = db.Categories.Where(x => x.Name == post.CategoryName).FirstOrDefault();
            item.CategoryId = temp.Id;
            db.Posts.Update(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            BlogContext db = new BlogContext();
            var item = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.UserId = userId;
                user.UserEmail = userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            var item2 = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            /////////////////////////////////////////////////////////  
            var relted_comment = db.Comments.Where(x => x.PostId == id).ToList();

            foreach (var comment in relted_comment)
            {

                db.Comments.Remove(comment);
                db.SaveChanges();

            }
            var relted_likes=db.Likes.Where(x=>x.PostId == id).ToList();
            foreach(var like in relted_likes)
            {
                db.Likes.Remove(like);
                db.SaveChanges();
            }

            var relted_details_saved = db.DetailsSaveds.Where(x => x.PostId == id).ToList();
            foreach(var details in relted_details_saved)
            {
                db.DetailsSaveds.Remove(details);
                db.SaveChanges();
            }
            var delete_item = db.Posts.Where(x => x.Id == id).FirstOrDefault();
           
            db.Posts.Remove(delete_item);
            db.SaveChanges();
            
            return Redirect("/MY/Index");
        }
    }
}
