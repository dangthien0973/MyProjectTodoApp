using System.ComponentModel.DataAnnotations;

namespace APIToDoListV1.Entities
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User Author { get; set; }

        public int CategoryId { get; set; }
        public CategoryBlog Category { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        // Other related properties
        public List<string> ImageUrls { get; set; } // List of image URLs
    }
}
