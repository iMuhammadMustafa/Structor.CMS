using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PostsService.Features.Dtos;
using PostsService.Features.Services;
using PostsService.Infrastructure.DTOs.REST;


namespace PostsService.Features.Controllers;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<CategoryDto>>>> Get([FromQuery] Pagination pagination)
    {
        var data = await _categoryService.GetAll(pagination);

        return new Response<IEnumerable<CategoryDto>>().WithData(data).WithPagination(pagination);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<CategoryDto>>> Get([FromRoute] int id)
    {
        var data = await _categoryService.FindById(id);

        return new Response<CategoryDto>().WithData(data);
    }

    [HttpPost]
    public async Task<Response<CategoryDto>> Post([FromBody] CategoryFormDto categoryFormDto)
    {
        var data = await _categoryService.Insert(categoryFormDto);
        return new Response<CategoryDto>().WithData(data);
    }

    [HttpPut("{id}")]
    public async Task<Response<CategoryDto>> Put([FromRoute] int id, [FromBody] CategoryFormDto categoryFormDto)
    {
        var data = await _categoryService.Update(id, categoryFormDto);
        return new Response<CategoryDto>().WithData(data);

    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _categoryService.Delete(id);
    }
}
