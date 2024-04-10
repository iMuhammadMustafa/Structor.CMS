using PostsService.Features.Dtos;
using PostsService.Infrastructure.DTOs.REST;

namespace PostsService.Features.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll(Pagination pagination);
    Task<CategoryDto?> FindById(int id);
    Task<CategoryDto?> FindByGuid(Guid guid);
    Task<CategoryDto> Insert(CategoryFormDto entity);
    Task<CategoryDto> Update(CategoryFormDto entity);
    Task<bool> Delete(int id);
    Task<int> GetCount();
}
