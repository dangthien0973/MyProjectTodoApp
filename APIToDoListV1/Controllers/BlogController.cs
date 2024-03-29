﻿using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.SeekWork;
using Model;
using Model.Enums;

using Model.Blog;
using erpsolution.entities.Common;
using System.Reflection.Metadata;
using APIToDoListV1.Entities.Common;

namespace APIToDoListV1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController :  ControllerBase
    {
        private readonly IBlogReponsitory _blogRepsitory;


        public BlogController(IBlogReponsitory blogRepsitor)
        {

            _blogRepsitory = blogRepsitor;
        }
        [HttpGet]
        public async Task<HandleList<BlogPostReponse>> GetAllBlog()
        {
            try
            {
                var listTodo = await _blogRepsitory.GetAllBlogPost();
                return new HandleList<BlogPostReponse>(listTodo);
            }
            catch (Exception ex) {

                return new HandleList<BlogPostReponse>();
            }
        }

        [HttpGet]
        [Route(nameof(GetTopicPopular))]
        public async Task<HandleList<BlogPostReponse>> GetTopicPopular()
        {
            try
            {
                var listBlog = await _blogRepsitory.GetTopicPopular();
                return new HandleList<BlogPostReponse>(listBlog);
            }
            catch (Exception ex)
            {

                return new HandleList<BlogPostReponse>();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HandleResponse<BlogPostReponse>> GetById([FromRoute] int id)
        {
            var task = await _blogRepsitory.GetById(id);
            if (task == null) return new HandleResponse<BlogPostReponse>(task);
            return new HandleResponse<BlogPostReponse>(task);
        }



        [HttpPost]
        public async Task<HandleResponse<bool>> Create([FromBody] BlogPostReq request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new HandleResponse<bool>(false, "Thất bại do không thỏa bộ lọc");

                var task = await _blogRepsitory.Create(request);
                return new HandleResponse<bool>(true, "Thành công");
            }
            catch(Exception ex)
            {
                return new HandleResponse<bool>(false, ex.ToString());
            }
        }

        [HttpGet]
        [Route(nameof(SearchBlog))]
        public async Task<IActionResult> SearchBlog([FromQuery] BlogSearch blogSearch)
        {
            var reusult = await _blogRepsitory.GetAllBlogPost(blogSearch);

            return Ok(reusult);
        }

        [HttpGet]
        [Route(nameof(GetAllTopic))]
        public async Task<HandleList<CategoryBlogRequest>> GetAllTopic()
        {
            try
            {
                var listMenu = await _blogRepsitory.GetAllMenu();
                var result = listMenu.Select(x => new CategoryBlogRequest()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name
                }).ToList();
                return new HandleList<CategoryBlogRequest>(result);
            }
            catch (Exception ex)
            {
                return new HandleList<CategoryBlogRequest>();
            }
        }

    }
}
