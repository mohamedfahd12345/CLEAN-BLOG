using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Blog.Controllers
{
    [Authorize]
    public class myfollowerController : Controller
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            BlogContext db = new BlogContext();
            var followers = db.Followers.Where(x => x.FatherId == userId).ToList();
            var list_followers = new List<User>();
            foreach(var item in followers)
            {
                var temp_user = db.Users.Where(x => x.UserId == item.ChildId).FirstOrDefault();
                list_followers.Add(temp_user);
            }
            return View(list_followers);
        }
    }
}
