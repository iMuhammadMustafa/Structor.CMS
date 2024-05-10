using Microsoft.Extensions.Caching.Distributed;
using PostsService.Features.Dtos;
using System.Text.Json;

namespace PostsService.Features.CachedServices;

//TODO: Needs to be Generic
public class CachedTopRatedPosts : ICachedTopRatedPosts
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _configuration;

    private const string KEY_NAME = "Redis:Keys:Posts:TopRated";
    private readonly string _cachekey;
    public CachedTopRatedPosts(IDistributedCache cache,
                               IConfiguration configuration)
    {
        _cache = cache;
        _configuration = configuration;

        _cachekey = _configuration[KEY_NAME] ?? throw new Exception("Cache Key Name Not found in Configuration");
    }

    public async Task<IEnumerable<PostDto>?> GetCachedPosts()
    {
        var posts = await _cache.GetStringAsync(_cachekey);

        if (posts is null) return null;

        var data = JsonSerializer.Deserialize<IEnumerable<PostDto>>(posts);

        return data;
    }


    public async Task<bool> SetCachedPosts(IEnumerable<PostDto> posts)
    {
        var data = JsonSerializer.Serialize(posts);

        if (string.IsNullOrWhiteSpace(data)) return false;

        await _cache.SetStringAsync(_cachekey, data);

        return true;
    }
}
