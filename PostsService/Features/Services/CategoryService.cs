using Mapster;
using Microsoft.EntityFrameworkCore;
using PostsService.Features.Dtos;
using PostsService.Features.Entities;
using PostsService.Features.Repositories;
using PostsService.Infrastructure.DTOs.REST;
using PostsService.Infrastructure.Extentions;

namespace PostsService.Features.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<IEnumerable<CategoryDto>> GetAll(Pagination pagination)
    {
        var data = await _categoryRepository.GetAll()
                                           .Paginate(pagination.Page, pagination.Size)
                                           .ToListAsync();

        return data.Adapt<IEnumerable<CategoryDto>>();
    }
    public async Task<CategoryDto?> FindById(int id)
    {
        var data = await _categoryRepository.FindById(id);

        return data?.Adapt<CategoryDto>();
    }
    public async Task<CategoryDto?> FindByGuid(Guid guid)
    {
        var data = await _categoryRepository.FindByGuid(guid);

        return data?.Adapt<CategoryDto>();

    }
    public async Task<CategoryDto> Insert(CategoryFormDto entity)
    {
        var data = entity.Adapt<Category>();

        await _categoryRepository.Add(data, true);

        return data.Adapt<CategoryDto>();
    }
    public async Task<CategoryDto> Update(CategoryFormDto entity)
    {
        var data = entity.Adapt<Category>();

        await _categoryRepository.Update(data, true);

        return data.Adapt<CategoryDto>();
    }
    public async Task<bool> Delete(int id)
    {
        var data = await _categoryRepository.FindById(id);

        if (data == null) return false;

        await _categoryRepository.Delete(data, true);

        return true;
    }
    public async Task<int> GetCount()
    {
        return await _categoryRepository.Count();
    }
}
