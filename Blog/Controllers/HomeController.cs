using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            BlogContext db = new BlogContext();
            var item=db.Users.Where(x=>x.UserId==userId).FirstOrDefault();  
            if (item == null)
            {
                var user = new User();
                user.UserId = userId;
                user.UserEmail = userName;
                db.Users.Add(user) ;
                db.SaveChanges();
            }
            ////////////////////////////////////////////////////////////////////////
          //  System.Web.Security.Roles.AddUserToRole("700ba57a-bf9b-4392-9f5f-a575d489d32a", "admin");

            var posts = db.Posts.ToList();
            return View(posts);
            
        }

        public IActionResult post(int id)
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

            var one_post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
            return View(one_post);
        }

        [HttpPost]
        public IActionResult comment(Post post)
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
            var comment = new Comment();
            comment.UserId=userId;
            comment.Decription = post.Header;
            comment.Likes = 0;
            comment.PostId = post.Id;

            db.Comments.Add(comment);
            db.SaveChanges();
            return Redirect("/Home/post/"+post.Id);
        }

        public IActionResult mostviewed()
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
            /////////////////////////////////////////////////////////////////////////////
            var mostview = db.Posts.OrderByDescending(x => x.Views).ToList();
            return View("Index",mostview);
        }
        public IActionResult newest()
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
            /////////////////////////////////////////////////////////////////////////////
            var mostnew = db.Posts.OrderByDescending(x => x.Date).ToList();
            return View("Index",mostnew);
        }
        public IActionResult mostlike()
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
            ///////////////////
            var mostlike_list = db.Posts.OrderByDescending(x => x.Liks).ToList();
            return View("Index", mostlike_list);
        }
        //public IActionResult following()
        //{
        //    checkuser();
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
        //    var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
        //    BlogContext db = new BlogContext();
            
            
        //    var follwing_list = db.Posts.OrderByDescending(x => x.Liks).ToList();
        //    return View("Index");
        //}
    }
}