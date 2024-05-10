using PostsService.Features.Dtos;
using PostsService.Infrastructure.DTOs.REST;

namespace PostsService.Features.Services;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAll(Pagination pagination);
    Task<IEnumerable<PostDto>> GetTopRated();
    Task<IEnumerable<PostDto>> CacheFrequentPosts();

    Task<IEnumerable<PostDto>> GetAllByCategoryId(int categoryId, Pagination pagination);
    Task<IEnumerable<PostDto>> GetAllByTagId(int tagId, Pagination pagination);

    Task<PostDto?> FindById(int id);
    Task<PostDto?> FindByGuid(Guid guid);

    Task<PostDto> Insert(PostFormDto entity);
    Task<PostDto> Update(int id, PostFormUpdateDto entity);
    Task<PostDto> AddTags(int id, IEnumerable<TagFormDto> tags);
    Task<PostDto> RemoveTags(int id, IEnumerable<int> tagIds);
    Task<bool> Delete(int id);
    Task<int> GetCount();
}
