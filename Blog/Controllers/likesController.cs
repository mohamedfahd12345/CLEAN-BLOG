using System.Diagnostics;
using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    public class likesController : Controller
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

        public IActionResult like(Post post)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BlogContext db = new BlogContext();
            var find_like = db.Likes.Where(x => x.UserId == userId && x.PostId == post.Id).FirstOrDefault();
            var item_post = db.Posts.Where(x => x.Id == post.Id).FirstOrDefault();
            Console.WriteLine("ffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Console.WriteLine(post.Id);

            Console.WriteLine("ffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            if (find_like == null)
            {
              
                item_post.Liks++;

                db.Posts.Update(item_post);
                db.SaveChanges();
                var like_db = new Like();
                like_db.UserId = userId;
                like_db.PostId = post.Id;
                db.Likes.Add(like_db);
                db.SaveChanges();
                Console.WriteLine("ddddddddooooooooooooooooooooneeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            }
            else
            {
                item_post.Liks--;
                db.Posts.Update(item_post);
                db.SaveChanges();

                var remove_like = db.Likes.Where(x => x.UserId == userId && x.PostId == post.Id).FirstOrDefault();
                db.Likes.Remove(remove_like);
                db.SaveChanges();
                Console.WriteLine("nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn");

            }
            return Redirect("/Home/post/"+post.Id);
        }
    }
}
