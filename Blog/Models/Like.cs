using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
