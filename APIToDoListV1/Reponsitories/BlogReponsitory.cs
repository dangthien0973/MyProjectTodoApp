﻿using APIToDoListV1.Entities;
using APIToDoListV1.Extentions;
using AppChat_API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Blog;
using Model.SeekWork;
using Polly;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

                var blogEntity = new BlogPost
                {
                    Title = task.Title,
                    CategoryId = task.CategoryId,
                    Content = task.Content,
                    Author = user,
                    ImageUrls = task.ImageUrls,
                    Description = task.Description,
                    CreatedAt = DateTimeExtensions.SetKindUtc(DateTime.Now)

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

        public async Task<BlogPostReponse> GetById( int id)
        {
           var result = await  _context.BlogPost.FirstOrDefaultAsync(x=>x.BlogPostId == id);
            var finalResult = new BlogPostReponse
            {
                BlogPostId = result.BlogPostId,
                CategoryId = result.CategoryId,
                Content = result.Content,
                ImageUrls = result.ImageUrls,
                Title = result.Title,
            };
            return finalResult;
        }
        public Task<BlogPostReq> Update(BlogPostReq task)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<BlogPostReponse>> GetAllBlogPost(BlogSearch blogSearch)
        {
            var count = _context.BlogPost.Where(x => (blogSearch.CategoryId > 0 ? x.CategoryId == blogSearch.CategoryId : x.CategoryId > 0)&& (!string.IsNullOrEmpty(blogSearch.TitleBlog) ? x.Title.Contains(blogSearch.TitleBlog) || x.Content.Contains(blogSearch.TitleBlog) || x.Description.ToLower().Contains(blogSearch.TitleBlog.ToLower()) : x.Title != null)).Count();
            var queryData = await _context.BlogPost.Where(x => (blogSearch.CategoryId > 0 ? x.CategoryId == blogSearch.CategoryId : x.CategoryId > 0)
                                                          && (!string.IsNullOrEmpty(blogSearch.TitleBlog) ?  x.Title.Contains(blogSearch.TitleBlog) || x.Content.Contains(blogSearch.TitleBlog) || x.Description.ToLower().Contains(blogSearch.TitleBlog.ToLower()): x.Title != null)).Skip((blogSearch.PageNumber - 1) * blogSearch.PageSize).Take(blogSearch.PageSize).ToListAsync();
         
            var resultData = queryData.Select(x => new BlogPostReponse
            {
                BlogPostId= x.BlogPostId,
                Content = x.Content,
                ImageUrls= x.ImageUrls,
                Title   = x.Title,
                CategoryId= x.CategoryId,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            }).ToList();          
            return new PagedList<BlogPostReponse>(resultData, count, blogSearch.PageNumber, blogSearch.PageSize);
        }

        public async Task<List<CategoryBlog>> GetAllMenu()
        {
            var resutl = _context.CategoryBlog.ToList();
            return resutl;
        }
        public async Task<List<BlogPostReponse>> GetTopicPopular()
        {
            var result = await _context.BlogPost.OrderByDescending(x=> x.CreatedAt).Take(5).ToListAsync();
            var lstBlogReponse = result.Select(x => new BlogPostReponse()
            {
                BlogPostId = x.BlogPostId,
                CategoryId = x.CategoryId,
                Content = x.Content,
                ImageUrls = x.ImageUrls,
                Title = x.Title,
                CreatedAt = x.CreatedAt
            }).ToList();
            return lstBlogReponse;
        }

    }
}
