using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostsService.Entities;
using PostsService.Repositories;

namespace PostsService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public ValuesController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Post>>> Index()
        {
            var post = new Post
            {
                Title = "Test",
                Author = "Me",
                AuthorId = 1,

            };
            await _postRepository.Add(post, true);


            var res = await _postRepository.GetAll().ToListAsync();

            return Ok(res);
        }
    }
}
