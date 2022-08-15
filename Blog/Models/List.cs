using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class List
    {
        public List()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SavedId { get; set; }

        public virtual Saved? Saved { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
