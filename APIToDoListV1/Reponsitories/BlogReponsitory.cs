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
        private  readonly IMapper _mapper;
        public BlogReponsitory(PostgresDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BlogPostReq> Create(BlogPostReq task)
        {
            var blogEntity =  _mapper.Map<BlogPost>(task);
            await _context.BlogPost.AddAsync(blogEntity);
            await _context.SaveChangesAsync();
            return task;
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
                Category = x.CategoryId
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
