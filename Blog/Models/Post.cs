using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            DetailsSaveds = new HashSet<DetailsSaved>();
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public int? Liks { get; set; }
        public DateTime? Date { get; set; }
        public int? CommentId { get; set; }
        public int? Views { get; set; }
        public int? CategoryId { get; set; }
        public string? UserId { get; set; }
        public string? Header { get; set; }
        public string? CategoryName { get; set; }
        public int? ListId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<DetailsSaved> DetailsSaveds { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
