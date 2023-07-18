using System.ComponentModel.DataAnnotations;

namespace APIToDoListV1.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
