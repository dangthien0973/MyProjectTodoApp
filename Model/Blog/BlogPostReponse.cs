using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Blog
{
    public class BlogPostReponse
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        
        public int CategoryId { get; set; }
        

       // public List<Comment> Comments { get; set; } = new List<Comment>();
        // Other related properties
        public List<string> ImageUrls { get; set; } // List of image URLs
    }
}
