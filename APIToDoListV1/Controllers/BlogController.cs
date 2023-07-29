using APIToDoListV1.Reponsitories;
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
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var task = await _blogRepsitory.GetById(id);
            if (task == null) return BadRequest();
            return      Ok(task);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogPostReq request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _blogRepsitory.Create(request);

            return Ok(task);
        }

    }
}
