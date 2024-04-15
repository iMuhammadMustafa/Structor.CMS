using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PostsService.Features.Dtos;
using PostsService.Features.Services;
using PostsService.Infrastructure.DTOs.REST;


namespace PostsService.Features.Controllers;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]/[action]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postsService;

    public PostsController(IPostService postsService)
    {
        _postsService = postsService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<PostDto>>>> GetAll([FromQuery] Pagination pagination)
    {
        var res = new Response<IEnumerable<PostDto>>();

        var data = await _postsService.GetAll(pagination);

        res.WithData(data)
           .WithPagination(pagination);
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<PostDto>>>> GetAllByCategory([FromQuery] int categoryId, [FromQuery] Pagination pagination)
    {
        var res = new Response<IEnumerable<PostDto>>();

        var data = await _postsService.GetAllByCategoryId(categoryId, pagination);

        res.WithData(data)
           .WithPagination(pagination);
        return Ok(res);
    }
    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<PostDto>>>> GetAllByTag([FromQuery] int tagId, [FromQuery] Pagination pagination)
    {
        var data = await _postsService.GetAllByTagId(tagId, pagination);


        var res = Response<IEnumerable<PostDto>>.Create()
                                                .WithData(data)
                                                .WithPagination(pagination);
        return Ok(res);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<PostDto>>> GetById(int id)
    {
        var data = await _postsService.FindById(id);

        if (data is null) throw new Exception("This Id Is not found");

        var res = Response<PostDto>.Create().WithData(data);

        return Ok(res);

    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> Post([FromBody] PostFormDto postDto)
    {
        var result = await _postsService.Insert(postDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] PostFormUpdateDto postDto)
    {
        var result = await _postsService.Update(id, postDto);
    }
    [HttpPut("{id}")]
    public async Task AddTags(int id, [FromBody] IEnumerable<TagFormDto> tags)
    {
        var result = await _postsService.AddTags(id, tags);
    }
    [HttpPut("{id}")]
    public async Task RemoveTags(int id, [FromBody] IEnumerable<int> tagIds)
    {
        var result = await _postsService.RemoveTags(id, tagIds);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _postsService.Delete(id);

        return Ok(result);
    }
}
