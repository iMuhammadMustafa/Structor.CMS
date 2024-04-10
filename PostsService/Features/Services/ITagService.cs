using PostsService.Features.Dtos;
using PostsService.Infrastructure.DTOs.REST;

namespace PostsService.Features.Services;

public interface ITagService
{
    Task<IEnumerable<TagDto>> GetAll(Pagination pagination);
    Task<TagDto?> FindById(int id);
    Task<TagDto?> FindByGuid(Guid guid);

    Task<TagDto> Insert(TagFormDto entity);
    Task<TagDto> Update(TagFormDto entity);
    Task<bool> Delete(int id);
    Task<int> GetCount();
}
