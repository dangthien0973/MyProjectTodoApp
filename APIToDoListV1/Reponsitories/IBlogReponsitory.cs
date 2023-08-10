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

        public  Task<BlogPostReponse> GetById(int id);
        public Task<PagedList<BlogPostReponse>> GetAllBlogPost(BlogSearch blogSearch);
        public  Task<List<CategoryBlog>> GetAllMenu();
        public Task<List<BlogPostReponse>> GetTopicPopular();
    }
}
