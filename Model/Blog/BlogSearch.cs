using Model.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Blog
{
    public class BlogSearch : PagingParameters
    {
        public string? TitleBlog { get; set; }
        public int? CategoryId { set; get; }
        public string? CategoryName { get; set; }
        public BlogSearch() { }

    }
}
