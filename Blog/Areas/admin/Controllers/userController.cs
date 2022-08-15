using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Blog.Areas.admin.Controllers
{
    [Area("admin")]
    public class userController : Controller
    {
        public IActionResult Index()
        {
            BlogContext db = new BlogContext();
            var list_users = db.Users.ToList();
            return View(list_users);
        }
        public IActionResult Details(string id)
        {
           
            BlogContext db = new BlogContext();
            var target_user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            return View(target_user);
        }
        public IActionResult Delete_post(int id)
        {
            BlogContext db = new BlogContext();
            var relted_comment = db.Comments.Where(x => x.PostId == id).ToList();

            foreach (var comment in relted_comment)
            {

                db.Comments.Remove(comment);
                db.SaveChanges();

            }
            var relted_likes = db.Likes.Where(x => x.PostId == id).ToList();
            foreach (var like in relted_likes)
            {
                db.Likes.Remove(like);
                db.SaveChanges();
            }

            var relted_details_saved = db.DetailsSaveds.Where(x => x.PostId == id).ToList();
            foreach (var details in relted_details_saved)
            {
                db.DetailsSaveds.Remove(details);
                db.SaveChanges();
            }
            var delete_item = db.Posts.Where(x => x.Id == id).FirstOrDefault();

            db.Posts.Remove(delete_item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public IActionResult delete_user(string id)
        //{

        //}
    }
}
