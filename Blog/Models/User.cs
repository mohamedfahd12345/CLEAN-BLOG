using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FollowerChildren = new HashSet<Follower>();
            FollowerFathers = new HashSet<Follower>();
            Likes = new HashSet<Like>();
            Posts = new HashSet<Post>();
            Saveds = new HashSet<Saved>();
        }

        public string UserId { get; set; } = null!;
        public int? PostId { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int? Followers { get; set; }

        public virtual AspNetUser UserNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follower> FollowerChildren { get; set; }
        public virtual ICollection<Follower> FollowerFathers { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Saved> Saveds { get; set; }
    }
}
