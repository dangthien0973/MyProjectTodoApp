using APIToDoListV1.Entities;
using Model.SeekWork;
using Model;
using Model.Blog;

namespace APIToDoListV1.Reponsitories
{
    public interface  IBlogReponsitory 
    {
       public  Task<List<BlogPostReponse>> GetAllBlogPost();

        public  Task<BlogPostReq> Create(BlogPostReq task);

        public  Task<BlogPostReq> Update(BlogPostReq task);

        public  Task<BlogPostReq> Delete(BlogPostReq task);

        public  Task<BlogPostReq> GetById(BlogPostReq id);
    }
}
