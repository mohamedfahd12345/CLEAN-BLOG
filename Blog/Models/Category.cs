using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public int? PostId { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
