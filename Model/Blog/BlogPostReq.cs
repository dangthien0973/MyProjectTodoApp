using Model.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Blog
{
    public  class BlogPostReq
    {

        public int BlogPostId { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
        public string ImageUrls { get; set; } // List of image URLs
        public string Description { set; get; }
    }
}
