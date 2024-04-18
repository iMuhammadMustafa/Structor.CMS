using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PostsService.Features.Dtos;
using PostsService.Features.Services;
using PostsService.Infrastructure.DTOs.REST;


namespace PostsService.Features.Controllers;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postsService;

    public PostsController(IPostService postsService)
    {
        _postsService = postsService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<PostDto>>>> GetAll([FromQuery] int? categoryId,
                                                                           [FromQuery] int? tagId,
                                                                           [FromQuery] Pagination? pagination)
    {
        if (pagination == null) pagination = new();
        var res = new Response<IEnumerable<PostDto>>();

        IEnumerable<PostDto> data;

        if (categoryId.HasValue)
        {
            data = await _postsService.GetAllByCategoryId(categoryId.Value, pagination);
        }
        else if (tagId.HasValue)
        {
            data = await _postsService.GetAllByTagId(tagId.Value, pagination);
        }
        else
        {
            data = await _postsService.GetAll(pagination);
        }

        res.WithData(data)
           .WithPagination(pagination);
        return Ok(res);
    }

    [HttpGet("Frequent")]
    public async Task<ActionResult<Response<IEnumerable<PostDto>>>> GetFrequent()
    {

        IEnumerable<PostDto> data = await _postsService.GetFrequent();

        var res = new Response<IEnumerable<PostDto>>();

        res.WithData(data);
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
    public async Task Update([FromRoute] int id, [FromBody] PostFormUpdateDto postDto)
    {
        var result = await _postsService.Update(id, postDto);
    }
    [HttpPut("{id}/tags/add")]
    public async Task AddTags(int id, [FromBody] IEnumerable<TagFormDto> tags)
    {
        var result = await _postsService.AddTags(id, tags);
    }
    [HttpPut("{id}/tags/remove")]
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
