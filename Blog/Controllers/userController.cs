using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    [Authorize]
    public class userController : Controller
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
        public IActionResult Index(string id)
        {

              BlogContext db = new BlogContext();
            Console.WriteLine("111111111111111111111111111111111111111111");
            var target_user=db.Users.Where(x=>x.UserId==id).FirstOrDefault();
            Console.WriteLine("111111111111111111111111111111111111111111");
            return View(target_user);
        }
        public IActionResult follow(User user)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            BlogContext db = new BlogContext();
            var find_user = db.Followers.Where(x => x.ChildId == userId && x.FatherId == user.UserId).FirstOrDefault();
            var father = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (find_user == null)
            {
                if (father.Followers == null)
                    father.Followers = 0;

                father.Followers++;
                db.Users.Update(father);
                db.SaveChanges();

                var new_follow = new Follower();
                new_follow.FatherId = user.UserId;
                new_follow.ChildId = userId;
                db.Followers.Add(new_follow);
                db.SaveChanges();
            }
            else
            {
                father.Followers--;
                db.Users.Update(father);
                db.SaveChanges();

                db.Followers.Remove(find_user);
                db.SaveChanges();
            }
            return Redirect("/user/Index/" + user.UserId);
        }
    }
}
