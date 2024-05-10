using PostsService.Features.Dtos;

namespace PostsService.Features.CachedServices;

public interface ICachedTopRatedPosts
{
    Task<IEnumerable<PostDto>?> GetCachedPosts();
    Task<bool> SetCachedPosts(IEnumerable<PostDto> posts);

}
