using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Follower
    {
        public int Id { get; set; }
        public string? FatherId { get; set; }
        public string? ChildId { get; set; }

        public virtual User? Child { get; set; }
        public virtual User? Father { get; set; }
    }
}
