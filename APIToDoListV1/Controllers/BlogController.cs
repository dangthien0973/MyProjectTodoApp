using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.SeekWork;
using Model;
using Model.Enums;

using Model.Blog;

namespace APIToDoListV1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController :  ControllerBase
    {
        private readonly IBlogReponsitory _blogRepsitory;
        public BlogController(IBlogReponsitory blogRepsitory)
        {
            _blogRepsitory = blogRepsitory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlog()
        {
            var listTodo = await _blogRepsitory.GetAllBlogPost();
            return Ok(listTodo);
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
