using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Blog.Controllers
{
    [Authorize]
    public class LISTController : Controller
    {
        public void checkuser()
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
            ////////////////////////////////////////////////////////////////////////
        }

        public IActionResult Index(Post post)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            BlogContext db = new BlogContext();
            Console.WriteLine(post.CategoryName);
            Console.WriteLine(post.Id);
            Console.WriteLine(post.Description);
            var new_saved = new Saved();
            if (post.Description == null)
            {
                var item_Saved = db.Saveds.Where(x => x.UserId == userId && x.Name == post.CategoryName).FirstOrDefault();
                var new_details = new DetailsSaved();
                new_details.PostId = post.Id;
                new_details.SavedId = item_Saved.Id;
                db.DetailsSaveds.Add(new_details);
                db.SaveChanges();
                ////////////////
               
            }
            else
            {
                new_saved.Name = post.Description;
                new_saved.UserId = userId;
                db.Saveds.Add(new_saved);
                db.SaveChanges();

                var item_Saved = db.Saveds.Where(x => x.UserId == userId && x.Name == post.Description).FirstOrDefault();
                var new_details = new DetailsSaved();
                new_details.PostId = post.Id;
                new_details.SavedId = item_Saved.Id;
                db.DetailsSaveds.Add(new_details);
                db.SaveChanges();
                return Redirect("/Home/post/" + post.Id);


            }
            return Redirect("/Home/post/" + post.Id);
        }
        public IActionResult ALLLISTS()
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            BlogContext db = new BlogContext();
            var item=db.Saveds.Where(x=>x.UserId==userId).ToList();
            return View(item);
        }
        public IActionResult ONELIST(int id)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            BlogContext db = new BlogContext();
            var item = db.DetailsSaveds.Where(x => x.SavedId == id).ToList();
             List <Post> list_posts = new List<Post>();
            foreach(var one in item)
            {
                var one_post = db.Posts.Where(x => x.Id == one.PostId).FirstOrDefault();
                list_posts.Add(one_post);
            }
            return View(list_posts);
        }
    }
}
