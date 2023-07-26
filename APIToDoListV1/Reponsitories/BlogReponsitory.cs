using APIToDoListV1.Entities;
using AppChat_API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Blog;
using Model.SeekWork;
using System.Threading.Tasks;

namespace APIToDoListV1.Reponsitories
{
    public class BlogReponsitory : IBlogReponsitory
    {
        private readonly PostgresDbContext _context; // private only không thể thay đổi trong quá trình chạy ứng dụng 
        private readonly IUserRepository _userRepository;
        //  private readonly ILogger _logger; // private only không thể thay đổi trong quá trình chạy ứng dụng 
        public BlogReponsitory(PostgresDbContext context, IUserRepository userRepository)
        {
            //      _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<BlogPostReq> Create(BlogPostReq task)
        {
            try
            {
                var user = _userRepository.GetUserbyUserName("dangthien");
                var addCategory = await _context.CategoryBlog.AddAsync(new CategoryBlog
                {
                    CategoryId = 1,
                    Name = "Tesst",
                });
                    
                

                var blogEntity = new BlogPost
                {
                    Title = task.Title,
                    BlogPostId =  task.BlogPostId,
                    CategoryId = 1,
                    ImageUrls = task.ImageUrls,
                    CreatedAt = task.CreatedAt,
                    Content = task.Content,
                    Author = user
                };
                await _context.BlogPost.AddAsync(blogEntity);
                await _context.SaveChangesAsync();
                return task;
            }
            catch(Exception ex)
            {
              
                return null;
            }
        }

        public Task<BlogPostReq> Delete(BlogPostReq task)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogPostReponse>> GetAllBlogPost()
        {
            var result =  await _context.BlogPost.ToListAsync();
            var lstBlogReponse = result.Select(x => new BlogPostReponse()
            {
                BlogPostId = x.BlogPostId,
                CategoryId = x.CategoryId,
                Content = x.Content,
                ImageUrls = x.ImageUrls,
                Title = x.Title,    
            }).ToList();
            return lstBlogReponse;
        }

        public Task<BlogPostReq> GetById(BlogPostReq id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostReq> Update(BlogPostReq task)
        {
            throw new NotImplementedException();
        }
    }
}
