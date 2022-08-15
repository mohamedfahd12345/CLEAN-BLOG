using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public string? UserId { get; set; }
        public string? Decription { get; set; }
        public int? Likes { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
