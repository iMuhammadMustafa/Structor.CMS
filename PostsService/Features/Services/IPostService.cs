using PostsService.Features.Dtos;
using PostsService.Infrastructure.DTOs.REST;

namespace PostsService.Features.Services;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAll(Pagination pagination);
    Task<IEnumerable<PostDto>> GetAllByCategoryId(int categoryId, Pagination pagination);
    Task<IEnumerable<PostDto>> GetAllByTagId(int tagId, Pagination pagination);

    Task<PostDto?> FindById(int id);
    Task<PostDto?> FindByGuid(Guid guid);

    Task<PostDto> Insert(PostFormDto entity);
    Task<PostDto> Update(PostFormDto entity);
    Task<bool> Delete(int id);
    Task<int> GetCount();
}
