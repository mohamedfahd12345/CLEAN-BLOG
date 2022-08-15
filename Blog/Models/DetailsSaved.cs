using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class DetailsSaved
    {
        public int Id { get; set; }
        public int? SavedId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual Saved? Saved { get; set; }
    }
}
