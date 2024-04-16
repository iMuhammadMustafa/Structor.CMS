using AutoMapper;
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
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CategoryDto>> GetAll(Pagination pagination)
    {
        var data = await _categoryRepository.GetAll()
                                           .Paginate(pagination.Page, pagination.Size)
                                           .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryDto>>(data);
    }
    public async Task<CategoryDto?> FindById(int id)
    {
        var data = await _categoryRepository.FindById(id);

        return _mapper.Map<CategoryDto>(data);
    }
    public async Task<CategoryDto?> FindByGuid(Guid guid)
    {
        var data = await _categoryRepository.FindByGuid(guid);

        return _mapper.Map<CategoryDto>(data);

    }
    public async Task<CategoryDto> Insert(CategoryFormDto entity)
    {
        var data = _mapper.Map<Category>(entity);

        await _categoryRepository.Add(data, true);

        return _mapper.Map<CategoryDto>(data);
    }
    public async Task<CategoryDto> Update(int id, CategoryFormDto entity)
    {
        var data = _mapper.Map<Category>(entity);
        data.Id = id;

        await _categoryRepository.Update(data, true);

        return _mapper.Map<CategoryDto>(data);
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
