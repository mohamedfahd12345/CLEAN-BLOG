using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    public class categoryController : Controller
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
        public IActionResult Index()
        {
            checkuser();
            BlogContext db = new BlogContext();
            var list_category = db.Categories.ToList();
            return View(list_category);

            //return View();
        }
        public IActionResult Details(int id)
        {
            checkuser();
            BlogContext db = new BlogContext();
            var list_posts=db.Posts.Where(x=>x.CategoryId==id).ToList();
            return View(list_posts);
          
        }
        public IActionResult mostviewed(int id)
        {
            checkuser();
            BlogContext db = new BlogContext();
            var mostview = db.Posts.Where(x=>x.CategoryId==id).OrderByDescending(x => x.Views).ToList();
            return View("Details",mostview);
        }
        public IActionResult newest(int id)
        {
            checkuser();
            BlogContext db = new BlogContext();
            var mostnew = db.Posts.Where(x => x.CategoryId == id).OrderByDescending(x => x.Date).ToList();
            return View("Details", mostnew);
        }
        public IActionResult mostlike(int id)
        {
            checkuser();
            BlogContext db = new BlogContext();
            var mostlike_list = db.Posts.Where(x => x.CategoryId == id).OrderByDescending(x => x.Liks).ToList();
            return View("Details", mostlike_list);
        }
    }
}
