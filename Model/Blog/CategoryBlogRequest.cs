using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Blog
{
    public  class CategoryBlogRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<BlogPostReq> BlogPosts { get; set; } = new List<BlogPostReq>();
    }
}
