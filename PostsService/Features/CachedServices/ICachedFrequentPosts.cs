using PostsService.Features.Dtos;

namespace PostsService.Features.CachedServices;

public interface ICachedFrequentPosts
{
    Task<IEnumerable<PostDto>?> GetCachedPosts();
    Task<bool> SetCachedPosts(IEnumerable<PostDto> posts);

}
