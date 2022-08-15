using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Saved
    {
        public Saved()
        {
            DetailsSaveds = new HashSet<DetailsSaved>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<DetailsSaved> DetailsSaveds { get; set; }
    }
}
