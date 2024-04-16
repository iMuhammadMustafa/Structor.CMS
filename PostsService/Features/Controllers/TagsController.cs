using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PostsService.Features.Dtos;
using PostsService.Features.Services;
using PostsService.Infrastructure.DTOs.REST;


namespace PostsService.Features.Controllers;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<TagDto>>>> Get([FromQuery] Pagination pagination)
    {
        var data = await _tagService.GetAll(pagination);

        return new Response<IEnumerable<TagDto>>().WithData(data).WithPagination(pagination);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<TagDto>>> Get([FromRoute] int id)
    {
        var data = await _tagService.FindById(id);

        return new Response<TagDto>().WithData(data);
    }

    [HttpPost]
    public async Task<Response<TagDto>> Post([FromBody] TagFormDto tagFormDto)
    {
        var data = await _tagService.Insert(tagFormDto);
        return new Response<TagDto>().WithData(data);
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<Response<TagDto>> Put([FromRoute] int id, [FromBody] TagFormDto tagFormDto)
    {
        var data = await _tagService.Update(id, tagFormDto);
        return new Response<TagDto>().WithData(data);

    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _tagService.Delete(id);
    }
}
