using System.ComponentModel.DataAnnotations;

namespace APIToDoListV1.Entities
{
    public class CategoryBlog
    {
 
            [Required]
            [Key]
            public int CategoryId { get; set; }

            [Required]
            public string Name { get; set; }

            public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        
    }
}
